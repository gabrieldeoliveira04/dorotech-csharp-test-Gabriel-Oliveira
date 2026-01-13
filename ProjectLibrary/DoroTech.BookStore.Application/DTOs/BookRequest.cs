using System.ComponentModel.DataAnnotations;

namespace DoroTech.BookStore.Application.DTOs
{
    public class BookRequest
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 150 caracteres.")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 100 caracteres.")]
        public string Author { get; set; } = default!;

        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Stock { get; set; }
    }

}

