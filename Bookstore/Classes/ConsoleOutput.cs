using Bookstore.Classes.ServiceOutputs;
using Bookstore.Classes.Strings;
using Bookstore.Interfaces;
using System;

namespace Bookstore.Classes
{
    /// <summary>
    /// Represents the console output for the bookstore program.
    /// </summary>
    internal class ConsoleOutput
    {
        private readonly IServiceManager _serviceManager;
        private readonly ServiceOutput _servicesOutput;
        private readonly MenuLabels _menuStrings;

        // Initializes a new instance of the ConsoleOutput class with the provided dependencies.
        public ConsoleOutput(IServiceManager serviceManager, ServiceOutput servicesOutput, MenuLabels menuStrings)
        {
            _serviceManager = serviceManager;
            _servicesOutput = servicesOutput;
            _menuStrings = menuStrings;
        }

        // Represents the available menu options.
        public enum MenuOption
        {
            DisplayBooks = 1,
            SearchBooks = 2,
            AddNewBook = 3,
            CalculateTotalValue = 4,
            ApplyDiscounts = 5,
            Save = 6
        }

        // Read user input and parse it as a MenuOption
        public void MenuOutput()
        {
            while (true)
            {
                _menuStrings.ShowMenuLabels();
                MenuOption choice;
                if (Enum.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case MenuOption.DisplayBooks:
                            _servicesOutput.DisplayBooksOutput();
                            break;
                        case MenuOption.SearchBooks:
                            _servicesOutput.SearchBooksOutput();
                            break;
                        case MenuOption.AddNewBook:
                            _servicesOutput.AddNewBookOutput();
                            break;
                        case MenuOption.CalculateTotalValue:
                            _servicesOutput.CalculateTotalValueOutput();
                            break;
                        case MenuOption.ApplyDiscounts:
                            _serviceManager.ApplyDiscounts();
                            break;
                        case MenuOption.Save:
                            _serviceManager.Save();
                            break;
                        default:
                            Console.WriteLine(ErrorMessages.MenuOptionError);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine(ErrorMessages.MenuOptionError);
                }

                Console.WriteLine();
            }
        }
    }
}
