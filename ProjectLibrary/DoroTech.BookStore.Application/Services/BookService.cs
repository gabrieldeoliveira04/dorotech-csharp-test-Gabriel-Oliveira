using DoroTech.BookStore.Application.Interfaces;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync(
            int page,
            int pageSize,
            string? title)
        {
            return await _repository.GetAllAsync(page, pageSize, title);
        }

        public async Task<Book?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(string title, string author, decimal price, int stock)
        {
            var existing = await _repository.GetByTitleAsync(title);

            if (existing != null)
                throw new InvalidOperationException("Livro já cadastrado.");

            var book = new Book(title, author, price, stock);
            await _repository.AddAsync(book);
        }

        public async Task UpdateAsync(Guid id, string title, string author, decimal price, int stock)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                throw new KeyNotFoundException("Livro não encontrado.");

            book.Update(title, author, price, stock);
            await _repository.UpdateAsync(book);
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                throw new KeyNotFoundException("Livro não encontrado.");

            await _repository.DeleteAsync(book);
        }
    }
}
