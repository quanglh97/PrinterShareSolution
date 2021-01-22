using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.BlockList
{
    public class BlockListCreateRequest
    {
        public Guid UserId { get; set; }
        public Guid UserBlockedId { get; set; }
        public string BlackListFilePath { get; set; }
    }
}
