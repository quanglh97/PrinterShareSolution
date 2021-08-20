using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using PrinterShareSolution.Application.Catalog.HistoryOfUsers;
using PrinterShareSolution.Application.Catalog.Update;
using PrintShareSolution.ViewModels.Catalog.HistoryOfUser;
using PrintShareSolution.ViewModels.Catalog.Update;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterShareSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdateVersionService _updateService;
        private readonly string  _rootPath;

        public UpdateController(IUpdateVersionService updateService, IWebHostEnvironment webHostEnvironment)
        {
            _updateService = updateService;
            _rootPath = webHostEnvironment.WebRootPath;
        }

        //[HttpGet("pagingFileUpdate")]
        //public async Task<IActionResult> GetPagingFileUpdate([FromQuery] GetHistoryOfUserPagingRequest request)
        //{
        //    var historyOfUsers = await _updateService.GetByMyId(request);
        //    return Ok(historyOfUsers);
        //}

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> create([FromForm] UpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resuft = await _updateService.Create(request);
            if (resuft == 0)
                return BadRequest();

            //var updateFile = await _updateService.GetById(orderPrintFileId);

            //return CreatedAtAction(nameof(GetById), new { id = orderPrintFileId }, orderPrintFile);
            return Ok();
        }
        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ListFileUpdate")]
        public async Task<IActionResult> GetListFileUpdate([FromQuery] GetListUpdateRequest request)
        {
            var products = await _updateService.GetListFileUpdate(request);
            return Ok(products);
        }


        [HttpPost("download")]
        public FileContentResult[] DownloadFile(Dictionary<string ,string> filesInfo)
        {
            FileContentResult[] files = new FileContentResult[filesInfo.Count];
            int count = 0;
            foreach (KeyValuePair<string, string> fileInfo in filesInfo)
            {
                var file = String.Concat(_rootPath, fileInfo.Value);
                //var fisicalFile = PhysicalFile(file, MimeTypes.GetMimeType(file), Path.GetFileName(file));
                //file = "E:\\CodeVisual_C#\\PrinterShareSolution\\PrinterShareSolution.BackendApi\\wwwroot\\update-folder\\3\\ec5678e0-4232-4b1f-83a8-cbdadc4817b9.exe";
                byte[] bytes = System.IO.File.ReadAllBytes(file);
              
                var test = File(bytes, "application/octet-stream", fileInfo.Key);
                files[count] = test;
                count++;
            }
            return files;
        }

        [HttpPost("download2")]
        public PhysicalFileResult[] DownloadFile2(Dictionary<string, string> filesInfo)
        {
            PhysicalFileResult[] files = new PhysicalFileResult[filesInfo.Count];
            int count = 0;
            foreach (KeyValuePair<string, string> fileInfo in filesInfo)
            {
                var file = String.Concat(_rootPath, fileInfo.Value);
                var fisicalFile = PhysicalFile(file, MimeTypes.GetMimeType(file), Path.GetFileName(file));
                files[count] = fisicalFile;
                count++;
            }
            return files;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{version}")]
        //[Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] float version, CommitUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _updateService.CommitUpdate(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
