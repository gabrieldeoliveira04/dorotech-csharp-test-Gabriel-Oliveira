namespace DoroTech.BookStore.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; } = null!;
        public string Author { get; private set; } = null!;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        protected Book() { } // EF

        public Book(string title, string author, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Título inválido.");
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Autor inválido.");


            if (price <= 0)
                throw new ArgumentException("Preço inválido.");

            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Price = price;
            Stock = stock;
        }

        public void Update(string title, string author, decimal price, int stock)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Título inválido.");

            if (price <= 0)
                throw new ArgumentException("Preço inválido.");

            if (stock < 0)
                throw new ArgumentException("Estoque inválido.");

            Title = title;
            Author = author;
            Price = price;
            Stock = stock;
        }

    }
}
