using BGMaterial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGMaterial.Persistence.Context
{
    public class MaterialContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MaterialDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
