using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrinterShareSolution.Application.Catalog.HistoryOfUsers;
using PrintShareSolution.ViewModels.Catalog.HistoryOfUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrinterShareSolution.BackendApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryOfUsersController : ControllerBase
    {
        private readonly IHistoryOfUserService _historyOfUserService;

        public HistoryOfUsersController(
            IHistoryOfUserService historyOfUserService)
        {
            _historyOfUserService = historyOfUserService;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetPaging([FromQuery] GetHistoryOfUserPagingRequest request)
        {
            var historyOfUsers = await _historyOfUserService.GetByMyId(request);
            return Ok(historyOfUsers);
        }

        [HttpGet("DateRange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] GetHistoryOfUserByDateRange request)
        {
            var historyOfUsers = await _historyOfUserService.GetByDateRange(request);
            return Ok(historyOfUsers);
        }

        [HttpGet("RefreshHistory/{myId}")]
        public async Task<IActionResult> Get(string myId)
        {
            var affectedResult = await _historyOfUserService.RefreshHistory(myId);
            if (affectedResult == 0)
                return BadRequest();
            return Ok();
        }
    }
}
