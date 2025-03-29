using MyApplication.Components.Domain;

namespace MyApplication.Components.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserContract>> GetAllUsersAsync();

        Task<UserContract> GetUserByIdAsync(Guid id);

        Task<UserContract> CreateUserAsync(CreateUserContract createUser);

        Task<UserContract> UpdateUserAsync(UpdateUserContract updateUser);

        Task DeleteUserAsync(Guid id);
    }
}
