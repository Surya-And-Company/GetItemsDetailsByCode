using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class ItemController : ControllerBase
    {

        private readonly IItemRepository _itemRepository;
        private readonly IThirdPartyRepository _thirdPartyRepository;
        public ItemController(IItemRepository itemRepository, IThirdPartyRepository thirdPartyRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _thirdPartyRepository = thirdPartyRepository ?? throw new ArgumentNullException(nameof(thirdPartyRepository));

        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItem(ItemDTO item)
        {
            await _itemRepository.AddItem(item);
            return Ok();
        }

        [Authorize]
        [HttpGet("{logdate}/{pageSize}/{page}")]
        [ProducesResponseType(typeof(IEnumerable<Log>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItems(DateTime? date, bool? status, int pageSize, int page)
        {
            (IEnumerable<Item> items, long totalRecord) result = await _itemRepository.GetItems(date, status, pageSize, page);
            return Ok(new { logs = result.items, totalRecord = result.totalRecord });
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(Item), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItem(string code)
        {
            if (Request.Headers.TryGetValue("token", out var token))
            {
                var IsValidClient = await _thirdPartyRepository.IsValidAsync(token);
                if (!IsValidClient)
                {
                    return Unauthorized();
                }
                var item = await _itemRepository.GetItem(code);
                return Ok(item);
            }
            return Unauthorized();
        }
    }
}