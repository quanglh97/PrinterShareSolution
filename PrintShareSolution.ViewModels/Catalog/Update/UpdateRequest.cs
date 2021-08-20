using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;


namespace PrintShareSolution.ViewModels.Catalog.Update
{
    public class UpdateRequest
    {
        public string adminId { get; set; }
        public string version { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<string> paths { get; set; }
    }
}
