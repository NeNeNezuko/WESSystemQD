using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{
    private readonly WarehouseDbContext _warehouseCtx;
    private readonly AppDbContext _appCtx;

    public WarehouseController(WarehouseDbContext warehouseCtx, AppDbContext appCtx)
    {
        _warehouseCtx = warehouseCtx;
        _appCtx = appCtx;
    }

    /// <summary>
    /// 搜索仓库列表（分页）
    /// </summary>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? keyword = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
    {
        var query = _warehouseCtx.MyWhs.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(w => w.WH.Contains(keyword) || (w.NAME != null && w.NAME.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(w => new 
            { 
                WH = w.WH, 
                NAME = w.NAME, 
                WH_TYPE = w.WH_TYPE, 
                ATTRIB = w.ATTRIB, 
                DEP = w.DEP, 
                INVALID = w.INVALID 
            })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }

    /// <summary>
    /// 搜索货品主档列表（分页，查询PRDT表）
    /// </summary>
    [HttpGet("prdsearch")]
    public async Task<IActionResult> SearchProduct(
        [FromQuery] string? keyword = null,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _warehouseCtx.Prdts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(p => p.PRD_NO.Contains(keyword) || (p.NAME != null && p.NAME.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(p => p.PRD_NO)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new
            {
                PRD_NO = p.PRD_NO,
                NAME = p.NAME ?? "",
                SNM = p.SNM ?? "",
                IDX1 = p.IDX1 ?? "",
                UT = p.UT ?? "",
                SPC = p.SPC ?? ""
            })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }

    /// <summary>
    /// 获取表的字段元数据（从 DICT_TAB + DICT_FLD 查询）
    /// </summary>
    [HttpGet("columns")]
    public async Task<IActionResult> GetColumns([FromQuery] string tableName)
    {
        var result = await _appCtx.DictFlds
            .Where(f => f.TAB_NAME == tableName)
            .Select(f => new
            {
                FieldKey = f.FLD_NAME,
                DisplayName = string.IsNullOrWhiteSpace(f.Note) ? f.FLD_NAME : f.Note
            })
            .ToListAsync();

        return Ok(result);
    }

    /// <summary>
    /// 搜索入库业务类型列表（分页）
    /// </summary>
    [HttpGet("rktypesearch")]
    public async Task<IActionResult> SearchRkType([FromQuery] string? keyword = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
    {
        var query = _warehouseCtx.RkTypeSets.Where(r => r.CR_TYPE == "1");

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(r => r.TYPE_ID.Contains(keyword) || (r.NAME != null && r.NAME.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(r => r.TYPE_ID)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new
            {
                TYPE_ID = r.TYPE_ID,
                NAME = r.NAME ?? ""
            })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }

    /// <summary>
    /// 搜索出库业务类型列表（分页，CR_TYPE=2）
    /// </summary>
    [HttpGet("cktypesearch")]
    public async Task<IActionResult> SearchCkType(
        [FromQuery] string? keyword = null,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        var query = _warehouseCtx.RkTypeSets.Where(r => r.CR_TYPE == "2");

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(r => r.TYPE_ID.Contains(keyword)
                || (r.NAME != null && r.NAME.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(r => r.TYPE_ID)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new
            {
                TYPE_ID = r.TYPE_ID,
                NAME = r.NAME ?? ""
            })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }

    /// <summary>
    /// 搜索部门列表（分页）
    /// </summary>
    [HttpGet("deptsearch")]
    public async Task<IActionResult> SearchDept([FromQuery] string? keyword = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var query = _warehouseCtx.Depts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(d => d.DEP.Contains(keyword) || (d.NAME != null && d.NAME.Contains(keyword)));
        }

        var totalCount = await query.CountAsync();
        var items = await query
            .OrderBy(d => d.DEP)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new
            {
                DEP = d.DEP,
                NAME = d.NAME ?? ""
            })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }

    /// <summary>
    /// 搜索客户列表（分页，从MF_CKTB去重获取）
    /// </summary>
    [HttpGet("customersearch")]
    public async Task<IActionResult> SearchCustomer([FromQuery] string? keyword = null, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var baseQuery = _warehouseCtx.MfCktbs
            .Where(m => m.CUS_NO != null && m.CUS_NO != "");

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            baseQuery = baseQuery.Where(m => m.CUS_NO.Contains(keyword)
                || (m.CUS_NAME != null && m.CUS_NAME.Contains(keyword)));
        }

        var distinctQuery = baseQuery
            .GroupBy(m => new { m.CUS_NO, m.CUS_NAME })
            .Select(g => g.Key);

        var totalCount = await distinctQuery.CountAsync();
        var items = await distinctQuery
            .OrderBy(x => x.CUS_NO)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new { CUS_NO = x.CUS_NO, CUS_NAME = x.CUS_NAME ?? "" })
            .ToListAsync();

        return Ok(new { items, totalCount });
    }
}
