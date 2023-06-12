using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Bookstore.Classes.Services;
using Bookstore.Classes;
using Bookstore.Interfaces;

namespace BookstoreTests
{
    [TestFixture]
    public class DiscountsServiceTests
    {
        private DiscountsService _discountsService;
        private Mock<IFileManager> _fileManagerMock;

        [SetUp]
        public void Setup()
        {
            // Set up the necessary objects for the tests
            _fileManagerMock = new Mock<IFileManager>();
            _discountsService = new DiscountsService(_fileManagerMock.Object);
        }

        [Test]
        public void ApplyDiscount_BooksIsNull_ReturnsZero()
        {
            // Arrange
            _fileManagerMock.Setup(fm => fm.ReadFromJson<BookStoreData>()).Returns(new BookStoreData { Books = null });

            // Act
            decimal result = _discountsService.ApplyDiscount();

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ApplyDiscount_BooksIsEmpty_ReturnsZero()
        {
            // Arrange
            _fileManagerMock.Setup(fm => fm.ReadFromJson<BookStoreData>()).Returns(new BookStoreData { Books = new List<Book>() });

            // Act
            decimal result = _discountsService.ApplyDiscount();

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void ApplyDiscount_Apply5PercentDiscountToBooksUnder15()
        {
            // Arrange
            // Create a list of books with prices below 15
            var books = new List<Book>
            {
                new Book { Price = 10 },
                new Book { Price = 12 },
                new Book { Price = 14 }
            };
            var bookStoreData = new BookStoreData { Books = books };
            _fileManagerMock.Setup(fm => fm.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            decimal result = _discountsService.ApplyDiscount();

            // Assert
            Assert.AreEqual(0, result);
            Assert.AreEqual(9.5m, books[0].Price);
            Assert.AreEqual(11.4m, books[1].Price);
            Assert.AreEqual(13.3m, books[2].Price);
        }

        [Test]
        public void ApplyDiscount_Apply10PercentDiscountToBooksBetween15And25()
        {
            // Arrange
            // Create a list of books with prices between 15 and 25
            var books = new List<Book>
            {
                new Book { Price = 15 },
                new Book { Price = 20 },
                new Book { Price = 25 }
            };
            var bookStoreData = new BookStoreData { Books = books };
            _fileManagerMock.Setup(fm => fm.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            decimal result = _discountsService.ApplyDiscount();

            // Assert
            Assert.AreEqual(0, result);
            Assert.AreEqual(13.5m, books[0].Price);
            Assert.AreEqual(18m, books[1].Price);
            Assert.AreEqual(22.5m, books[2].Price);
        }

        [Test]
        public void ApplyDiscount_Apply15PercentDiscountToBooksAbove25()
        {
            // Arrange
            // Create a list of books with prices above 25
            var books = new List<Book>
            {
                new Book { Price = 30 },
                new Book { Price = 35 },
                new Book { Price = 40 }
            };
            var bookStoreData = new BookStoreData { Books = books };
            _fileManagerMock.Setup(fm => fm.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            decimal result = _discountsService.ApplyDiscount();

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}
