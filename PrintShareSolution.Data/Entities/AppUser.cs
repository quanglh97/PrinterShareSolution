using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
     
        public List<BlockList> BlockIds { get; set; }
        public List<OrderPrintFile> OrderPrintFiles { get; set; }
        public List<ListPrinterOfUser> ListPrinterOfUsers { get; set; }
    }
}
