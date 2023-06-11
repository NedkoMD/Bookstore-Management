using System.Collections.Generic;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Represents a service for calculating the total value of books in the bookstore.
    /// </summary>
    public class CalculateTotalValueService
    {
        private readonly List<Book> _books;
        private readonly FileManager _fileManager;

        // Initializes a new instance of the CalculateTotalValueService class with the provided dependencies.
        public CalculateTotalValueService(List<Book> books, FileManager fileManager)
        {
            _fileManager = fileManager;

            // Read the bookstore data from the file and retrieve the list of books
            var bookStoreData = _fileManager.ReadFromJson<BookStoreData>();
            _books = bookStoreData?.Books;
        }

        // Calculates the total value of all books in the bookstore.
        public decimal CalculateTotalValue()
        {
            decimal totalValue = 0;
            foreach (var book in _books)
            {
                // Multiply the price of each book by its quantity and add it to the total value
                totalValue += book.Price * book.Quantity;
            }
            return totalValue;
        }
    }
}

