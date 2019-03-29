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
        }

        public string GetCommand()
        {
            string[] commands = { "add", "delete", "search", "exit" };
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


        private string RecipeName;
        private List<string> ingredients=new List<string>();
        private List<string> RecipeDescription= new List<string>();
        private double RecipeRating = 0;


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
            Console.Write("\nName: ");
            RecipeName = Console.ReadLine();
            Console.WriteLine("\nIngredients:");
            string ingredient="";
            while(ingredient.ToLower()!="end")
            {
                ingredient = Console.ReadLine();
                if (ingredient.ToLower() == "end") break;
                ingredients.Add(ingredient);
            }
            Console.WriteLine("\nDescription:");
            string descriptionLine="";
            while (descriptionLine.ToLower() != "end")
            {
                descriptionLine = Console.ReadLine();
                if (descriptionLine.ToLower() == "end") break;
                RecipeDescription.Add(ingredient);
            }
            //Console.WriteLine("\nRating: ");
            //RecipeRating = double.Parse(Console.ReadLine());
        }


        public string GetRecipeName()
        {
            //Console.WriteLine($"\nRecipeName={RecipeName}");*/              //Shows the name of the recipe
            return RecipeName;
        }
        public List<string> GetIngredients()
        {
            //Console.WriteLine("\nList of ingredients:");
            //for (int i = 0; i < ingredients.Count; i++)
            //{                                                             //Shoes the names of the ingredients
            //    Console.WriteLine($"{ingredients[i]}");
            //}
            return ingredients;
        }
        public List<string> GetDescription()
        {
            return RecipeDescription;
        }
        public double GetRating()
        {
            return RecipeRating;
        }
    }
}
