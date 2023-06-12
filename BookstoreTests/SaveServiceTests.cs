using NUnit.Framework;
using Moq;
using Bookstore.Classes.Services;
using Bookstore.Classes;
using Bookstore.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace BookstoreTests
{
    [TestFixture]
    public class SaveServiceTests
    {
        private Mock<IFileManager> _fileManagerMock;
        private SaveService _saveService;

        [SetUp]
        public void Setup()
        {
            _fileManagerMock = new Mock<IFileManager>();
            _saveService = new SaveService(_fileManagerMock.Object);
        }

        [Test]
        public void Save_UpdatesBookstoreDataAndWritesToJsonFile()
        {
            // Arrange
            var books = new List<Book>
            {
                new Book { Title = "Book 1" },
                new Book { Title = "Book 2" }
            };

            // Create a new instance of SaveService with the mock IFileManager object
            _saveService = new SaveService(_fileManagerMock.Object);

            // Set the private _books field in the SaveService instance to the list of books
            SetBooksField(books);

            // Act
            _saveService.Save();

            // Assert
            // Verify that the WriteToJson method of the fileManagerMock is called once with the BookStoreData argument whose Books property matches the list of books
            _fileManagerMock.Verify(fm => fm.WriteToJson(It.Is<BookStoreData>(data => data.Books == books)), Times.Once);
        }

        [Test]
        public void Save_CallsWriteToJsonMethodWithNonNullBookstoreData()
        {
            // Arrange
            // Set the private _books field in the SaveService instance to an empty list
            SetBooksField(new List<Book>());

            // Act
            _saveService.Save();

            // Assert
            // Verify that the WriteToJson method of the fileManagerMock is called once with a non-null BookStoreData argument
            _fileManagerMock.Verify(fm => fm.WriteToJson(It.IsNotNull<BookStoreData>()), Times.Once);
        }

        private void SetBooksField(List<Book> books)
        {
            // Use reflection to access the private _books field in the SaveService instance
            var booksField = typeof(SaveService).GetField("_books", BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the _books field to the provided list of books
            booksField.SetValue(_saveService, books);
        }
    }
}
