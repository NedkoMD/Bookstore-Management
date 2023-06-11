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
                new Book { Id = 1, Title = "Book 1", Author = "Author 1", Price = 10.99m, Quantity = 5 },
                new Book { Id = 2, Title = "Book 2", Author = "Author 2", Price = 12.99m, Quantity = 3 }
            }
            };
            var json = JsonConvert.SerializeObject(bookStoreData);
            File.WriteAllText(Paths.JsonFilePath, json);

            // Act
            _displayBooksService.DisplayBooks(sb);

            // Assert
            var expectedOutput = "1 | Book 1 | Author 1 | 10,99 | 5 | \r\n2 | Book 2 | Author 2 | 12,99 | 3 | \r\n";
            Assert.AreEqual(expectedOutput, sb.ToString());
        }

        [Test]
        public void DisplayBooks_WithNoBooks_ShouldDisplayErrorMessage()
        {
            // Arrange
            var sb = new StringBuilder();
            var bookStoreData = new BookStoreData { Books = new List<Book>() };
            _fileManager.WriteToJson(bookStoreData);

            // Act
            _displayBooksService.DisplayBooks(sb);
            string result = sb.ToString();

            // Assert
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
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result, Contains.Substring(ErrorMessages.NoBooks));
        }

        [Test]
        public void DisplayBooks_WithEmptyJsonFile_ShouldDisplayErrorMessage()
        {
            // Arrange
            var sb = new StringBuilder();
            var bookStoreData = new BookStoreData();
            _fileManager.WriteToJson(bookStoreData);

            // Act
            _displayBooksService.DisplayBooks(sb);
            string result = sb.ToString();

            // Assert
            Assert.That(result, Is.Not.Null.Or.Empty);
            Assert.That(result, Contains.Substring(ErrorMessages.NoBooks));
        }
    }
}

