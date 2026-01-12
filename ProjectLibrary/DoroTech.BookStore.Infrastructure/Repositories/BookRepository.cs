using Microsoft.EntityFrameworkCore;
using DoroTech.BookStore.Domain.Interfaces;
using DoroTech.BookStore.Domain.Entities;
using DoroTech.BookStore.Infrastructure.Data;

namespace DoroTech.BookStore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreDbContext _context;

        public BookRepository(BookStoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(
            int page,
            int pageSize,
            string? title)
        {
            var query = _context.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                var normalizedTitle = title.ToLower();

                query = query.Where(b =>
                    b.Title.ToLower().Contains(normalizedTitle));
            }

            return await query
                .OrderBy(b => b.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<Book?> GetByIdAsync(Guid id)
        {
            return _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public Task<Book?> GetByTitleAsync(string title)
        {
            var normalizedTitle = title.ToLower();

            return _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(b =>
                    b.Title.ToLower().Contains(normalizedTitle));
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }
}

