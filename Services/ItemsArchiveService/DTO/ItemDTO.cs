using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ItemsArchiveService.DTO
{
    public class ItemDTO
    {
        [Required]
        public string Code { get; set; }
        
        public string Brand { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]        
        public decimal SellingPrice { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}