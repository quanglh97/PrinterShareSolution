using PrintShareSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class HistoryOfUser
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ReceiveId { get; set; }
        public int PrinterId { get; set; }
        public string FileName { get; set; }
        public int Pages { get; set; }
        public ActionHistory ActionHistory { get; set; }
        public DateTime DateTime { get; set; }
        public AppUser AppUser { get; set; }
    }
}
