using ItemsArchiveService.Model;

namespace ItemsArchiveService.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public string ValidateJwtToken(string token);
    }
}