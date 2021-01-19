using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.Data.Entities
{
    public class AppRole:IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
