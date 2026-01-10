using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoroTech.BookStore.Application.Services;
using DoroTech.BookStore.Domain.Entities;

namespace DoroTech.BookStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _service;

        public BooksController(BookService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todos os livros (acesso público)
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? title = null)
        {
            var books = await _service.GetAllAsync(page, pageSize, title);
            return Ok(books);
        }

        /// <summary>
        /// Obtém um livro por ID (acesso público)
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _service.GetByIdAsync(id);
            return book == null ? NotFound() : Ok(book);
        }

        /// <summary>
        /// Cria um novo livro (somente Admin)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Book request)
        {
            try
            {
                await _service.CreateAsync(
                    request.Title,
                    request.Author,
                    request.Price,
                    request.Stock);

                return CreatedAtAction(nameof(GetAll), null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza um livro (somente Admin)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(Guid id, [FromBody] Book request)
        {
            try
            {
                await _service.UpdateAsync(
                    id,
                    request.Title,
                    request.Author,
                    request.Price,
                    request.Stock);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Remove um livro (somente Admin)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
