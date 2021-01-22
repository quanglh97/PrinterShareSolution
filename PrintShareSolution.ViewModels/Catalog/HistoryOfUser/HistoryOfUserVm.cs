using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.HistoryOfUser
{
    public class HistoryOfUserVm
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }
        public string FileName { get; set; }
        public ActionHistory ActionHistory { get; set; }
        public DateTime DateTime { get; set; }
    }
}
