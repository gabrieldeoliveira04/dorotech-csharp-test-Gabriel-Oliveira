namespace DoroTech.BookStore.Application.DTOs
{
    public record BookRequest(
        string Title,
        string Author,
        decimal Price,
        int Stock
    );
}
