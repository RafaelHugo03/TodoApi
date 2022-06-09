
using TodoApi.Entities;

namespace TodoApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task DeleteAsync(User user);
        Task<User> GetByEmailAsync(string email);
    }
}
