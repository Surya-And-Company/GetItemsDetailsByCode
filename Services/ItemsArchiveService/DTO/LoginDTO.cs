using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.CustomValidations;

namespace ItemsArchiveService.DTO
{
    public class LoginDTO
    {
        [Required]
        [IsValidMobileNo]
        public string MobileNo {get;set;}

        [Required]
        [StringLength(15, MinimumLength = 8)]
        public string Password {get;set;}
    }
}