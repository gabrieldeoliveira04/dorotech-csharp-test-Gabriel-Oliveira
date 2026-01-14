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

        public async Task<int> CountAsync(string? title)
        {
            var query = _context.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b =>
                    EF.Functions.ILike(b.Title, $"%{title}%"));
            }

            return await query.CountAsync();
        }


        public async Task<IEnumerable<Book>> GetAllAsync(
    int page,
    int pageSize,
    string? title)
        {
            page = page <= 0 ? 1 : page;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Books.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b =>
                    EF.Functions.ILike(b.Title, $"%{title}%"));
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

        public async Task<bool> ExistsAsync(string title, string author, Guid? ignoreId = null)
        {
            return await _context.Books.AnyAsync(b =>
                b.Title == title &&
                b.Author == author &&
                (!ignoreId.HasValue || b.Id != ignoreId.Value)
            );
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


