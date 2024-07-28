using Microsoft.EntityFrameworkCore;
using RESTful.API.Models.Entity;

namespace RESTful.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        

        public DbSet<Product> Products { get; set; }
    }
}
