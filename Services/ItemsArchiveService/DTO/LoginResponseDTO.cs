using System;

namespace ItemsArchiveService.DTO
{
    public class LoginResponseDTO
    {
        public string Name { get; set; }

        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }

        public string ProfileImage { get; set; }
    }
}