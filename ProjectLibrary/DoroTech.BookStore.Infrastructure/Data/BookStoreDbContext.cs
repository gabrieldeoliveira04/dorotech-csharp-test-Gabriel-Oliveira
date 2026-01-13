using Microsoft.EntityFrameworkCore;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Infrastructure.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
            : base(options) { }

        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
            .HasIndex(b => new { b.Title, b.Author })
            .IsUnique();
        }
    }
}
