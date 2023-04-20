using Microsoft.EntityFrameworkCore;
using mvc1.Models;

namespace mvc1
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
    }
}
