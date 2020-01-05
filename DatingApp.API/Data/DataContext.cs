using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
            
        }

// Virualt for lazy loading
        public virtual DbSet<Value> Values { get; set; }
    }
}