using Bookstore.Classes;
using Bookstore.Classes.ServiceOutputs;
using Bookstore.Classes.Services;
using Bookstore.Classes.Strings;
using Bookstore.Interfaces;
using System.Collections.Generic;
using System.Text;

namespace Bookstore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create instances of required objects
            Book book = new Book();
            List<Book> books = new List<Book>();
            FileManager fileManager = new FileManager(Paths.JsonFilePath);
            BookStoreData bookStoreData = fileManager.ReadFromJson<BookStoreData>();
            StringBuilder stringBuilder = new StringBuilder();
            MenuLabels menuLabels = new MenuLabels();

            // Create instances of required services
            DisplayBooksService displayBooksService = new DisplayBooksService(fileManager);
            SearchBooksService searchBooksService = new SearchBooksService(fileManager);
            AddNewBookService addNewBookService = new AddNewBookService(bookStoreData, fileManager, books);
            CalculateTotalValueService calculateTotalValueService = new CalculateTotalValueService(books, fileManager);

            // Create instance of the service manager and pass the required services
            IServiceManager serviceManager = new ServiceManager(displayBooksService, 
                                                                searchBooksService, 
                                                                addNewBookService,
                                                                calculateTotalValueService);

            // Create instance of the service output and pass the service manager and string builder
            ServiceOutput serviceOutput = new ServiceOutput(serviceManager, stringBuilder);

            // Create instance of the console output and pass the service manager, service output, and menu labels
            ConsoleOutput consoleOutput = new ConsoleOutput(serviceManager, serviceOutput, menuLabels);

            // Output the menu to the console
            consoleOutput.MenuOutput();
        }
    }
}
