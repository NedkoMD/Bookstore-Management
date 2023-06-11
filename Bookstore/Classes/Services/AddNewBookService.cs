using Bookstore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Represents a service for adding a new book to the bookstore.
    /// </summary>
    public class AddNewBookService
    {
        private readonly BookStoreData _bookstoreData;
        private readonly IFileManager _fileManager;
        private readonly List<Book> _books;

        // Initializes a new instance of the AddNewBookService class with the provided dependencies.
        public AddNewBookService(BookStoreData bookstoreData, IFileManager fileManager, List<Book> books)
        {
            _bookstoreData = bookstoreData;
            _fileManager = fileManager;
            _books = books;
        }

        // Adds a new book to the bookstore.
        public void AddNewBook(string title, string author, decimal price, int quantity, string description)
        {
            int id;
            if (_bookstoreData.Books.Count > 0)
            {
                // Generate a new ID by incrementing the maximum existing ID
                int maxId = _bookstoreData.Books.Max(book => book.Id);
                id = maxId + 1;
            }
            else
            {
                // Set the ID to 1 if there are no existing books
                id = 1;
            }

            // Create a new Book instance with the provided details
            var newBook = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                Price = price,
                Quantity = quantity,
                Description = description
            };

            // Add the new book to the list of books in the bookstore data
            _bookstoreData.Books.Add(newBook);

            // Save the updated bookstore data to the file using the file manager
            _fileManager.WriteToJson(_bookstoreData);
        }
    }
}
