
namespace Bookstore.Classes
{
    // Reusable error messages for the project
    public static class ErrorMessages
    {
        public const string GenError = "Oops! Something went wrong!";
        public const string EmptyFileError = "The file is empty!";
        public const string FileNotFoundError = "File doesn't exist in the current directory!";
        public const string TitleError = "Title should be a valid text and should contain max 50 characters!";
        public const string AuthorError = "Author should be a valid text and should contain max 50 characters!";
        public const string PriceError = "Price must be a positive numerical value!";
        public const string QuantityError = "Quantity must be a positive numerical value!";
        public const string DescriptionError = "Description cannot be empty or whitespace!";
        public const string NoBooks = "There are no books in the file!";
        public const string NoBooksFound = "No books found matching the search criteria.";
        public const string JsonDeserializationError = "There is a problem reading from the file!";
        public const string UnhandledExceptionError = "Unhandled exception occured!";
        public const string MenuOptionError = "Invalid choice. Please enter a number between 1 and 6.";
    }
}
