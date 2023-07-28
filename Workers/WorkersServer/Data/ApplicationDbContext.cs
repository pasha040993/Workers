using Microsoft.EntityFrameworkCore;
using WorkersServer.Models.POCOs;

namespace WorkersServer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
