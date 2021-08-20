using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Common
{
    public class PageUpdateResult<T> : PagedResultBase
    {
        public string currentVersion { set; get; }
        public List<T> Items { set; get; }
    }

}
