using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoroTech.BookStore.Application.Services;
using DoroTech.BookStore.Domain.Entities;
using DoroTech.BookStore.Application.DTOs;

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

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<BookResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? title = null)
        {
            return Ok(await _service.GetAllAsync(page, pageSize, title));
        }


        [HttpGet("{idOrTitle}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdOrTitle(string idOrTitle)
        {
            var book = Guid.TryParse(idOrTitle, out var id)
                ? await _service.GetByIdAsync(id)
                : await _service.GetByTitleAsync(idOrTitle);

            return book == null ? NotFound() : Ok(book);
        }

        [Authorize(Roles = "Admin")]
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
        
        [Authorize(Roles = "Admin")]
        [HttpPut("{idOrTitle}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string idOrTitle, [FromBody] BookRequest request)
        {
            var book = Guid.TryParse(idOrTitle, out var id)
                ? await _service.GetByIdAsync(id)
                : await _service.GetByTitleAsync(idOrTitle);

            if (book == null)
                return NotFound("Livro não encontrado.");

            await _service.UpdateAsync(
                book.Id,
                request.Title,
                request.Author,
                request.Price,
                request.Stock
            );

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{idOrTitle}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string idOrTitle)
        {
            var book = Guid.TryParse(idOrTitle, out var id)
                ? await _service.GetByIdAsync(id)
                : await _service.GetByTitleAsync(idOrTitle);

            if (book == null)
                return NotFound("Livro não encontrado.");

            await _service.DeleteAsync(book.Id);
            return NoContent();
        }

    }
}
