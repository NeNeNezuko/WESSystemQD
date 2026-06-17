namespace WmsPlus.Models
{
    public class ProductCodeSettingModel
    {
        public int Seq { get; set; }
        public string PrdNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Snm { get; set; } = "";
        public string Idx1 { get; set; } = "";
        public string Ut { get; set; } = "";
        public string Ut1 { get; set; } = "";
        public string Spc { get; set; } = "";
        public string Knd { get; set; } = "";
        public string CwxzNo { get; set; } = "";
        public string Dep { get; set; } = "";
        public string DepName { get; set; } = "";
        public string Pkg1Ut { get; set; } = "";
        public string Pkg1Qty { get; set; } = "";
        public string Pkg2Ut { get; set; } = "";
        public string Pkg2Qty { get; set; } = "";
        public DateTime? NouseDd { get; set; }
    }

    public class ProductCodeSettingQuery
    {
        public string PrdNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Idx1 { get; set; } = "";
        public string Dep { get; set; } = "";
    }

    /// <summary>货品代号设定-新增表单模型</summary>
    public class ProductCodeSettingAddModel
    {
        // ===== 一般资料 =====
        public string PrdNo { get; set; } = "";           // 物料编码(主键)
        public string Name { get; set; } = "";             // 货品名称
        public string Snm { get; set; } = "";              // 简称
        public string NameEng { get; set; } = "";          // 英文名称
        public string Spc { get; set; } = "";              // 货品规格
        public string UsrWh { get; set; } = "";            // 仓管人员
        public bool ChkBat { get; set; }                   // 批号管制
        public bool ChkNum { get; set; }                   // 序列号管制

        // 类别
        public string Knd { get; set; } = "";              // 大类
        public string Idx1 { get; set; } = "";             // 所属中类

        // 单位
        public string Ut { get; set; } = "";               // 主单位
        public string Ut1 { get; set; } = "";              // 副单位

        // 其他
        public string Rem { get; set; } = "";              // 摘要

        // ===== 其他资料 =====
        public string Wh { get; set; } = "";               // 预设仓库
        public string Chuw { get; set; } = "";             // 预设储位
        public string Dep { get; set; } = "";              // 所属部门
        public string ValidId { get; set; } = "";          // 有效期计算方式
        public int? ValidDays { get; set; }                // 有效天数
        public DateTime? StartDD { get; set; }             // 货品创建日
        public DateTime? NouseDD { get; set; }             // 货品停用日期
        public string TplNo { get; set; } = "";            // 货品模版
        public string MobId { get; set; } = "";            // 货品特征模版
        public string CfProp { get; set; } = "";           // 存放属性
        public bool NotBarcode { get; set; }               // 非条码货品
        public bool AllowShqFh { get; set; }               // 允许从收货区直接发货
        public string CwctrlId { get; set; } = "";         // 货品受储位管制
        public string CwxzNo { get; set; } = "";           // 所属储位性质

        // 货品超交率
        public decimal RtoPc { get; set; }                 // 进货超交率(%)
        public decimal RtoMm { get; set; }                 // 生产超交率(%)
        public decimal RtoTb { get; set; }                 // 托工超交率(%)
        public decimal RtoSa { get; set; }                 // 销货超交率(%)

        // ===== 包装资料 =====
        // 库存包装资料
        public string Pk2Ut { get; set; } = "";            // 包装(一)单位
        public int? Pk2Qty { get; set; }                   // 包装(一)数量
        public string Pk3Ut { get; set; } = "";            // 包装(二)单位
        public int? Pk3Qty { get; set; }                   // 包装(二)数量
        public decimal QtyWeight { get; set; }             // 货品单重
        public string UnitWeight { get; set; } = "";       // 货品单重单位
        public string MlUt { get; set; } = "";             // 领料单位

        // 级易包装资料
        public string PakUnit { get; set; } = "";          // 包装单位
        public decimal PakExc { get; set; }                // 换算
        public decimal PakNw { get; set; }                 // 包装净重
        public string PakWeightUnit { get; set; } = "";    // 净重单位
        public decimal PakGw { get; set; }                 // 包装毛重
        public string PakMeast { get; set; } = "";         // 包装大小
        public string PakMeastUnit { get; set; } = "";     // 大小单位

        // 箱码信息
        public string EffectId { get; set; } = "";         // 箱码衍生生效方式

        // ===== 品质检验 =====
        public bool TyIn { get; set; }                     // 入库检验
        public bool TyOut { get; set; }                    // 出库检验
        public bool TyStock { get; set; }                  // 库存检验
        public int TyInr { get; set; } = 30;               // 仓库检验周期(天)
    }
}
