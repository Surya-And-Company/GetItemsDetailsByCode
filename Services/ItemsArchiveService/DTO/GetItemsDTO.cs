using System;
namespace ItemsArchiveService.DTO
{
    public class GetItemsDTO
    {
        public DateTime? Date { get; set; }
        public bool? Status { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}