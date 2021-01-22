using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.ListPrinterOfUser
{
    public class ListPrinterOfUserVm
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public int PrinterId { get; set; }
        public string Name { get; set; }
    }
}
