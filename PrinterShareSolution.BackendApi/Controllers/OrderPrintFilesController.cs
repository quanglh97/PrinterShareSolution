using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrinterShareSolution.Application.Catalog.OrderPrinterFiles;
using PrintShareSolution.ViewModels.Catalog.OrderPrintFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterShareSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPrintFilesController : ControllerBase
    {
        private readonly IOrderPrintFileService _orderPrintFileService;

        public OrderPrintFilesController(IOrderPrintFileService orderPrintFileService)
        {
            _orderPrintFileService = orderPrintFileService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] OrderPrintFileCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderPrintFileId = await _orderPrintFileService.Create(request);
            if (orderPrintFileId == 0)
                return BadRequest();

            var orderPrintFile = await _orderPrintFileService.GetById(orderPrintFileId);

            return CreatedAtAction(nameof(GetById), new { id = orderPrintFileId }, orderPrintFile);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] OrderPrintFileDeleteRequest request)
        {
            var affectedResult = await _orderPrintFileService.Delete(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("Command")]
        public async Task<IActionResult> GetCommandPaging([FromQuery] GetOrderPrintFilePagingRequest request)
        {
            var products = await _orderPrintFileService.GetByMyId(request);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var printer = await _orderPrintFileService.GetById(id);
            if (printer == null)
                return BadRequest("Cannot find printer");
            return Ok(printer);
        }

        [HttpGet("RefreshHistory/{myId}")]
        public async Task<IActionResult> Get(string myId)
        {
            var affectedResult = await _orderPrintFileService.RefreshHistory(myId);
            if (affectedResult == 0)
                return BadRequest(); 
            return Ok();
        }
    }
}
