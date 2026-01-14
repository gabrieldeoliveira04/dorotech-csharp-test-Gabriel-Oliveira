namespace DoroTech.BookStore.Application.DTOs
{
    public class PagedResult<T>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
    }
}
