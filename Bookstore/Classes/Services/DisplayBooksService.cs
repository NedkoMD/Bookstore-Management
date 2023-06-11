using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Represents a service for displaying books in the bookstore.
    /// </summary>
    public class DisplayBooksService
    {
        private readonly FileManager _fileManager;
        private List<Book> _books = new List<Book>();

        // Initializes a new instance of the DisplayBooksService class with the provided FileManager dependency.
        public DisplayBooksService(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        // Displays the books in the bookstore.
        public void DisplayBooks(StringBuilder sb)
        {
            // Read the bookstore data from the file and retrieve the list of books
            var bookStoreData = _fileManager.ReadFromJson<BookStoreData>();
            _books = bookStoreData?.Books; // Assign the retrieved books to _books

            if (_books != null && _books.Any()) // Check if _books is not null and has any books
            {
                foreach (var book in _books)
                {
                    sb.AppendLine($"{book.Id} | {book.Title} | {book.Author} | {book.Price} | {book.Quantity} | {book.Description ?? ""}");
                }
            }
            else
            {
                sb.AppendLine(ErrorMessages.NoBooks);
            }
        }
    }
}
