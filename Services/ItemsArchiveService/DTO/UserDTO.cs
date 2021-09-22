using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.CustomValidations;
using ItemsArchiveService.Utility;

namespace ItemsArchiveService.DTO
{
    public class UserDTO
    {

        [Required]
        [IsValidMobileNo]
        public string MobileNo {get;set;}

        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name {get;set;}

        [Required]
        [IsValidRole]
        public List<string> Roles {get;set;}
        
    }
}