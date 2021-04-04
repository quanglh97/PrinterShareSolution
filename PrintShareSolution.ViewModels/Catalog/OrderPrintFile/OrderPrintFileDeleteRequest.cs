using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderPrintFile
{
    public class OrderPrintFileDeleteRequest
    {
        public string MyId { get; set; }
        public int Id { get; set; }
        public Result Result { get; set; }
    }
}
