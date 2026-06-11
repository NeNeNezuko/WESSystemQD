namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 单据条码明细表（PDA_BAR_COLLECT）
/// 对应数据库 db_gz01.PDA_BAR_COLLECT 表
/// </summary>
public class PdaBarCollect
{
    /// <summary>自动装箱上架标记</summary>
    public string? AUTO_BOX_SJ_FLAG { get; set; }

    /// <summary>自动装箱1上架标记</summary>
    public string? AUTO_BOX1_SJ_FLAG { get; set; }

    /// <summary>自动容器上架标记</summary>
    public string? AUTO_CONTAIN_SJ_FLAG { get; set; }

    /// <summary>条码</summary>
    public string? BAR_CODE { get; set; }

    /// <summary>条码类型</summary>
    public string? BAR_TYPE { get; set; }

    /// <summary>波次号</summary>
    public string? BC_NO { get; set; }

    /// <summary>单据日期</summary>
    public DateTime? BIL_DD { get; set; }

    /// <summary>单据别（主键之一）</summary>
    public string? BIL_ID { get; set; }

    /// <summary>单据项次（主键之一）</summary>
    public int? BIL_ITM { get; set; }

    /// <summary>单据号码（主键之一）</summary>
    public string? BIL_NO { get; set; }

    /// <summary>单据类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>箱号</summary>
    public string? BOX_NO { get; set; }

    /// <summary>子箱号</summary>
    public string? BOX_NO_SUB { get; set; }

    /// <summary>箱号1</summary>
    public string? BOX_NO1 { get; set; }

    /// <summary>装箱上架标记</summary>
    public string? BOX_SJ_FLAG { get; set; }

    /// <summary>装箱1上架标记</summary>
    public string? BOX1_SJ_FLAG { get; set; }

    /// <summary>车号</summary>
    public string? CAR_NO { get; set; }

    /// <summary>检查回原日期要求</summary>
    public string? CHK_BACKORIGRQ { get; set; }

    /// <summary>检查箱1</summary>
    public string? CHK_BOX1 { get; set; }

    /// <summary>检查日期要求</summary>
    public string? CHK_RBRQ { get; set; }

    /// <summary>储位1</summary>
    public string? CHUW1 { get; set; }

    /// <summary>储位2</summary>
    public string? CHUW2 { get; set; }

    /// <summary>客户项次</summary>
    public int? CLIENT_ITEM { get; set; }

    /// <summary>批号</summary>
    public string? CODE_BAT_NO { get; set; }

    /// <summary>货品特征</summary>
    public string? CODE_PRD_MARK { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>货品代号（条码解析）</summary>
    public string? CODE_PRD_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>容器编号</summary>
    public string? CONTAIN_CODE_BH { get; set; }

    /// <summary>容器出库编号</summary>
    public string? CONTAIN_CODE_CKBH { get; set; }

    /// <summary>容器上架标记</summary>
    public string? CONTAIN_SJ_FLAG { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>分区号</summary>
    public string? FQ_NO { get; set; }

    /// <summary>项次（主键之一）</summary>
    public int? ITM { get; set; }

    /// <summary>交货类型</summary>
    public string? JH_TYPE { get; set; }

    /// <summary>配对项次</summary>
    public int? MATCH_ITM { get; set; }

    /// <summary>包装号</summary>
    public string? PACKAGE_NO { get; set; }

    /// <summary>工序代号</summary>
    public string? PRC_ID { get; set; }

    /// <summary>箱1原始数量</summary>
    public decimal? QTY_BOX1_ORIG { get; set; }

    /// <summary>不良数量</summary>
    public decimal? QTY_IMPERFECT { get; set; }

    /// <summary>缺货数量</summary>
    public decimal? QTY_MISS { get; set; }

    /// <summary>原始数量</summary>
    public decimal? QTY_ORIG { get; set; }

    /// <summary>货品数量</summary>
    public decimal? QTY_PRD { get; set; }

    /// <summary>扫描数量</summary>
    public decimal? QTY_SCAN { get; set; }

    /// <summary>副单位货品数量</summary>
    public decimal? QTY1_PRD { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>扫描时间</summary>
    public DateTime? SCAN_TIME { get; set; }

    /// <summary>单据类别代号</summary>
    public string? SPC_NO { get; set; }

    /// <summary>类别备注</summary>
    public string? SPC_REM { get; set; }

    /// <summary>校验状态</summary>
    public string? STATUS_JY { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>唯一ID</summary>
    public long? UNIQUE_ID { get; set; }

    /// <summary>录入员</summary>
    public string? USR { get; set; }

    /// <summary>仓库类别</summary>
    public string? WH_SPC { get; set; }

    /// <summary>仓库1</summary>
    public string? WH1 { get; set; }

    /// <summary>仓库2</summary>
    public string? WH2 { get; set; }
}
