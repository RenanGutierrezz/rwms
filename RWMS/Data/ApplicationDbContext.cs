using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RWMS.Models.Domain;

namespace RWMS.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // ── Domain DbSets ──────────────────────────────────────────────────────────
    // Each DbSet tells EF Core: "there is a table for this entity"
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<SupplyItem> SupplyItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ── Identity table names ───────────────────────────────────────────────
        builder.Entity<ApplicationUser>().ToTable("Users");
        builder.Entity<ApplicationRole>().ToTable("Roles");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("RoleClaims");
        builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("UserTokens");

        // ── Product ───────────────────────────────────────────────────────────
        builder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Unit)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(p => p.Category)
                .HasMaxLength(100);
        });

        // ── Order ─────────────────────────────────────────────────────────────
        builder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");

            entity.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            entity.Property(o => o.Notes)
                .HasMaxLength(500);

            // Order belongs to a Client (ApplicationUser)
            // Restrict: deleting a user does NOT delete their orders (preserve history)
            entity.HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Store OrderStatus enum as a string in the DB ("Pending", "Accepted", etc.)
            // This makes the database readable without needing a lookup table
            entity.Property(o => o.Status)
                .HasConversion<string>();
        });

        // ── OrderItem ─────────────────────────────────────────────────────────
        builder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItems");

            entity.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            // OrderItem belongs to an Order
            // Cascade: deleting an order deletes its line items (they have no meaning alone)
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderItem references a Product
            // Restrict: deleting a product must NOT delete order history
            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── SupplyItem ────────────────────────────────────────────────────────
        builder.Entity<SupplyItem>(entity =>
        {
            entity.ToTable("SupplyItems");

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(s => s.Unit)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(s => s.EstimatedUnitCost)
                .HasColumnType("decimal(18,2)");

            entity.Property(s => s.Notes)
                .HasMaxLength(500);

            // Optional link to a Product — nullable FK
            // SetNull: if a product is deactivated, supply items lose the link but are kept
            entity.HasOne(s => s.Product)
                .WithMany(p => p.SupplyItems)
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        });
    }
}
