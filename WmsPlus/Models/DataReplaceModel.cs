namespace WmsPlus.Models
{
    /// <summary>
    /// 资料替换 - 数据模型（表格行：原代号 → 新代号的映射）
    /// </summary>
    public class DataReplaceModel
    {
        public string OldCode { get; set; } = "";
        public string NewCode { get; set; } = "";
    }

    /// <summary>
    /// 资料替换 - 查询条件
    /// </summary>
    public class DataReplaceQuery
    {
        /// <summary>基础资料类型: dept/warehouse/midclass/product/mark/customer</summary>
        public string BaseType { get; set; } = "";
        /// <summary>替换类型: new_not_exists/new_exists</summary>
        public string ReplaceType { get; set; } = "";
    }
}
