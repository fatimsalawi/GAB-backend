using GAB.Models;
using Microsoft.EntityFrameworkCore;

namespace GAB.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 👇 Add this so LocationsController compiles
        public DbSet<Location> Locations => Set<Location>();

        public DbSet<MenuItem> MenuItems => Set<MenuItem>();

        public DbSet<ContactMessage> ContactMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ----- locations -----
            modelBuilder.Entity<Location>(e =>
            {
                e.ToTable("locations");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("id");
                e.Property(x => x.Name).HasColumnName("name");
                e.Property(x => x.Address).HasColumnName("address");
                e.Property(x => x.Hours).HasColumnName("hours");
                e.Property(x => x.Maps).HasColumnName("maps");
                e.Property(x => x.CreatedAt).HasColumnName("created_at");
            });

            // ----- menu_items -----
            modelBuilder.Entity<MenuItem>(e =>
            {
                e.ToTable("menu_items");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).HasColumnName("id");
                e.Property(x => x.Name).HasColumnName("name");
                e.Property(x => x.Description).HasColumnName("description");
                e.Property(x => x.Price).HasColumnName("price").HasColumnType("decimal(8,2)");
                e.Property(x => x.Image).HasColumnName("image");
                e.Property(x => x.Category).HasColumnName("category");
                e.Property(x => x.IsVegetarian).HasColumnName("is_vegetarian");
                e.Property(x => x.IsSpicy).HasColumnName("is_spicy");
                e.Property(x => x.IsSignature).HasColumnName("is_signature");
                e.Property(x => x.CreatedAt).HasColumnName("created_at");
            });
        }
    }
}
