using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class OrderSendFile
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string  UserNameReceive { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime DateTime { get; set; }
        public AppUser AppUser { get; set; }
    }
}
