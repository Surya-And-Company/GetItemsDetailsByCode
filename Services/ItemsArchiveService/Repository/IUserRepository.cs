using System.Threading.Tasks;
using ItemsArchiveService.DTO;
using ItemsArchiveService.Model;

namespace ItemsArchiveService.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id, bool? isDeleted);

        Task<User> GetUserByMobileNoAsync(string mobileNo);

        Task AddUserAsync(CreateUserDTO user);

        Task UpdateUserAsync(UpdateUserDTO user, User previousDetails);

        Task UpdateUserAsync(CreateUserDTO user, string id);

        Task DeleteUserAsync(string id);

        Task UnDeleteUserAsync(string id);

    }
}