using DoroTech.BookStore.Domain.Interfaces;
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

        // Lista livros com paginação e filtro por título (parcial, case-insensitive)
        public async Task<IEnumerable<BookResponse>> GetAllAsync(
            int page,
            int pageSize,
            string? title)
        {
            var books = await _repository.GetAllAsync(page, pageSize, title);

            return books.Select(MapToResponse);
        }

        // Busca por ID
        public async Task<BookResponse?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book == null ? null : MapToResponse(book);
        }

        // Busca por título (parcial)
        public async Task<BookResponse?> GetByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;

            var book = await _repository.GetByTitleAsync(title);
            return book == null ? null : MapToResponse(book);
        }

        // Criação de livro
        public async Task<Guid> CreateAsync(
            string title,
            string author,
            decimal price,
            int stock)
        {
            var existing = await _repository.GetByTitleAsync(title);

            if (existing != null)
                throw new InvalidOperationException("Livro já cadastrado.");

            var book = new Book(title, author, price, stock);
            await _repository.AddAsync(book);

            return book.Id;
        }

        // Atualização
        public async Task<bool> UpdateAsync(
            Guid id,
            string title,
            string author,
            decimal price,
            int stock)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return false;

            book.Update(title, author, price, stock);
            await _repository.UpdateAsync(book);

            return true;
        }

        // Exclusão
        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return false;

            await _repository.DeleteAsync(book);
            return true;
        }

        // Mapper centralizado (boa prática)
        private static BookResponse MapToResponse(Book book)
        {
            return new BookResponse(
                book.Id,
                book.Title,
                book.Author,
                book.Price,
                book.Stock
            );
        }
    }
}


