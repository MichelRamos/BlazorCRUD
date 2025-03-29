using Microsoft.EntityFrameworkCore;
using MyApplication.Components.Data;
using MyApplication.Components.Domain;

namespace MyApplication.Components.Service
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserContract> CreateUserAsync(CreateUserContract createUser)
        {
            var User = new User
            {
                Id = Guid.NewGuid(),
                UserName = createUser.UserName,
                Password = createUser.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return new UserContract
            {
                Id = User.Id,
                UserName = User.UserName,
                Password = User.Password,
                CreatedAt = User.CreatedAt,
                UpdatedAt = User.UpdatedAt
            };
        }

        public async Task<IEnumerable<UserContract>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserContract
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<UserContract> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return new UserContract
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<UserContract> UpdateUserAsync(UpdateUserContract updateUser)
        {
            var user = await _context.Users.FindAsync(updateUser.Id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            user.UserName = updateUser.UserName;
            user.Password = updateUser.Password;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return new UserContract
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
