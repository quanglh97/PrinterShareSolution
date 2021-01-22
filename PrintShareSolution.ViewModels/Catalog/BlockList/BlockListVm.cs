using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.BlockList
{
    public class BlockListVm
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid UserBlockedId { get; set; }
    }
}
