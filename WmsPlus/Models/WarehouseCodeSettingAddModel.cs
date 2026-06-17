namespace WmsPlus.Models;

/// <summary>仓库代号设定新增/编辑页面数据模型</summary>
public class WarehouseCodeSettingAddModel
{
    // ===== 基本信息 =====
    public string Wh { get; set; } = "";              // 仓库代号*
    public string Name { get; set; } = "";            // 仓库名称*
    public string Attrib { get; set; } = "";          // 属性
    public string UpWh { get; set; } = "";            // 上层仓库
    public string CntMan { get; set; } = "";          // 联系人
    public string TelNo { get; set; } = "";           // 电话
    public string FaxNo { get; set; } = "";           // 传真
    public DateTime? StopDd { get; set; }             // 停用日期
    public string Dep { get; set; } = "";             // 所属部门
    public string DeproNo { get; set; } = "";         // 部门群组
    public string Rem { get; set; } = "";             // 备注

    // ===== 仓库管理 =====
    public string RkFlow { get; set; } = "";          // 出库流程
    public string PkFlow { get; set; } = "";          // 拣货流程
    public string CkFlow { get; set; } = "";          // 库存不足管制
    public string IcType { get; set; } = "";          // 调拨方式
    public string ModePgPk { get; set; } = "";        // 拣货派工方式
    public string PdMth { get; set; } = "";           // 盘点方式
    public int? XjBillCount { get; set; }             // 分组单据容量
    public string XjGroupCond { get; set; } = "";     // 自动分组条件
    public string XjKcbzcl { get; set; } = "";        // 库存不足处理
    public string XjPwckyj { get; set; } = "";        // 配位仓库依据
    public string XjWhs { get; set; } = "";           // 指定仓库
    public string RuleIdBc { get; set; } = "";        // 波次策略代号
    public string RuleIdPk { get; set; } = "";        // 拣货策略代号
    public string RuleIdXj { get; set; } = "";        // 下架策略代号

    // ===== 储位管理 =====
    public bool CwFlag { get; set; }                  // 启用储位管理
    public string WhType { get; set; } = "";          // 上下架模式
    public string Hjfl { get; set; } = "";            // 货架分类
    public string MultCwBy { get; set; } = "";        // 多深位依据
    public bool ShuttleAqycf { get; set; }            // 按区域设定存放规则
    public string ShuttleCfzlyj { get; set; } = "";   // 巷道存放种类依据
    public string ShuttleGs { get; set; } = "";       // 巷道规则
    public string ShuttleSort { get; set; } = "";     // 配位顺序
    public string RkChuwSort { get; set; } = "";      // 入库储位配位排序（非穿梭式）
    public string RkChuwSort2 { get; set; } = "";     // 入库储位配位排序（穿梭式）
    public string KrqrkChuwSort { get; set; } = "";   // 空容器入库储位配位排序
    public bool AllowKrqsj { get; set; }              // 空容器允许上架
    public bool AllowBhrqsj { get; set; }             // 启用备货容器上架管理
    public string AllowStatusJy { get; set; } = "";   // 允许上架的检验状态
    public int? QtyKeepCw { get; set; }               // 预留空储位数
    public string CapacityType { get; set; } = "";    // 储位容量管制方式
    public bool FlagDg { get; set; }                  // 是代管仓
    public bool FlagFkc { get; set; }                 // 负库存控制
    public bool PtlSw { get; set; }                   // 启用PTL电子标签拣货
    public string LkifId { get; set; } = "";          // 立库接口代号
    public string MapNo { get; set; } = "";           // 地图编号
}
