using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Entities;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDataContext _context;

        public TodoRepository(TodoDataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Todo todo)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Todo>> GetAllAsync(int userId)
        {
            var users = await _context
                .Todos
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<Todo>> GetAllDoneAsync(int userId)
        {
            var users = await _context
                .Todos
                .AsNoTracking()
                .Where(x => x.UserId == userId && x.Done == true)
                .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<Todo>> GetAllUndoneAsync(int userId)
        {
            var users = await _context
               .Todos
               .AsNoTracking()
               .Where(x => x.UserId == userId && x.Done == false)
               .ToListAsync();

            return users;
        }

        public async Task<Todo> GetByIdAsync(int id, int userId)
        {
            var user = await _context
                .Todos
                .AsNoTracking()
                .Where(x => x.Id == id && x.UserId == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task UpdateAsync(Todo todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
