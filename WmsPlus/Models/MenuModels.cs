namespace WmsPlus.Models
{
    public class MenuGroup
    {
        public string Key { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Title { get; set; } = "";
        public List<SubMenuItem> Children { get; set; } = new();
    }

    public class ThirdLevelMenuItem
    {
        public string Title { get; set; } = "";
        public bool IsCategoryHeader { get; set; } = false;
        public string Icon { get; set; } = "";
    }

    public class SubMenuItem
    {
        public string Icon { get; set; } = "";
        public string Title { get; set; } = "";
        public List<ThirdLevelMenuItem> Children { get; set; } = new();
    }
}
