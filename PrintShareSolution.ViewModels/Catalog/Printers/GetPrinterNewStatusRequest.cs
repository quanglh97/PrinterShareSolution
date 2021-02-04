using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Printers
{
    public class GetPrinterNewStatusRequest : PagingRequestBase
    {
        public string MyId { get; set; }
        public List<int> L_PrinterId { get; set; }
    }
}
