
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class PrinterVm
    {
        public int Id { set; get; }
        public string myId { get; set; }
        public string Name { set; get; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Status Status { set; get; }
    }
}