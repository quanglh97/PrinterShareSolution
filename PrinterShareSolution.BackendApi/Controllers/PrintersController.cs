using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrinterShareSolution.Application.Catalog.Printers;
using PrintShareSolution.ViewModels.Catalog.Printers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterShareSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintersController : ControllerBase
    {
        private readonly IPrinterService _printerService;

        public PrintersController(
            IPrinterService printerService)
        {
            _printerService = printerService;
        }

/*        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Khi bố mày test thì chỉ có thể là Test Ok");
        }*/

        [HttpGet("{printerId}/{userId}")]
        public async Task<IActionResult> GetById(int printerId, Guid userId)
        {
            var printer = await _printerService.GetById(printerId, userId);
            if (printer == null)
                return BadRequest("Cannot find printer");
            return Ok(printer);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] PrinterCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var printerId = await _printerService.Create(request);
            if (printerId == 0)
                return BadRequest();

            var printer = await _printerService.GetById(printerId, request.UserId);

            return CreatedAtAction(nameof(GetById), new { id = printerId }, printer);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] PrinterDeleteRequest request)
        {
            var affectedResult = await _printerService.Delete(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpPut("{printerId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromRoute] int printerId, [FromForm] PrinterUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.PrinterId = printerId;
            var affectedResult = await _printerService.Update(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetStatusPaging([FromQuery] GetPrinterPagingRequest request)
        {
            var printers = await _printerService.GetStatusPaging(request);
            return Ok(printers);
        }
        [HttpGet("keyWord")]
        public async Task<IActionResult> GetKeyWordPaging(string keyWord)
        {
            var printers = await _printerService.GetKeyWordPaging(keyWord);
            return Ok(printers);
        }
    }
}
