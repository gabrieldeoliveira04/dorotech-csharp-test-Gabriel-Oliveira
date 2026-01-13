using DoroTech.BookStore.Infrastructure.Data;
using DoroTech.BookStore.Domain.Entities;

public static class DatabaseSeeder
{
    public static void Seed(BookStoreDbContext context)
    {
        if (context.Books.Any())
            return;

        context.Books.AddRange(
            new Book("Clean Code", "Robert C. Martin", 99.90m, 10),
            new Book("Domain-Driven Design", "Eric Evans", 120.00m, 5),
            new Book("The Pragmatic Programmer", "Andrew Hunt", 89.90m, 8)
        );

        context.SaveChanges();
    }
}
