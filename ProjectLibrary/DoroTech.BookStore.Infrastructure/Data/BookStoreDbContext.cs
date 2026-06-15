using Microsoft.EntityFrameworkCore;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Infrastructure.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(
            DbContextOptions<BookStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uuid");

                entity.Property(e => e.Price)
                    .HasColumnType("numeric(18,2)");

                entity.HasIndex(e => new { e.Title, e.Author })
                    .IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}