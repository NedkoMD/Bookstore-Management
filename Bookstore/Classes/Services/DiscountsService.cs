
using Bookstore.Classes.Strings;
using Bookstore.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bookstore.Classes.Services
{
    /// <summary>
    /// Represents a service for applying discounts to books in a bookstore.
    /// </summary>
    public class DiscountsService
    {
        private readonly IFileManager _fileManager;

        // Initializes a new instance of the DiscountsService class with the provided dependencies.
        public DiscountsService(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public decimal ApplyDiscount()
        {
            var bookStoreData = _fileManager.ReadFromJson<BookStoreData>();
            var books = bookStoreData?.Books;

            decimal discount = 0;

            if(books != null || books.Any())
            {
                foreach (var book in books)
                {
                    if (book.Price < 15)
                    {
                        book.Price *= 0.95m; // 5% discount
                    }
                    else if (book.Price >= 15 && book.Price <= 25)
                    {
                        book.Price *= 0.90m; // 10% discount
                    }
                    else
                    {
                        book.Price *= 0.85m; // 15% discount
                    }
                }

                string updateFile = JsonConvert.SerializeObject(bookStoreData, Formatting.Indented);
                File.WriteAllText(Paths.JsonFilePath, updateFile);

                return discount;
            }
            else
            {
                return 0;
            }
        }
    }
}
