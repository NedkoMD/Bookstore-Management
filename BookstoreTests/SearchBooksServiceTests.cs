using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Bookstore.Classes.Services;
using Bookstore.Classes;
using Bookstore.Classes.Strings;

namespace BookstoreTests
{
    // A wrapper interface for the file manager to facilitate mocking.
    public interface IFileManagerWrapper
    {
        T ReadFromJson<T>();
    }

    // A concrete implementation of the IFileManagerWrapper interface.
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
            // Creates a mock object for the IFileManagerWrapper interface.
            _fileManagerWrapperMock = new Mock<IFileManagerWrapper>();

            // Initializes the SearchBooksService with a FileManager instance and a JSON file path.
            _searchBooksService = new SearchBooksService(new FileManager(Paths.JsonFilePath));
        }

        [Test]
        public void SearchBooks_WhenNoBooksExist_ReturnsNull()
        {
            // Arrange
            var bookStoreData = new BookStoreData { Books = null };

            // Sets up the mock object to return the specified bookStoreData when ReadFromJson<BookStoreData> is called.
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

            // Sets up the mock object to return the specified bookStoreData when ReadFromJson<BookStoreData> is called.
            _fileManagerWrapperMock.Setup(f => f.ReadFromJson<BookStoreData>()).Returns(bookStoreData);

            // Act
            var result = _searchBooksService.SearchBooks(null);

            // Assert
            Assert.IsNull(result);
        }
    }
}
