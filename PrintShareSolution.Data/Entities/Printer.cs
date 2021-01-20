using PrintShareSolution.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class Printer
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public Status Status { set; get; }
        public List<ListPrinterOfUser> ListPrinterOfUsers { get; set; }
        public List<OrderPrintFile> OrderPrintFiles { get; set; }


    }
}   
