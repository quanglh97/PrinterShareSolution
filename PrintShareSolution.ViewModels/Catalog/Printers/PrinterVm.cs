
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class PrinterVm
    {
        public int Id { set; get; }
        public Guid UserId { get; set; }
        public string Name { set; get; }
        public Status Status { set; get; }
    }
}