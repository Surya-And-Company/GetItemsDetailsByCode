using System.Threading.Tasks;
using ItemsArchiveService.DTO;


namespace ItemsArchiveService.Repository
{
    public interface IThirdPartyRepository
    {
        Task Add(ThirdPartyDTO thirdparty);
        Task Get(string token);
    }
}