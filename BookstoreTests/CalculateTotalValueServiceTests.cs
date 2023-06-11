using Bookstore.Classes.Services;
using Bookstore.Classes;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using NUnit;
using Bookstore.Classes.Strings;
using Bookstore.Interfaces;
using Newtonsoft.Json;
using System.IO;

namespace BookstoreTests
{
    [TestFixture]
    public class CalculateTotalValueServiceTests
    {
        [Test]
        public void CalculateTotalValue_ReturnsZero_WhenBooksListIsEmpty()
        {
            // Arrange
            var emptyBooksList = new List<Book>();
            var fileManager = new FileManager(Paths.JsonFilePath);
            var service = new CalculateTotalValueService(emptyBooksList, fileManager);

            // Act
            var result = service.CalculateTotalValue();

            // Assert
            // Verifies that the result is zero.
            Assert.AreEqual(0, result);
        }

        [Test]
        public void CalculateTotalValue_WhenBookListIsNull_ReturnsZero()
        {
            // Arrange
            List<Book> books = null;
            var fileManager = new FileManager(Paths.JsonFilePath);
            var calculateTotalValueService = new CalculateTotalValueService(books, fileManager);

            // Act
            decimal totalValue = calculateTotalValueService.CalculateTotalValue();

            // Assert
            // Verifies that the result is zero.
            Assert.AreEqual(0, totalValue);
        }

        [Test]
        public void CalculateTotalValue_WhenBookListIsNotEmpty_ReturnsCorrectTotalValue()
        {
            // Arrange
            var books = new List<Book>
            {
                // Creates a list of books with sample data.
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Price = 10.99m, Quantity = 2 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Price = 15.99m, Quantity = 1 },
                new Book { Id = 3, Title = "Book 3", Author = "Author 3", Price = 7.99m, Quantity = 3 }
            };

            // Creates a temporary JSON file for testing.
            var jsonFilePath = "test.json";
            File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(new BookStoreData { Books = books }));

            try
            {
                var fileManager = new FileManager(jsonFilePath);
                var calculateTotalValueService = new CalculateTotalValueService(books, fileManager);

                // Act
                decimal totalValue = calculateTotalValueService.CalculateTotalValue();

                // Assert
                // Verifies that the calculated total value matches the expected result.
                Assert.AreEqual(10.99m * 2 + 15.99m * 1 + 7.99m * 3, totalValue);
            }
            finally
            {
                // Cleans up the temporary JSON file.
                File.Delete(jsonFilePath);
            }
        }
    }
}
