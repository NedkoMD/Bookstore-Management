using System;

namespace Bookstore.Classes
{
    /// <summary>
    /// Represents a book.
    /// </summary>
    public class Book
    {
        // Gets or sets the ID of the book.
        public int Id { get; set; }

        // Gets or sets the title of the book.
        public string Title { get; set; }

        // Gets or sets the author of the book.
        public string Author { get; set; }

        // Gets or sets the price of the book.
        public decimal Price { get; set; }

        // Gets or sets the quantity of the book.
        public int Quantity { get; set; }

        // Gets or sets the description of the book.
        public string Description { get; set; }
    }
}
