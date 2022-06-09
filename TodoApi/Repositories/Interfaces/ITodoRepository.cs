using TodoApi.Entities;

namespace TodoApi.Repositories.Interfaces
{
    public interface ITodoRepository
    {
        Task CreateAsync(Todo todo);
        Task UpdateAsync(Todo todo);
        Task Delete(Todo todo);
        Task<Todo> GetByIdAsync(int id, int userId);
        Task<IEnumerable<Todo>> GetAllAsync(int userId);
        Task<IEnumerable<Todo>> GetAllDoneAsync(int userId);
        Task<IEnumerable<Todo>> GetAllUndoneAsync(int userId);
    }
}
