using System;
using System.ComponentModel.DataAnnotations;

namespace ItemsArchiveService.DTO
{
    public class ThirdPartyDTO
    {
        [Required]
        public string Company { get; set; }

        [Required]
        public DateTime ActivationDate { get; set; }

        [Required]
        public DateTime ExpiresDate { get; set; }

        [Required]
        public long AllowedRequest { get; set; }      

        [Required]
        public string Token { get; set; }
    }
}