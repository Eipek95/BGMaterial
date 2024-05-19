using BGMaterial.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BGMaterial.Persistence.Context
{
    public class MaterialContext : DbContext
    {
        public MaterialContext(DbContextOptions<MaterialContext> options) : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
