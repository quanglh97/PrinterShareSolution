using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderPrintFile
{
    public class OrderPrintFileVm
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string Email { get; set; }
        public string ReceiveId { get; set; }
        public string ReceiveName { get; set; }
        public int PrinterId { get; set; }
        public string PrinterName { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int Pages { get; set; }
        public DateTime DateTime { get; set; }
        public string FilePath { get; set; }


    }
}
