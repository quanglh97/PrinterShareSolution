using PrintShareSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.Update
{
    public class GetListUpdateRequest : PagingRequestBase
    {
        public string myId { get; set; }
    }
}
