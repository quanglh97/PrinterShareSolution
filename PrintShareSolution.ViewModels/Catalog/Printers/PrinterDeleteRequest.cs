using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class PrinterDeleteRequest
    {
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }
    }
}