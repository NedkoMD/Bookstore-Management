using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Bookstore.Classes.Services;
using Bookstore.Classes;
using Bookstore.Classes.Strings;

namespace BookstoreTests
{
    public interface IFileManagerWrapper
    {
        T ReadFromJson<T>();
    }

    public class FileManagerWrapper : IFileManagerWrapper
    {
        private readonly FileManager _fileManager;

        public FileManagerWrapper(FileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public T ReadFromJson<T>()
        {
            return _fileManager.ReadFromJson<T>();
        }
    }

    [TestFixture]
    public class SearchBooksServiceTests
    {
        private SearchBooksService _searchBooksService;
        private Mock<IFileManagerWrapper> _fileManagerWrapperMock;

        [SetUp]
        public void Setup()
        {
            _fileManagerWrapperMock = new Mock<IFileManagerWrapper>();
            _searchBooksService = new SearchBooksService(new FileManager(Paths.JsonFilePath));
        }

        [Test]
        public void SearchBooks_WhenNoBooksExist_ReturnsNull()
        {
            // Arrange
            var bookStoreData = new BookStoreData { Books = null };
            _fileManagerWrapperMock.Setup(f => f.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            var result = _searchBooksService.SearchBooks("keyword");

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void SearchBooks_WhenKeywordIsNull_ReturnsNull()
        {
            // Arrange
            var bookStoreData = new BookStoreData { Books = new List<Book>() };
            _fileManagerWrapperMock.Setup(f => f.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            var result = _searchBooksService.SearchBooks(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}