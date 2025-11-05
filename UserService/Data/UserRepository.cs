
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace UserService.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public Task<User> CreateUserAsync(User newUser)
        {
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Task.FromResult(newUser);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => string.Equals(u.Email, email, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}