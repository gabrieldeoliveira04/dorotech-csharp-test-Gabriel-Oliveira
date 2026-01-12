namespace DoroTech.BookStore.Application.DTOs
{
    public class BookResponse
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = default!;
        public string Author { get; init; } = default!;
        public decimal Price { get; init; }
        public int Stock { get; init; }

        public BookResponse(Guid id, string title, string author, decimal price, int stock)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
            Stock = stock;
        }
    }
}

