using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class BlockList
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string UserBlocked { get; set; }
        public string BlackListFilePath { get; set; }
        public AppUser AppUser { set; get; }
    }
}
