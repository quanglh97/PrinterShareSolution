using PrintShareSolution.ViewModels.Common;
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class GetPrinterPagingRequest : PagingRequestBase
    {
        public string MyId { get; set; }
        public Status Status { get; set; }
    }
}
