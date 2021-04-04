using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class PrinterUpdateRequest
    {
        public string MyId { get; set; }
        public int PrinterId { get; set; }
        public Status Status { get; set; }
    }
}