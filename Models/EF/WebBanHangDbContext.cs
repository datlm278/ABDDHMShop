using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BTL_LTWeb.Models.EF
{
    public partial class WebBanHangDbContext : DbContext
    {
        public WebBanHangDbContext()
            : base("name=WebBanHangDbContext")
        {
        }

        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Catalog>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Catalog)
                .HasForeignKey(e => e.catalog_id);

            modelBuilder.Entity<Order>()
                .Property(e => e.data)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Product>()
                .Property(e => e.content)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.image_link)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.user_name)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.user_email)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.payment)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.payment_info)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.message)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Transaction)
                .HasForeignKey(e => e.transaction_id);

            modelBuilder.Entity<User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.user_id);
        }
    }
}
