using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcePickingController : ControllerBase
    {
        private readonly WarehouseDbContext _db;
        private readonly ILogger<SourcePickingController> _logger;

        public SourcePickingController(WarehouseDbContext db, ILogger<SourcePickingController> logger)
        {
            _db = db;
            _logger = logger;
        }

        /// <summary>
        /// 批量上架查询（基于MF_XJRW表头）
        /// </summary>
        [HttpGet("batch-search")]
        public async Task<IActionResult> BatchSearch(
            string? dateFrom,
            string? dateTo,
            string? warehouseCode,
            string? documentNumber,
            string? businessType,
            string? deptCode,
            string? customerCode,
            string? businessOrderNumber,
            string? erpApplyOrderNumber,
            string? receivePoint)
        {
            try
            {
                var query = _db.Set<MfXjrw>().AsQueryable();

                // 日期范围筛选
                if (DateTime.TryParse(dateFrom, out var df))
                    query = query.Where(m => m.JR_DD >= df);
                if (DateTime.TryParse(dateTo, out var dt))
                    query = query.Where(m => m.JR_DD <= dt.AddDays(1));

                if (!string.IsNullOrWhiteSpace(warehouseCode))
                    query = query.Where(m => m.WH != null && m.WH.Contains(warehouseCode));
                if (!string.IsNullOrWhiteSpace(documentNumber))
                    query = query.Where(m => m.JR_NO != null && m.JR_NO.Contains(documentNumber));
                if (!string.IsNullOrWhiteSpace(businessType))
                    query = query.Where(m => m.TYPE_ID != null && m.TYPE_ID.Contains(businessType));
                if (!string.IsNullOrWhiteSpace(deptCode))
                    query = query.Where(m => m.DEP != null && m.DEP.Contains(deptCode));

                var list = await query
                    .OrderByDescending(m => m.JR_DD)
                    .ThenBy(m => m.JR_NO)
                    .Select(m => new BatchShelvingDto
                    {
                        Priority = m.PRIORITY_WCS.HasValue ? m.PRIORITY_WCS.Value.ToString() : "",
                        DocumentDate = m.JR_DD,
                        DocumentNumber = m.JR_NO ?? "",
                        BusinessType = m.TYPE_ID ?? "",
                        ErpApplyOrderNumber = "",
                        BusinessOrderNumber = m.PR_NO ?? "",
                        PreOutboundDate = null,
                        CustomerName = "",
                        DeptName = ""
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "批量上架查询失败");
                return Ok(new ApiResponse<object> { Success = false, Message = ex.Message });
            }
        }

        /// <summary>
        /// 直接拣货任务单查询（MF_XJRW表头 + TF_XJRW表身明细）
        /// </summary>
        [HttpGet("pick-search")]
        public async Task<IActionResult> PickSearch(
            string? dateFrom,
            string? dateTo,
            string? documentNumber,
            string? businessType,
            string? warehouseCode,
            string? applyOrderNumber,
            string? noticeNumber,
            string? businessOrderNumber)
        {
            try
            {
                var mainQuery = _db.Set<MfXjrw>().AsQueryable();

                // 日期范围筛选
                if (DateTime.TryParse(dateFrom, out var df))
                    mainQuery = mainQuery.Where(m => m.JR_DD >= df);
                if (DateTime.TryParse(dateTo, out var dt))
                    mainQuery = mainQuery.Where(m => m.JR_DD <= dt.AddDays(1));

                if (!string.IsNullOrWhiteSpace(documentNumber))
                    mainQuery = mainQuery.Where(m => m.JR_NO != null && m.JR_NO.Contains(documentNumber));
                if (!string.IsNullOrWhiteSpace(businessType))
                    mainQuery = mainQuery.Where(m => m.TYPE_ID != null && m.TYPE_ID.Contains(businessType));
                if (!string.IsNullOrWhiteSpace(warehouseCode))
                    mainQuery = mainQuery.Where(m => m.WH != null && m.WH.Contains(warehouseCode));
                if (!string.IsNullOrWhiteSpace(noticeNumber))
                    mainQuery = mainQuery.Where(m => m.PR_NO != null && m.PR_NO.Contains(noticeNumber));

                // 查询主表数据
                var mainList = await mainQuery
                    .OrderByDescending(m => m.JR_DD)
                    .ThenBy(m => m.JR_NO)
                    .Select((m, idx) => new PickMainDto
                    {
                        ItemNo = 0,
                        TaskNumber = m.JR_NO ?? "",
                        ShelvingDate = m.MODIFY_DD,
                        Priority = m.PRIORITY_WCS.HasValue ? m.PRIORITY_WCS.Value.ToString() : "",
                        WarehouseName = m.WH ?? "",
                        ContainerBarcode = m.CONTAIN_CODE ?? "",
                        CloseMark = m.CLS_ID ?? "",
                        ShelvingMark = ""
                    })
                    .ToListAsync();

                // 赋值项次
                for (int i = 0; i < mainList.Count; i++)
                    mainList[i].ItemNo = i + 1;

                // 关联部门名称
                var depCodes = mainList.Select(m => m.TaskNumber).ToList();
                // 部门名称需通过额外查询或LEFT JOIN补充，此处先留空

                // 查询明细表数据（TF_XJRW）
                IQueryable<TfXjrw> detailQuery = _db.Set<TfXjrw>().AsQueryable();
                if (mainList.Any())
                {
                    var taskNos = mainList.Select(m => m.TaskNumber).ToList();
                    detailQuery = detailQuery.Where(d => taskNos.Contains(d.JR_NO));
                }

                var detailList = await detailQuery
                    .OrderBy(d => d.JR_NO)
                    .ThenBy(d => d.ITM)
                    .Select(d => new PickDetailDto
                    {
                        TaskNumber = d.JR_NO ?? "",
                        Itm = d.ITM,
                        PrdNo = d.PRD_NO ?? "",
                        PrdName = d.PRD_NAME ?? "",
                        BatNo = d.BAT_NO ?? "",
                        Unit = d.UNIT ?? "",
                        Qty = d.QTY ?? 0,
                        PickQty = d.QTY_PK ?? 0,
                        PasteId = d.ERP_BIL_ID == null ? "" : d.ERP_BIL_ID.ToString(),
                        PasteNo = d.ERP_BIL_NO ?? "",
                        PasteItm = d.ERP_BIL_ITM ?? 0,
                        Ef = ""
                    })
                    .ToListAsync();

                return Ok(new ApiResponse<PickSearchResultDto>
                {
                    Success = true,
                    Data = new PickSearchResultDto
                    {
                        MainList = mainList,
                        DetailList = detailList
                    },
                    Total = mainList.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "直接拣货任务单查询失败");
                return Ok(new ApiResponse<PickSearchResultDto> { Success = false, Message = ex.Message });
            }
        }
    }

    // ====== DTO 定义 ======
    public class BatchShelvingDto
    {
        [JsonPropertyName("priority")]
        public string Priority { get; set; } = "";
        [JsonPropertyName("documentDate")]
        public DateTime? DocumentDate { get; set; }
        [JsonPropertyName("documentNumber")]
        public string DocumentNumber { get; set; } = "";
        [JsonPropertyName("businessType")]
        public string BusinessType { get; set; } = "";
        [JsonPropertyName("erpApplyOrderNumber")]
        public string ErpApplyOrderNumber { get; set; } = "";
        [JsonPropertyName("businessOrderNumber")]
        public string BusinessOrderNumber { get; set; } = "";
        [JsonPropertyName("preOutboundDate")]
        public DateTime? PreOutboundDate { get; set; }
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; } = "";
        [JsonPropertyName("deptName")]
        public string DeptName { get; set; } = "";
    }

    public class PickMainDto
    {
        [JsonPropertyName("itemNo")]
        public int ItemNo { get; set; }
        [JsonPropertyName("taskNumber")]
        public string TaskNumber { get; set; } = "";
        [JsonPropertyName("shelvingDate")]
        public DateTime? ShelvingDate { get; set; }
        [JsonPropertyName("deptName")]
        public string DeptName { get; set; } = "";
        [JsonPropertyName("priority")]
        public string Priority { get; set; } = "";
        [JsonPropertyName("warehouseName")]
        public string WarehouseName { get; set; } = "";
        [JsonPropertyName("containerBarcode")]
        public string ContainerBarcode { get; set; } = "";
        [JsonPropertyName("closeMark")]
        public string CloseMark { get; set; } = "";
        [JsonPropertyName("shelvingMark")]
        public string ShelvingMark { get; set; } = "";
    }

    public class PickDetailDto
    {
        [JsonPropertyName("taskNumber")]
        public string TaskNumber { get; set; } = "";
        [JsonPropertyName("itm")]
        public int Itm { get; set; }
        [JsonPropertyName("prdNo")]
        public string PrdNo { get; set; } = "";
        [JsonPropertyName("prdName")]
        public string PrdName { get; set; } = "";
        [JsonPropertyName("batNo")]
        public string BatNo { get; set; } = "";
        [JsonPropertyName("unit")]
        public string Unit { get; set; } = "";
        [JsonPropertyName("qty")]
        public decimal Qty { get; set; }
        [JsonPropertyName("pickQty")]
        public decimal PickQty { get; set; }
        [JsonPropertyName("pasteId")]
        public string PasteId { get; set; } = "";
        [JsonPropertyName("pasteNo")]
        public string PasteNo { get; set; } = "";
        [JsonPropertyName("pasteItm")]
        public int PasteItm { get; set; }
        [JsonPropertyName("ef")]
        public string Ef { get; set; } = "";
    }

    public class PickSearchResultDto
    {
        [JsonPropertyName("mainList")]
        public List<PickMainDto>? MainList { get; set; }
        [JsonPropertyName("detailList")]
        public List<PickDetailDto>? DetailList { get; set; }
    }

    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; } = "";
        [JsonPropertyName("data")]
        public T? Data { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
