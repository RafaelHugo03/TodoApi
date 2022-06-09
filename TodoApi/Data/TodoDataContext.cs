using Microsoft.EntityFrameworkCore;
using TodoApi.Data.Mappings;
using TodoApi.Entities;

namespace TodoApi.Data
{
    public class TodoDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public TodoDataContext(DbContextOptions<TodoDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new TodoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}
