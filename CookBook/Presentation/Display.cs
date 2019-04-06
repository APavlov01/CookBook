using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    /// <summary>
    /// This class is for displaying all the messages and user interfaces for different functionalities.
    /// </summary>
    public class Display
    {
        /// <summary>
        /// This is the constructor of the class.
        /// </summary>
        public Display()
        {

        }

        /// <summary>
        /// This method displays the user interface for the main menu of the program.
        /// </summary>
        public void WelcomeScreen() {
            for (int i = 0; i < 120; i++) Console.Write("-");
            for (int i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Welcome to CookBook!");
            for (int i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine("\nAbout:");
            Console.WriteLine("- With this program you can add, delete, search, update, rate recipes.");
            Console.WriteLine("- Each recipe contains of name, ingredients and description.");
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

        /// <summary>
        /// This method gets the command entered by the user.
        /// </summary>
        /// <returns>Returns the input of the user.</returns>
        public string GetCommand()
        {
            Console.Write("\nChoose an option: ");
            //If the input of the user contains any additional spaces they are removed.
            var choice = Console.ReadLine().ToLower().Trim();

            choice = string.Join(" ", choice.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
            return choice;
        }

        /// <summary>
        /// This method displays the user interface for the 'Rate a recipe' function.
        /// </summary>
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

        /// <summary>
        /// This method displays the user interface for the 'Delete a recipe' function.
        /// </summary>
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

        /// <summary>
        /// This method displays the user interface for the 'Search a recipe' function.
        /// </summary>
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

        /// <summary>
        /// This method displays the user interface for the 'Update a recipe' function.
        /// </summary>
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

        /// <summary>
        /// This method displays the user interface for the 'Add a recipe' function.
        /// </summary>
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

        /// <summary>
        /// This method gets the entered by the user recipe name.
        /// </summary>
        /// <returns>Returns the recipe entered by the user.</returns>
        public string GetRecipeName() {
            Console.Write("\nName: ");

            string RecipeName = Console.ReadLine().Trim();

            return RecipeName;
        }

        /// <summary>
        /// This method gets the entered by the user ingredients.
        /// </summary>
        /// <returns>Returns the entered ingredients.</returns>
        public string GetIngredients() {
            Console.Write("Enter ingredient: ");

            string ingredient = Console.ReadLine();

            return ingredient;
        }

        /// <summary>
        /// Тhis method gets the description entered by the user.
        /// </summary>
        /// <returns>Returns the entered description.</returns>
        public string GetDescription() {
            Console.Write("Enter description: ");

            string description = Console.ReadLine();

            return description;
        }

        /// <summary>
        /// This method gets the rating entered by the user.
        /// </summary>
        /// <returns>Returns the entered rating.</returns>
        public string GetRating() {
            Console.Write("Enter rating between 0 and 5: ");
            string rating = Console.ReadLine();
            return rating;
        }

        /// <summary>
        /// This method displays the string in 'result'.
        /// </summary>
        /// <param name="result">A string which can contain various messages depending on where it's used.</param>
        public void PrintResult(string result) {
            if (!string.IsNullOrEmpty(result)) {
                Console.WriteLine(result);
            }
        }

        /// <summary>
        /// This method allows the user to return to the main menu by pressing any key on the keyboard.
        /// </summary>
        public void ReturnToMainMenuScreen() {
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// This method gets the command used to update the ingredients such as 'add', 'delete', 'edit'. 
        /// </summary>
        /// <returns>Returns the command entered by the user.</returns>
        public string UpdateIngredientsScreen() {
            Console.WriteLine("Do you want to add a new ingredient or to edit/remove an existing one.");
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// This method gets the ingredient index in the list of ingredients.
        /// </summary>
        /// <returns>Returns the index of the ingredient.</returns>
        public string GetIngredientIndex() {
            Console.WriteLine("Which ingredient do you want to edit? (enter a number): ");
            string index = Console.ReadLine();
            return index;
        }

        /// <summary>
        /// This method gets the ingredient quantity.
        /// </summary>
        /// <returns>Returns the ingredient quantity.</returns>
        public string GetQuantity() {
            Console.Write("Enter new quantity: ");
            string quantity =Console.ReadLine();
            return quantity;
        }

        /// <summary>
        /// This method displays the top 5 recipes in the console.
        /// </summary>
        /// <param name="output">The name and rating of the recipes converted to a string.</param>
        public void DisplayTop5(string output) {
            int i = 0;
            Console.Clear();
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("Top 5 recipes.");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine(output);
        }

        /// <summary>
        /// This method displays all the recipes in the database.
        /// </summary>
        /// <param name="output">The name of the recipes converted to a string.</param>
        public void DisplayAllRecipes(string output) {
            int i = 0;
            Console.Clear();
            for (i = 0; i < 120; i++) Console.Write("-");
            for (i = 0; i < 52; i++) Console.Write(" "); Console.WriteLine("All recipes.");
            for (i = 0; i < 120; i++) Console.Write("-");
            Console.WriteLine(output);
        }

        /// <summary>
        /// This method displays the recipe.
        /// </summary>
        /// <param name="result">String containing the features of the recipe.</param>
        public void DisplayRecipe(string result) {
            Console.Clear();
            Console.WriteLine(result);
        }
    }
}