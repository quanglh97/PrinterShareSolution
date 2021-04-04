using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class PrinterCreateRequest
    {
        public string MyId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
