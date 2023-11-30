using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebChild.Areas.Identity.Data;
using WebChild.Models;

namespace WebChild.Data;

public class AppDbContext:IdentityDbContext<AppUser>
{
      public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
        
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Product_Order> Product_Orders { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
       
        public DbSet<Status> StatusEnumerable { get; set; } = null!;
        
}

public class ApplicationUserEntityConfiguration:IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
    }
    
}