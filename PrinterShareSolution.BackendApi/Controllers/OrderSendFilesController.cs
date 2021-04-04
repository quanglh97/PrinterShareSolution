using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrinterShareSolution.Application.Catalog.OrderSendFiles;
using PrintShareSolution.ViewModels.Catalog.OrderSendFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterShareSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSendFilesController : ControllerBase
    {
        private readonly IOrderSendFileService _orderSendFileService;

        public OrderSendFilesController(IOrderSendFileService orderSendFileService)
        {
            _orderSendFileService = orderSendFileService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orderSendFile = await _orderSendFileService.GetById(id);
            if (orderSendFile == null)
                return BadRequest("Cannot find this orderSendFile");
            return Ok(orderSendFile);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] OrderSendFileCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderSendFileId = await _orderSendFileService.Create(request);
            if (orderSendFileId == 0)
                return BadRequest();

            var orderSendFile = await _orderSendFileService.GetById(orderSendFileId);

            return CreatedAtAction(nameof(GetById), new { id = orderSendFileId }, orderSendFile);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] OrderSendFileDeleteRequest request)
        {
            var affectedResult = await _orderSendFileService.Delete(request);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("Command")]
        public async Task<IActionResult> GetCommandPaging([FromQuery] GetOrderSendFilePagingRequest request)
        {
            var products = await _orderSendFileService.GetByMyId(request);
            return Ok(products);
        }
    }

}
