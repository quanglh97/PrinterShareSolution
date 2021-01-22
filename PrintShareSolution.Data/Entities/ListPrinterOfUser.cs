using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class ListPrinterOfUser
    {
        //public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }

        public AppUser AppUser { get; set; }
        public Printer Printer {get; set;}
    }
}
