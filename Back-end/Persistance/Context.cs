using Back_end.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_end.Persistance
{
    public class Context : IdentityDbContext
    {
        protected readonly IConfiguration _configuration;

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

       

        public DbSet<Request> Requests { get; set; }
    }
}
