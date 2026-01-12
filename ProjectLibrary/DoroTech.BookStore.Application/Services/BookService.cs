using DoroTech.BookStore.Application.Interfaces;
using DoroTech.BookStore.Domain.Entities;
using DoroTech.BookStore.Application.DTOs;

namespace DoroTech.BookStore.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookResponse>> GetAllAsync(
    int page,
    int pageSize,
    string? title)
        {
            var books = await _repository.GetAllAsync(page, pageSize, title);

            return books.Select(b => new BookResponse(
                b.Id,
                b.Title,
                b.Author,
                b.Price,
                b.Stock
            ));
        }

        public async Task<BookResponse?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            return book == null
                ? null
                : new BookResponse(book.Id, book.Title, book.Author, book.Price, book.Stock);
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
