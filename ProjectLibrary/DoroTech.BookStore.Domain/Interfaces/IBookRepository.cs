using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync(int page, int pageSize, string? title);
        Task<Book?> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(string title, string author, Guid? ignoreId = null);

        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<int> CountAsync(string? title);
    }
}
