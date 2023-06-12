using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Bookstore.Classes.Services;
using Bookstore.Classes;
using Bookstore.Interfaces;

namespace BookstoreTests
{
    [TestFixture]
    public class AddNewBookServiceTests
    {
        private AddNewBookService _addNewBookService;
        private BookStoreData _bookstoreData;
        private Mock<IFileManager> _fileManagerMock;
        private List<Book> _books;

        [SetUp]
        public void Setup()
        {
            // Set up initial objects for the tests
            _books = new List<Book>();
            _bookstoreData = new BookStoreData
            {
                Books = _books
            };
            _fileManagerMock = new Mock<IFileManager>();

            // Create an instance of AddNewBookService for testing
            _addNewBookService = new AddNewBookService(_bookstoreData, _fileManagerMock.Object, _books);
        }

        [Test]
        public void AddNewBook_WhenBookstoreIsEmpty_ShouldSetIdToOne()
        {
            // Arrange
            var expectedId = 1;

            // Act
            _addNewBookService.AddNewBook("Title", "Author", 10.99m, 5, "Description");

            // Assert
            Assert.AreEqual(expectedId, _bookstoreData.Books.First().Id);
        }

        [Test]
        public void AddNewBook_WhenBookstoreIsNotEmpty_ShouldSetIdToIncrementedValue()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "Existing Book", Author = "Existing Author", Price = 9.99m, Quantity = 2, Description = "Existing Description" };
            _bookstoreData.Books.Add(existingBook);
            var expectedId = existingBook.Id + 1;

            // Act
            _addNewBookService.AddNewBook("Title", "Author", 10.99m, 5, "Description");

            // Assert
            Assert.AreEqual(expectedId, _bookstoreData.Books.Last().Id);
        }

        [Test]
        public void AddNewBook_ShouldAddNewBookToBookstoreData()
        {
            // Arrange
            var title = "Title";
            var author = "Author";
            var price = 10.99m;
            var quantity = 5;
            var description = "Description";

            // Act
            _addNewBookService.AddNewBook(title, author, price, quantity, description);

            // Assert
            Assert.AreEqual(1, _bookstoreData.Books.Count);
            var addedBook = _bookstoreData.Books.First();
            Assert.AreEqual(title, addedBook.Title);
            Assert.AreEqual(author, addedBook.Author);
            Assert.AreEqual(price, addedBook.Price);
            Assert.AreEqual(quantity, addedBook.Quantity);
            Assert.AreEqual(description, addedBook.Description);
        }

        [Test]
        public void AddNewBook_ShouldCallWriteToJsonOnFileManager()
        {
            // Arrange
            var title = "Title";
            var author = "Author";
            var price = 10.99m;
            var quantity = 5;
            var description = "Description";

            // Act
            _addNewBookService.AddNewBook(title, author, price, quantity, description);

            // Assert
            _fileManagerMock.Verify(fm => fm.WriteToJson(_bookstoreData), Times.Once);
        }

    }
}