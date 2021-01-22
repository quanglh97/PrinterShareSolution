using PrintShareSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class OrderPrintFile
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public ActionOrder ActionOrder { get; set; }
        public DateTime DateTime { get; set; }

        public Printer Printer { get; set; }
        public AppUser AppUser { get; set; }
    }
}
