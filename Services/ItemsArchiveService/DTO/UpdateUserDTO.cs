using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ItemsArchiveService.CustomValidations;
using Microsoft.AspNetCore.Http;

namespace ItemsArchiveService.DTO
{
    public class UpdateUserDTO
    {
        [Required]
        [IsValidMobileNo]
        public string MobileNo {get;set;}
        
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name {get;set;}

        public IFormFile ProfileImage { get; set; }
      
    }
}