using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderPrintFile
{
    public class GetOrderPrintFilePagingRequest : PagingRequestBase
    {
        public Guid UserId { get; set; }
        public int  PrinterId { get; set; }
    
    }
}
