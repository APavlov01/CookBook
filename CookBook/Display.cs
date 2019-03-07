using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Display
    {
        public Display()
        {

        }

        public void WelcomeScreen()
        {
            for (int i = 0; i < 120; i++) Console.Write("-");
            for (int i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Welcome to CookBook!");
            for (int i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- With this program you can add, delete and search for recipes.");
            Console.WriteLine("- Each recipe contains of name, ingredients and description.");
            Console.WriteLine("- You can either search a recipe by an igredient it contains or by its name.");
            Console.WriteLine("- You can delete the recipe by simply typing it's name in the delete section.");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("- Add");
            Console.WriteLine("- Delete");
            Console.WriteLine("- Search");
        }

        public string GetCommand()
        {
            string[] commands = { "add", "delete", "search", "exit" };
            Console.Write("\nChoose an option: ");
            var choice = Console.ReadLine();
            while (choice == "")
            {
                Console.Write("\nChoose an option: ");
                choice = Console.ReadLine();
            }
            choice = choice.ToLower();
            if (!commands.Contains(choice))
            {
                Console.WriteLine("Choice not valid!");
                GetCommand();
            }
            if (choice == "exit")
            {
                Environment.Exit(0);
            }
            return choice;
        }
    }
}
