using Bookstore.Classes.Strings;
using Bookstore.Interfaces;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;

namespace Bookstore.Classes.ServiceOutputs
{
    /// <summary>
    /// Represents the output display for service operations.
    /// </summary>
    internal class ServiceOutput
    {
        private readonly IServiceManager _serviceManager;
        private readonly StringBuilder _sb;

        public ServiceOutput(IServiceManager serviceManager, StringBuilder sb)
        {
            _serviceManager = serviceManager;
            _sb = sb;
        }

        // Displays the search results for books based on a keyword.
        public void SearchBooksOutput()
        {
            Console.Write("Enter a keyword to search: ");
            string keyword = Console.ReadLine();
            var searchResults = _serviceManager.SearchBooks(keyword);
            if (searchResults != null && searchResults.Any())
            {
                Console.WriteLine("Search results:");
                foreach (var book in searchResults)
                {
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"Price: {book.Price}");
                    Console.WriteLine($"Quantity: {book.Quantity}");
                    Console.WriteLine($"Description: {book.Description}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine(ErrorMessages.NoBooksFound);
            }
        }

        // Adds a new book to the collection.
        public void AddNewBookOutput()
        {
            Console.Write("Title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrEmpty(title) || title.Length > 50)
            {
                Console.WriteLine(ErrorMessages.TitleError);
                return;
            }

            Console.Write("Author: ");
            string author = Console.ReadLine();
            if (string.IsNullOrEmpty(author) || author.Length > 50)
            {
                Console.WriteLine(ErrorMessages.AuthorError);
                return;
            }

            Console.Write("Price: ");
            decimal price;
            if (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
            {
                Console.WriteLine(ErrorMessages.PriceError);
                return;
            }

            Console.Write("Quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity < 0)
            {
                Console.WriteLine(ErrorMessages.QuantityError);
                return;
            }

            Console.Write("Description: ");
            string description = Console.ReadLine();

            _serviceManager.AddNewBook(title, author, price, quantity, description);
            Console.Write(SucceedMessages.NewBookAdded);
        }

        // Displays the list of books.
        public void DisplayBooksOutput()
        {
            _serviceManager.DisplayBooks(_sb);
            Console.WriteLine(_sb.ToString());
        }

        // Calculates and displays the total value of all books in the collection.
        public void CalculateTotalValueOutput()
        {
            _serviceManager.CalculateTotalValue();
            Console.WriteLine(_serviceManager.CalculateTotalValue());
        }

        // Applies discounts to books.
        public void DiscountOutput()
        {
            _serviceManager.ApplyDiscounts();
            Console.WriteLine(SucceedMessages.AppliedDiscounts);
        }

        // Saves the changes made to the book collection.
        public void SaveOutput()
        {
            _serviceManager.Save();
            Console.WriteLine(SucceedMessages.SavesChanged);
        }
    }
}
