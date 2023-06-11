
using Bookstore.Interfaces;
using System.Collections.Generic;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Responsible for saving a list of books to a JSON file using a provided file manager.
    /// </summary>
    public class SaveService
    {
        private List<Book> _books;
        private readonly IFileManager _fileManager;

        public SaveService(IFileManager fileManager)
        {
            _fileManager = fileManager;
            var bookstoreData = fileManager.ReadFromJson<BookStoreData>(); // Read the bookstore data from JSON file using the injected file manager
            _books = bookstoreData.Books; // Set the books list from the loaded bookstore data
        }

        public void Save()
        {
            var bookstoreData = new BookStoreData
            {
                Books = _books
            };

            _fileManager.WriteToJson(bookstoreData); // Write the updated bookstore data to JSON file using the injected file manager
        }
    }
}
