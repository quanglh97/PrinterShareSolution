using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.HistoryOfUser
{
    public class HistoryOfUserVm
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public string ReceiveId { get; set; }
        public string ReceiveName { get; set; }
        public int PrinterId { get; set; }
        public string PrinterName { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public int OrderPrintFileId { get; set; }
        public int OrderSendFileId { get; set; }
        public ActionHistory ActionHistory { get; set; }
        public int Pages { get; set; }
        public Result Result { get; set; }
        public DateTime DateTime { get; set; }
    }
}
