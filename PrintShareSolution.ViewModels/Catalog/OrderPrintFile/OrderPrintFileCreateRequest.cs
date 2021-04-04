using Microsoft.AspNetCore.Http;
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderPrintFile
{
    public class OrderPrintFileCreateRequest
    {
        public string MyId { get; set; }
        public int PrinterId { get; set; }
        public string FileName { get; set; }
        public int Pages { get; set; } //number pages order print of this file
        public IFormFile ThumbnailFile { get; set; }
    }
}
