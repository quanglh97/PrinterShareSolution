using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderSendFile
{
    public class GetOrderSendFilePagingRequest : PagingRequestBase
    {
        public string MyId { get; set; }
        // public int  PrinterId { get; set; }

    }
}
