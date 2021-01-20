using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class BlockList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public Guid UserBlockedId { get; set; }

        public string BlackListFilePath { get; set; }
        public AppUser AppUser { set; get; }
    }
}
