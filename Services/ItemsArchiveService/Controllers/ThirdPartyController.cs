using System;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class ThirdPartyController :  ControllerBase
    {
        private readonly IThirdPartyRepository _thirdPartyRepository;

        public ThirdPartyController(IThirdPartyRepository thirdPartyRepository)
        {
            _thirdPartyRepository = thirdPartyRepository ?? throw new ArgumentNullException(nameof(thirdPartyRepository));
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public  async Task<IActionResult> Add(ThirdPartyDTO thirdPartyDTO)
        {
            await _thirdPartyRepository.AddAsync(thirdPartyDTO);
            return Ok();
        }
    }
}