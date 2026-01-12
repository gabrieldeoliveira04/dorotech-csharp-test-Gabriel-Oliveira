namespace DoroTech.BookStore.Application.DTOs
{
    public record BookResponse(
        Guid Id,
        string Title,
        string Author,
        decimal Price,
        int Stock
    );
}
