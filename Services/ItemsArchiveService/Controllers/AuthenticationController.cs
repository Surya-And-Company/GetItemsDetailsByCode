using System;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Repository;
using ItemsArchiveService.Utility;
using Microsoft.AspNetCore.Mvc;

namespace ItemsArchiveService.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        private readonly IJwtUtils _jwtUtils;

        public AuthenticationController(IUserRepository userRepository, IPasswordHasher hasher, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
            _jwtUtils = jwtUtils ?? throw new ArgumentNullException(nameof(jwtUtils));
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _userRepository.GetUserByMobileNoAsync(login.MobileNo);

            if (user == null)
            {
                return BadRequest(new { Message = "Username or password not matching!" });
            }
            else if (user.IsDeleted)
            {
                return BadRequest(new { Message = "Your account locked!" });
            }
            else if (!_hasher.Check(user.Password, login.Password))
            {
                return BadRequest(new { Message = "Username or password not matching!" });
            }

            (string token, DateTime expireDate) = _jwtUtils.GenerateJwtToken(user);

            return Ok(new LoginResponseDTO() { Name = user.Name, Token = token, ExpireDate = expireDate,  ProfileImage = user.ProfileImage });

        }
    }
}