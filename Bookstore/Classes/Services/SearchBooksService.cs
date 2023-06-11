using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Represents a service for searching books in the bookstore.
    /// </summary>
    public class SearchBooksService
    {
        private readonly FileManager _fileManager;
        private List<Book> _books = new List<Book>();

        // Initializes a new instance of the SearchBooksService class with the provided FileManager dependency.
        public SearchBooksService(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        // Searches for books in the bookstore based on the given keyword.
        public List<Book> SearchBooks(string keyword)
        {
            // Read the bookstore data from the file and retrieve the list of books
            var bookStoreData = _fileManager.ReadFromJson<BookStoreData>();
            _books = bookStoreData?.Books;

            // Create a StringBuilder to store search results
            StringBuilder sb = new StringBuilder();

            if (_books != null && _books.Any())
            {
                // Perform case-insensitive search on book titles and authors using LINQ
                var searchKeyword = _books.Where(book =>
                    book.Title.ToLower().Contains(keyword.ToLower()) ||
                    book.Author.ToLower().Contains(keyword.ToLower())
                ).ToList();

                return searchKeyword;
            }
            else
            {
                // Return null if there are no books in the bookstore
                return null;
            }
        }
    }
}
