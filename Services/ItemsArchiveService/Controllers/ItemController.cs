using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using ItemsArchiveService.Repository;
using ItemsArchiveService.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
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

            var user = (User)HttpContext.Items["User"];
            await _itemRepository.AddItem(item, user.Id);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof((IEnumerable<Item> items, long totalRecord)), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(long), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItems(GetItemsDTO getItemDto)
        {
            (IEnumerable<Item> items, long totalRecord) result = await _itemRepository.GetItems(getItemDto.Date, getItemDto.Status, getItemDto.PageSize, getItemDto.Page);
            return Ok(new { items = result.items, totalRecord = result.totalRecord });
        }

        [HttpGet("{code}")]
        [ProducesResponseType(typeof(List<GetItemByCodeResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetItemsByCode(string code)
        {
            if (Request.Headers.TryGetValue("token", out var token))
            {
                var IsValidClient = await _thirdPartyRepository.IsValidAsync(token);
                if (!IsValidClient)
                {
                    return Unauthorized();
                }
                var items = await _itemRepository.GetItemsByCode(code);
                return Ok(items);
            }
            return Unauthorized();
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAndUndeleteImage(DeleteUndeleteImageDTO dto)
        {
            await _itemRepository.DeleteAndUndeleteImage(dto.ItemId, dto.Path);
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ApproveAndDisapproveItem([FromRoute]string id)
        {
            await _itemRepository.ApproveAndDisapproveItem(id);
            return Ok();
        }
    }
}