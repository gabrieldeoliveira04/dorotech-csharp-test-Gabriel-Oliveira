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

        // Lista livros com paginação e filtro por título
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

        // Busca por ID
        public async Task<BookResponse?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            return book is null
                ? null
                : new BookResponse(
                    book.Id,
                    book.Title,
                    book.Author,
                    book.Price,
                    book.Stock
                );
        }

        // Busca por título
        public async Task<BookResponse?> GetByTitleAsync(string title)
        {
            var book = await _repository.GetByTitleAsync(title);

            return book is null
                ? null
                : new BookResponse(
                    book.Id,
                    book.Title,
                    book.Author,
                    book.Price,
                    book.Stock
                );
        }

        // Criação de livro (retorna o ID)
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

        // Atualização de livro (true se atualizado, false se não encontrado)
        public async Task<bool> UpdateAsync(
            Guid id,
            string title,
            string author,
            decimal price,
            int stock)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book is null)
                return false;

            book.Update(title, author, price, stock);
            await _repository.UpdateAsync(book);

            return true;
        }

        // Exclusão de livro (true se excluído, false se não encontrado)
        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book is null)
                return false;

            await _repository.DeleteAsync(book);
            return true;
        }
    }
}

