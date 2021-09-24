using System;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;
using ItemsArchiveService.Repository;
using ItemsArchiveService.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }
       
        [Authorize(Role.Admin)]
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await _userRepository.GetUserByIdAsync(id, null));
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public IActionResult GetDetails()
        {
            var user = (User)HttpContext.Items["User"];
            user.Password = null;
            return Ok(user);
        }

        [Authorize(Role.Admin)]
        [HttpGet]
        [Route("{mobile}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByMobileNo(string mobile)
        {
            return Ok(await _userRepository.GetUserByMobileNoAsync(mobile));
        }


        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateInfo(UpdateUserDTO user)
        {
            var previousInfo = (User)HttpContext.Items["User"];
            await _userRepository.UpdateUserAsync(user, previousInfo);
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateByAdmin(CreateUserDTO user, [FromRoute] string id)
        {
            await _userRepository.UpdateUserAsync(user, id);
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPost]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(CreateUserDTO user)
        {
            await _userRepository.AddUserAsync(user);
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok();
        }

        [Authorize(Role.Admin)]
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UnDelete([FromRoute] string id)
        {
            await _userRepository.UnDeleteUserAsync(id);
            return Ok();
        }
    }
}