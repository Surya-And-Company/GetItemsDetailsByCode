using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;


namespace ItemsArchiveService.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = await userRepository.GetUserByIdAsync(userId, false);               
            }
            await _next(context);
        }
    }
}