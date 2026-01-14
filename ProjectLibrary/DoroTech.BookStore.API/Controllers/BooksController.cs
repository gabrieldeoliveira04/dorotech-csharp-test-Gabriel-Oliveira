using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DoroTech.BookStore.Application.Services;
using DoroTech.BookStore.Domain.Entities;
using DoroTech.BookStore.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations;

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

        //Lista todos os livros com paginação e filtro
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PagedResult<BookResponse>), StatusCodes.Status200OK)]
        [SwaggerOperation(
        Summary = "Lista livros",
        Description = "Retorna livros paginados, opcionalmente filtrados por título (case-insensitive)."
        )]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? title = null)
        {
            var books = await _service.GetAllAsync(page, pageSize, title);
            return Ok(books);
        }

        //filtra o livro pelo ID
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _service.GetByIdAsync(id);
            return book is null ? NotFound() : Ok(book);
        }

        //Cria um novo livro(somente admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] BookRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var id = await _service.CreateAsync(
                    request.Title,
                    request.Author,
                    request.Price,
                    request.Stock
                );

                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //Atualiza um livro existente (somente admin)
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] BookRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var updated = await _service.UpdateAsync(
                    id,
                    request.Title,
                    request.Author,
                    request.Price,
                    request.Stock
                );

                return updated ? NoContent() : NotFound("Livro não encontrado.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }
        }

        //Deleta um livro pelo ID (somente admin)
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound("Livro não encontrado.");
        }

    }
}
