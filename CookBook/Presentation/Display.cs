using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook {
    public class Display {
        public Display() {

        }

        public void WelcomeScreen() {
            for (int i = 0; i < 120; i++) Console.Write("-");
            for (int i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Welcome to CookBook!");
            for (int i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("\nAbout:");
            Console.WriteLine("- With this program you can add, delete, search, update recipes.");
            Console.WriteLine("- Each recipe contains of name, ingredients and description.");
            Console.WriteLine("- You can search a recipe by simply typing its name.");
            Console.WriteLine("- You can delete the recipe by simply typing its name in the delete section.");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("- Add");
            Console.WriteLine("- Delete");
            Console.WriteLine("- Search");
            Console.WriteLine("- Update");
            Console.WriteLine("- Rate");
            Console.WriteLine("- Top 5");
            Console.WriteLine("- Show all");
            Console.WriteLine("- Exit");
        }

        public string GetCommand() {
            Console.Write("\nChoose an option: ");
            var choice = Console.ReadLine().ToLower().Trim();
            choice = string.Join(" ", choice.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return choice;
        }

        public void RatingCmdDisplay() {
            Console.Clear();

            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Rate a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- Add a rating to a recipe by typing its name below");
            Console.WriteLine("- You can only rate an existing recipe once or multiple times");
            Console.WriteLine("- If you rate a recipe multiple times, the rating of the recipe will be averaged");
        }

        public void DeleteCmdDisplay() {
            Console.Clear();

            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Delete a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- Enter the recipe name you want to delete below!");
            Console.WriteLine("- You can only delete an existing recipe");
        }

        public void SearchCmdDisplay() {
            Console.Clear();

            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Search for a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- You can search for a recipe by simply typing its name below");
            Console.WriteLine("- You can only search for an existing recipe");
        }

        public void UpdateCmdDisplay() {
            Console.Clear();

            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Update a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- By updating a recipe you have to change all its features");
            Console.WriteLine("- Type the name of the recipe you want to change below");
            Console.WriteLine("- You can only update an existing recipe");
            Console.WriteLine("- Type either ingredients,description or name to edit");
        }

        public void AddCmdDisplay() {
            Console.Clear();
            int i = 0;
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Add a recipe");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("About:");
            Console.WriteLine("- This command lets you add a new recipe with all its characteristics");
            Console.WriteLine("- After you enter an ingredient and its ml or grams, press Enter to enter new ingredient");
            Console.WriteLine("- After you entered your ingredients enter 'end' as an ingredient to finish your input");
            Console.WriteLine("- In the description you say how the meal is made and after you finished writing it enter '#' and press ENTER");
        }


        public string GetRecipeName() {
            Console.Write("\nName: ");

            string RecipeName = Console.ReadLine().Trim();

            return RecipeName;
        }

        public string GetIngredients() {
            Console.Write("Enter ingredient: ");

            string ingredient = Console.ReadLine();

            return ingredient;
        }

        public string GetDescription() {
            Console.Write("Enter description: ");

            string description = Console.ReadLine();

            return description;
        }

        public string GetRating() {
            Console.Write("Enter rating between 0 and 5: ");
            string rating = Console.ReadLine();
            return rating;
        }

        public void PrintResult(string result) {
            if (!string.IsNullOrEmpty(result)) {
                Console.WriteLine(result);
            }
        }

        public void ReturnToMainMenuScreen() {
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        public string UpdateIngredientsScreen() {
            Console.WriteLine("Do you want to add a new ingredient or to edit/remove an existing one.");
            string input = Console.ReadLine();
            return input;
        }

        public string GetIngredientIndex() {
            Console.WriteLine("Which ingredient do you want to edit? (enter a number): ");
            string index = Console.ReadLine();
            return index;
        }

        public string GetQuantity() {
            Console.Write("Enter new quantity: ");
            string quantity =Console.ReadLine();
            return quantity;
        }

        public void DisplayTop5(string output) {
            int i = 0;
            Console.Clear();
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Top 5 recipes.");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine(output);
        }

        public void DisplayAllRecipes(string output) {
            int i = 0;
            Console.Clear();
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("All recipes.");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine(output);
        }

        public void DisplayRecipe(string result) {
            Console.Clear();
            Console.WriteLine(result);
        }
    }
}