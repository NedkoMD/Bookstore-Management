using NUnit.Framework;
using System.IO;
using System.Text;
using Bookstore.Classes;
using System.Collections.Generic;
using Bookstore.Classes.Services;
using Newtonsoft.Json;
using Bookstore.Classes.Strings;

namespace BookstoreTests
{
    [TestFixture]
    public class DisplayBooksTests
    {
        private FileManager _fileManager;
        private DisplayBooksService _displayBooksService;

        [SetUp]
        public void Setup()
        {
            // Initializes the FileManager and DisplayBooksService objects.
            _fileManager = new FileManager(Paths.JsonFilePath);
            _displayBooksService = new DisplayBooksService(_fileManager);
        }

        [Test]
        public void DisplayBooks_WithExistingBooks_ShouldDisplayBooks()
        {
            // Arrange
            var sb = new StringBuilder();
            var bookStoreData = new BookStoreData
            {
                Books = new List<Book>
                {
                    // Creates a list of books with sample data.
                    new Book { Id = 1, Title = "Book 1", Author = "Author 1", Price = 10.99m, Quantity = 5 },
                    new Book { Id = 2, Title = "Book 2", Author = "Author 2", Price = 12.99m, Quantity = 3 }
                }
            };
            var json = JsonConvert.SerializeObject(bookStoreData);

            // Writes the JSON data to a file.
            File.WriteAllText(Paths.JsonFilePath, json);

            // Act
            _displayBooksService.DisplayBooks(sb);

            // Assert
            // Verifies that the displayed books match the expected output.
            var expectedOutput = "1 | Book 1 | Author 1 | 10,99 | 5 | \r\n2 | Book 2 | Author 2 | 12,99 | 3 | \r\n";
            Assert.AreEqual(expectedOutput, sb.ToString());
        }

        [Test]
        public void DisplayBooks_WithNoBooks_ShouldDisplayErrorMessage()
        {
            // Arrange
            var sb = new StringBuilder();
            var bookStoreData = new BookStoreData { Books = new List<Book>() };

            // Writes the empty book store data to the JSON file.
            _fileManager.WriteToJson(bookStoreData);

            // Act
            _displayBooksService.DisplayBooks(sb);
            string result = sb.ToString();

            // Assert
            // Verifies that the result is not null or empty and contains the appropriate error message.
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result, Contains.Substring(ErrorMessages.NoBooks));
        }

        [Test]
        public void DisplayBooks_WithNonExistingJsonFile_ShouldDisplayErrorMessage()
        {
            // Arrange
            var sb = new StringBuilder();

            // Act
            _displayBooksService.DisplayBooks(sb);
            string result = sb.ToString();

            // Assert
            // Verifies that the result is not null or empty and contains the appropriate error message.
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result, Contains.Substring(ErrorMessages.NoBooks));
        }

        [Test]
        public void DisplayBooks_WithEmptyJsonFile_ShouldDisplayErrorMessage()
        {
            // Arrange
            var sb = new StringBuilder();
            var bookStoreData = new BookStoreData();

            // Writes the empty book store data to the JSON file.
            _fileManager.WriteToJson(bookStoreData);

            // Act
            _displayBooksService.DisplayBooks(sb);
            string result = sb.ToString();

            // Assert
            // Verifies that the result is not null or empty and contains the appropriate error message.
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result, Contains.Substring(ErrorMessages.NoBooks));
        }
    }
}