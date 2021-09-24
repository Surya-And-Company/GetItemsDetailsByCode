using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.Model;
using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _ilogRepository;
        public LogController(ILogRepository ilogRepository)
        {
            _ilogRepository = ilogRepository ?? throw new ArgumentNullException(nameof(ilogRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateLog([FromBody] Log log)
        {
            await _ilogRepository.CreateLog(log);
            return Ok();
        }

        [HttpGet("{logdate}/{top}", Name = "GetLogsByDate")]
        [ProducesResponseType(typeof(IEnumerable<Log>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Log>>> GetLogsByDate(DateTime logdate, int top)
        {
            return Ok(await _ilogRepository.GetLogs(logdate, top));
        }

    }
}