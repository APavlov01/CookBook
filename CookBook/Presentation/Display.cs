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
            Console.WriteLine("\nAbout:");
            Console.WriteLine("- With this program you can add, delete and search for recipes.");
            Console.WriteLine("- Each recipe contains of name, ingredients and description.");
            Console.WriteLine("- You can either search a recipe by an igredient it contains or by its name.");
            Console.WriteLine("- You can delete the recipe by simply typing it's name in the delete section.");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("- Add");
            Console.WriteLine("- Delete");
            Console.WriteLine("- Search");
            Console.WriteLine("- Rate");
        }

        public string GetCommand()
        {
            string[] commands = { "add", "delete", "search", "exit","rate" };
            Console.Write("\nChoose an option: ");
            var choice = Console.ReadLine().ToLower();
            while (!commands.Contains(choice))
            {
                Console.WriteLine("Choice not valid!");
                Console.Write("\nChoose an option: ");
                choice = Console.ReadLine().ToLower();
            }
            if (choice == "exit")
            {
                Environment.Exit(0);
            }
            return choice;
        }

        public void RatingCmdDisplay()
        {
            Console.WriteLine("Add a rating to a recipe.\n -Name:");
           // RecipeName = Console.ReadLine();
            

            Console.WriteLine(" -Rating: ");
           // RecipeRating = double.Parse(Console.ReadLine());
        }
        public string DeleteCmdDisplay()
        {
            Console.WriteLine("Enter a recipe name:");
            string recipeName = Console.ReadLine();
            return recipeName;
        }
        public void SearcheCmdDisplay()
        {
            Console.WriteLine("Enter a recipe name:");
            //RecipeName = Console.ReadLine();
        }
        public void AddCmdDisplay()
        {
            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Add a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- This command lets you add a new recipe with all its characteristics");
            Console.WriteLine("- After you enter an ingredient and its count or grams, press Enter to enter new ingredient");
            Console.WriteLine("- After you entered your ingredients enter 'end' as an ingredient to finish your input");
            Console.WriteLine("- In the description you say how the meal is made");
            Console.WriteLine("- In rating put a number between 1 and 10 including to rate the recipe");      
        }


        public string GetRecipeName()
        {
            Console.Write("\nName: ");
            string RecipeName = Console.ReadLine();
            return RecipeName;
        }
        public string GetIngredients()
        {
            Console.WriteLine("Enter ingredient: ");
            string ingredient = Console.ReadLine();
            return ingredient;
        }
        public string GetDescription()
        {
            Console.WriteLine("Enter descreption: ");

            string description = Console.ReadLine();

            return description;
        }
        public double GetRating()
        {
            return 0;
        }
        public string ReadIngredient()
        {
            return null;
        }
    }
}
