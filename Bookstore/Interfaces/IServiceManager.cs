using Bookstore.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Interfaces
{
    /// <summary>
    /// Represents a service manager for managing books.
    /// </summary>
    public interface IServiceManager
    {
        /// <summary>
        /// Displays the list of books.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/> used for displaying the books.</param>
        void DisplayBooks(StringBuilder sb);

        /// <summary>
        /// Searches for books based on the specified keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <returns>A list of <see cref="Book"/> objects matching the search keyword.</returns>
        List<Book> SearchBooks(string keyword);

        /// <summary>
        /// Adds a new book to the collection.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="price">The price of the book.</param>
        /// <param name="quantity">The quantity of the book.</param>
        /// <param name="description">The description of the book.</param>
        void AddNewBook(string title, string author, decimal price, int quantity, string description);

        /// <summary>
        /// Calculates the total value of all books in the collection.
        /// </summary>
        /// <returns>The total value of all books.</returns>
        decimal CalculateTotalValue();

        /// <summary>
        /// Applies discounts to the books based on certain criteria.
        /// </summary>
        void ApplyDiscounts();

        /// <summary>
        /// Saves the changes made to the book collection.
        /// </summary>
        void Save();
    }

}
