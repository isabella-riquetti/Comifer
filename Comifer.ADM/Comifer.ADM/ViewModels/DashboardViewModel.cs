using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comifer.ADM.ViewModels
{
    public class DashboardViewModel
    {
        public DashboardItemViewModel Customer { get; set; }
        public DashboardItemViewModel Product { get; set; }
        public DashboardItemViewModel ProductParent { get; set; }
    }

    public class DashboardItemViewModel
    {
        public decimal CurrentValue { get; set; }
        public decimal Growth { get; set; }
    }
}
