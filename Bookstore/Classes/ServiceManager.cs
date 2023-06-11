using Bookstore.Classes.Services;
using Bookstore.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Classes
{
    /// <summary>
    /// Represents a service manager that coordinates various book-related operations.
    /// </summary>
    internal class ServiceManager : IServiceManager
    {
        private readonly DisplayBooksService _displayBooksService;
        private readonly SearchBooksService _searchBooksService;
        private readonly AddNewBookService _addNewBookService;
        private readonly CalculateTotalValueService _calculateTotalValueService;
        private readonly DiscountsService _discountsService;
        private readonly SaveService _saveService;

        // Initializes a new instance of the ServiceManager class with the provided services.
        public ServiceManager(DisplayBooksService displayBooksService,
                               SearchBooksService searchBooksService,
                               AddNewBookService addNewBookService,
                               CalculateTotalValueService calculateTotalValueService,
                               DiscountsService discountsService,
                               SaveService saveService)
        {
            _displayBooksService = displayBooksService;
            _searchBooksService = searchBooksService;
            _addNewBookService = addNewBookService;
            _calculateTotalValueService = calculateTotalValueService;
            _discountsService = discountsService;
            _saveService = saveService;
        }

        // Displays the list of books.
        public void DisplayBooks(StringBuilder sb)
        {
            _displayBooksService.DisplayBooks(sb);
        }

        // Searches for books based on a keyword.
        public List<Book> SearchBooks(string keyword)
        {
            return _searchBooksService.SearchBooks(keyword);
        }

        // Adds a new book to the collection.
        public void AddNewBook(string title, string author, decimal price, int quantity, string description)
        {
            _addNewBookService.AddNewBook(title, author, price, quantity, description);
        }

        // Calculates the total value of all books in the collection.
        public decimal CalculateTotalValue()
        {
            return _calculateTotalValueService.CalculateTotalValue();
        }

        // Applies discounts to books.
        public decimal ApplyDiscounts()
        {
            return _discountsService.ApplyDiscount();
        }

        // Saves the changes made to the book collection.
        public void Save()
        {
            _saveService.Save();
        }
    }
}
