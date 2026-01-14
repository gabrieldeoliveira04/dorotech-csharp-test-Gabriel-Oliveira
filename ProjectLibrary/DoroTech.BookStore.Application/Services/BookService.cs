using DoroTech.BookStore.Domain.Interfaces;
using DoroTech.BookStore.Domain.Entities;
using DoroTech.BookStore.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace DoroTech.BookStore.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<BookService> _logger;

        public BookService(
        IBookRepository repository,
        ILogger<BookService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // Lista livros com paginação e filtro por título (parcial, case-insensitive)
        public async Task<PagedResult<BookResponse>> GetAllAsync(
     int page,
     int pageSize,
     string? title)
        {
            _logger.LogInformation(
                "Buscando livros | Página: {Page}, PageSize: {PageSize}, Filtro: {Title}",
                page, pageSize, title);

            var totalItems = await _repository.CountAsync(title);
            var books = await _repository.GetAllAsync(page, pageSize, title);

            return new PagedResult<BookResponse>
            {
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                Items = books.Select(MapToResponse)
            };
        }



        // Busca por ID
        public async Task<BookResponse?> GetByIdAsync(Guid id)
        {
            var book = await _repository.GetByIdAsync(id);
            return book == null ? null : MapToResponse(book);
        }

        // Criação de livro
        public async Task<Guid> CreateAsync(
        string title,
        string author,
        decimal price,
        int stock)
        {
            _logger.LogInformation(
                "Tentativa de criação de livro: {Title} - {Author}",
                title, author);

            var exists = await _repository.ExistsAsync(title, author);

            if (exists)
            {
                _logger.LogWarning(
                    "Livro já existente: {Title} - {Author}",
                    title, author);

                throw new InvalidOperationException("Livro já cadastrado.");
            }

            var book = new Book(title, author, price, stock);
            await _repository.AddAsync(book);

            _logger.LogInformation(
                "Livro criado com sucesso. Id: {BookId}",
                book.Id);

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
            _logger.LogInformation("Atualizando livro | Id: {BookId}", id);
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogWarning("Livro não encontrado| Id: {BookId}", id);
                return false;
            }
            book.Update(title, author, price, stock);
            await _repository.UpdateAsync(book);

            _logger.LogInformation("Livro atualizado com sucesso | Id: {BookId}", id);
            return true;
        }

        // Exclusão
        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Excluindo livro| Id: {BookId}", id);
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
            {
                _logger.LogWarning("Livro não encontrado | Id: {BookId}", id);
                return false;
            }
            _logger.LogInformation("Livro deletado com sucesso | Id: {BookId}", id);
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


