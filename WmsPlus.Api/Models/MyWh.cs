namespace WmsPlus.Api.Models;

public class MyWh
{
    public string WH { get; set; } = "";
    public string? NAME { get; set; }
    public string? WH_TYPE { get; set; }
    public string? ATTRIB { get; set; }
    public string? DEP { get; set; }
    public string? INVALID { get; set; }
    public string? CW_FLAG { get; set; }
    public string? UP_WH { get; set; }
    public string? REM { get; set; }
    public string? USR { get; set; }
    public DateTime? STOP_DD { get; set; }        // 停用日期
    public DateTime? UP_DD { get; set; }          // 上次更新时间

    // ===== 基本信息扩展字段 =====
    public string? CNT_MAN { get; set; }        // 联系人代号
    public string? TEL_NO { get; set; }          // 电话
    public string? FAX_NO { get; set; }          // 传真
    public string? DEPRO_NO { get; set; }        // 部门群组代号

    // ===== 仓库管理扩展字段 =====
    public string? RK_FLOW { get; set; }         // 入库流程(1.入库-上架、2.入库同时上架)
    public string? PK_FLOW { get; set; }         // 拣货流程(1、总拣-分拣 2、直接拣货)
    public string? CK_FLOW { get; set; }         // 出库流程标记
    public string? IC_TYPE { get; set; }         // 调拨方式
    public string? MODE_PG_PK { get; set; }      // 拣货派工方式
    public string? PD_MTH { get; set; }          // 盘点方式
    public int? XJ_BILL_COUNT { get; set; }      // 分组单据容量
    public string? XJ_GROUP_COND { get; set; }   // 自动分组条件
    public string? XJ_KCBZCL { get; set; }       // 库存不足处理
    public string? XJ_PWCKYJ { get; set; }       // 配位仓库依据
    public string? XJ_WHS { get; set; }          // 指定仓库
    public string? RULE_ID_BC { get; set; }      // 波次策略代号
    public string? RULE_ID_PK { get; set; }      // 拣货策略代号
    public string? RULE_ID_XJ { get; set; }      // 下架策略代号

    // ===== 储位管理扩展字段 =====
    public string? HJFL { get; set; }            // 货架分类
    public string? MULT_CW_BY { get; set; }      // 多深位依据
    public string? SHUTTLE_AQYCF { get; set; }   // 穿梭式货架-按区域设定存放规则
    public string? SHUTTLE_CFZLYJ { get; set; }  // 穿梭式货架-巷道存放种类依据
    public string? SHUTTLE_GS { get; set; }      // 穿梭式货架-巷道规则
    public string? SHUTTLE_SORT { get; set; }    // 穿梭式货架-配位顺序
    public string? RK_CHUW_SORT { get; set; }    // 入库储位配位排序（非穿梭式）
    public string? RK_CHUW_SORT2 { get; set; }   // 入库储位配位排序（穿梭式）
    public string? KRQRK_CHUW_SORT { get; set; } // 空容器入库储位配位排序
    public string? ALLOW_KRQSJ { get; set; }     // 空容器允许上架
    public string? ALLOW_BHRQSJ { get; set; }    // 启用备货容器上架管理
    public string? ALLOW_STATUS_JY { get; set; } // 允许上架的检验状态
    public int? QTY_KEEP_CW { get; set; }        // 预留空储位数
    public string? CAPACITY_TYPE { get; set; }   // 储位容量管制方式
    public string? FLAG_DG { get; set; }         // 是代管仓
    public string? FLAG_FKC { get; set; }        // 负库存控制
    public string? PTL_SW { get; set; }          // 启用PTL电子标签拣货
    public string? LKIF_ID { get; set; }         // 立库接口代号
    public string? MAP_NO { get; set; }          // 地图编号
    public DateTime? SYS_DATE { get; set; }      // 输入日期
    public string? CUS_NO { get; set; }          // 客户代号
    public string? NAME_PY { get; set; }         // 助记码
    public string? TP_ID { get; set; }           // 第三方标记
    public string? TYWZ { get; set; }            // 检验位置
    public string? WH_TY { get; set; }           // 检验仓库
    public string? WMS_ID { get; set; }          // WMS接管仓库标记
    public string? RQ_SXJ_MODE { get; set; }     // 容器上下架方式
}
