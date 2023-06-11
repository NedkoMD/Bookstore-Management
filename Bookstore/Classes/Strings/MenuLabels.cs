using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Classes.Strings
{
    // Represents labels for the menu with a method for showing them
    public class MenuLabels
    {
        public void ShowMenuLabels()
        {
            Console.WriteLine("========== Bookstore Management ==========");
            Console.WriteLine("1. Display Books");
            Console.WriteLine("2. Search Books");
            Console.WriteLine("3. Add New Book");
            Console.WriteLine("4. Calculate Total Value");
            Console.WriteLine("5. Apply Discounts");
            Console.WriteLine("6. Save");
            Console.WriteLine();
            Console.Write("Enter your choice (1-6): ");
        }
    }
}
