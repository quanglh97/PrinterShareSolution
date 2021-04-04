using Microsoft.AspNetCore.Http;
using PrintShareSolution.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintShareSolution.ViewModels.Catalog.OrderSendFile
{
    public class OrderSendFileCreateRequest
    {
        public string MyId { get; set; }
        public string UserReceive { get; set; }
        public string FileName { get; set; }
        public IFormFile ThumbnailFile { get; set; }
    }
}
