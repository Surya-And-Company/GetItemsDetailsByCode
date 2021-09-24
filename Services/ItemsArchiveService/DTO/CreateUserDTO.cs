using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.CustomValidations;

namespace ItemsArchiveService.DTO
{
    public class CreateUserDTO
    {

        [Required]
        [IsValidMobileNo]
        public string MobileNo {get;set;}
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name {get;set;}

        [Required]
        [IsValidRole]
        public Utility.Role Role {get;set;}

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 8 and 15 characters and contain one uppercase letter, one lowercase letter, one digit and one special character!")]
        public string Password {get;set;}
        
    }
}