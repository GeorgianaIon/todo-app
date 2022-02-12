using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Persistence
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = Todos.db");
        }
    }
}