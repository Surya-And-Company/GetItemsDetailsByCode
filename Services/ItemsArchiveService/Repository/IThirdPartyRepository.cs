using System.Threading.Tasks;
using ItemsArchiveService.DTO;


namespace ItemsArchiveService.Repository
{
    public interface IThirdPartyRepository
    {
        Task AddAsync(ThirdPartyDTO thirdparty);
        Task<bool> IsValidAsync(string token);
    }
}