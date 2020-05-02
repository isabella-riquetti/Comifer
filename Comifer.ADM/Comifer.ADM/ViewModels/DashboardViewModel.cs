namespace Comifer.ADM.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardItemViewModel Customers { get; set; }
        public DashboardItemViewModel Orders { get; set; }
        public DashboardItemViewModel Products { get; set; }
        public DashboardItemViewModel ProductParents { get; set; }
    }

    public class DashboardItemViewModel
    {
        public decimal CurrentValue { get; set; }
        public decimal? Growth { get; set; }
    }
}
