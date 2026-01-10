namespace DoroTech.BookStore.Domain.Entities
{
    public class Book
    {
        public Guid Id { get; private set; }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }

        protected Book() { } // EF

        public Book(string title, string author, decimal price, int stock)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            Price = price;
            Stock = stock;
        }

        public void Update(string title, string author, decimal price, int stock)
        {
            Title = title;
            Author = author;
            Price = price;
            Stock = stock;
        }
    }
}
