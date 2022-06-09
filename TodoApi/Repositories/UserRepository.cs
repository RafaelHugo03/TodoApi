using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Entities;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDataContext _context;

        public UserRepository(TodoDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            var user = await _context
                .Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .Where(x => x.Email == email)
                .FirstOrDefaultAsync();
            return user;
        }
    }
}
