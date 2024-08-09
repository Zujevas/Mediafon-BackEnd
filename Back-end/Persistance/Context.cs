using Back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Persistance
{
    public class Context : DbContext
    {
        protected readonly IConfiguration _configuration;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DbConnectionString"));
        }

       // public DbSet<user> Users { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}
