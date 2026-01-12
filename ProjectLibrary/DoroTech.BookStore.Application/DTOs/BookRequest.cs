using System.ComponentModel.DataAnnotations;

namespace DoroTech.BookStore.Application.DTOs
{
    public class BookRequest
    {
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; } = default!;

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Author { get; set; } = default!;

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}

