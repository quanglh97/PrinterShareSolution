using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.HistoryOfUser
{
    public class GetHistoryOfUserByDateRange
    {
        public string MyId { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
    }
}
