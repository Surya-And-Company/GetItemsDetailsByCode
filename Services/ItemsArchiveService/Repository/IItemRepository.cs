using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.Repository
{
    public interface IItemRepository
    {        

        
        Task AddItem(ItemDTO item , string userId);

        Task<List<GetItemByCodeResponseDTO>> GetItemsByCode(string Code);

        Task<(IEnumerable<Item>, long)> GetItems(DateTime? date, bool? status, int pageSize, int page);

        Task ApproveAndDisapproveItem(string id);

        Task DeleteAndUndeleteImage(string id, string path);
        
    }
}