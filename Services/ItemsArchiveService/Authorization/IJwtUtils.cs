using System;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.Authorization
{
    public interface IJwtUtils
    {
        public (string, DateTime) GenerateJwtToken(User user);
        public string ValidateJwtToken(string token);
    }
}