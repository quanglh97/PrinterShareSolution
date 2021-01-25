using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderPrintFile
{
    public class OrderPrintFileVm
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public ActionOrder ActionOrder { get; set; }
        public DateTime DateTime { get; set; }
        public string FilePath { get; set; }

    }
}
