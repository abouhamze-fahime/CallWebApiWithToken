using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OrderStore.Domain.Models.Users;
using OrderStore.Domain.Models.EshopModel;

#nullable disable

namespace OrderStore.Repository
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Nationalcode)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId)
                    .HasName("PK_CartItems");

                entity.HasIndex(e => e.StockId, "IX_CartItems_ItemId");

                entity.Property(e => e.RowSum).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Product2");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Stock");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK_Carts");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotoalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product2_Categories");

                entity.HasOne(d => d.Stock)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.StockId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product2_Stock");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserName).HasMaxLength(50);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
