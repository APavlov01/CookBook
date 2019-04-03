﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook {
    public class Controller {
        private Display display = new Display();
        private RecipeContext recipeContext = new RecipeContext();
        private string command;
        private string result;

        public Controller() {

        }

        public void Start() {
            display.WelcomeScreen();
            command = display.GetCommand();
            //if (command == "add") { Console.Clear(); Add(); }
            //else if (command == "rate") { Console.Clear(); Rate(); }
            //else if (command == "delete") { Console.Clear(); Delete(); }
            //else if (command == "search") { Console.Clear(); Search(); }
            //else if (command == "update") { Console.Clear(); Update(); }
            switch (command) {
                case "add":
                    Add();
                    break;
                case "rate":
                    Rate();
                    break;
                case "search":
                    Search();
                    break;
                case "update":
                    Update();
                    break;
                case "delete":
                    Delete();
                    break;
            }
        }
        private void Add() {
            string recipeName = null;
            string ingredients = null;
            string description = null;


            display.AddCmdDisplay();
            recipeName = RecipeRead();
            ingredients = IngredientsRead();
            description = DescriptionRead();


            Recipe recipe = new Recipe {
                Name = recipeName,
                Ingredients = ingredients,
                Description = description
            };

            recipeContext.Recipes.Add(recipe);

            recipeContext.SaveChanges();
            display.PrintResult("\nPress ENTER to go to the main menu");
            Console.ReadKey();
            Console.Clear();
            Start();
        }
        //Add function bug: Pressing enter on name without inputting anything =====> See RecipeRead() ====>fixed
        private string RecipeRead() {
            string recipeName = null;
            int validator = 0;

            do {
                recipeName = display.GetRecipeName().ToLower();
                if (ValidateRecipeName(recipeName) != -1) {
                    recipeName = recipeName.First().ToString().ToUpper() + recipeName.Substring(1); // That caused the problem
                }

                validator = ValidateRecipeName(recipeName);

                if (validator == 0) {
                    result = "Name already used!";
                }

                else if (validator == -1) {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1) {
                    result = "Successfully added recipe name!\n";
                    break;
                }

                display.PrintResult(result);

            } while (ValidateRecipeName(recipeName) != 1);

            display.PrintResult(result);

            return recipeName;
        }

        private string IngredientsRead() {
            string ingredientArgs = "";
            int validator = 0;
            bool continueReading = true;
            string ingredients = "";
            List<string> ingredientsToParse = new List<string>();

            int ingredientCount = 0;

            while (continueReading) {

                ingredientArgs = display.GetIngredients().ToLower();

                if (ingredientCount == 0 && ingredientArgs.Equals("end")) {
                    result = "Please enter atleast one ingredient.";
                    display.PrintResult(result);
                }

                else if (!ingredientArgs.Equals("end")) {

                    validator = ValidateIngredients(ingredientArgs);

                    if (validator == 1) {
                        result = "Successfuly added ingredient!\n";
                        ingredientCount++;
                        ingredientsToParse.Add(ingredientArgs);
                        //var ingredient = IngredientParsing(ingredientArgs);
                        //ingredients.Add(ingredient);
                    }

                    else if (validator == -1) {
                        result = "Invalid quantity input!\n";
                    }

                    else if (validator == 0) {
                        result = "Ingredient doesn't exist!\n";
                    }
                    display.PrintResult(result);
                }

                else {
                    continueReading = false;
                    break;
                }

            }

            result = "Finished adding ingredients!\n";

            display.PrintResult(result);
            ingredients = IngredientParse(ingredientsToParse);
            return ingredients;  //TO DO implement IngredientParsing() method 
        }

        private string IngredientParse(List<string> ingredientArgs) {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (var ingredient in ingredientArgs) {
                List<string> ingredientToParse = ingredient.Split().ToList();
                int argumentsCount = ingredientToParse.Count - 1;
                for (int i = 0; i < argumentsCount; i++) {
                    sb.Append(ingredientToParse[i]);
                    if (i == argumentsCount - 1) {
                        break;
                    }
                    sb.Append("-");
                }
                counter++;
                sb.Append(" " + ingredientToParse[argumentsCount]);
                if (ingredientArgs.Count - counter > 0) {
                    sb.Append(";");
                }

            }
            string ingredientParsed = sb.ToString().ToLower();
            return ingredientParsed;
        }

        private string DescriptionRead() {
            int paragraphCount = 1;
            int validator = 0;
            string description = null;

            StringBuilder sb = new StringBuilder();
            do {
                string paragraph = display.GetDescription();

                validator = ValidateDescription(paragraph, paragraphCount);

                if (validator == 1) {
                    result = "Successfully added description!";
                }
                else if (validator == -1 || validator == 0) {
                    result = "Please enter a valid paragraph.";
                    display.PrintResult(result);
                    continue;
                }

                sb.Append(paragraph + "\n");
                paragraphCount++;

            } while (validator != 1);

            description = sb.ToString();
            description = description.Remove(description.Length - 2);
            display.PrintResult(result);

            return description;
        }

        private int ValidateDescription(string description, int paragraphCount) {
            if (string.IsNullOrEmpty(description)) {
                return -1;
            }
            else if (description.Equals("#") && paragraphCount == 1) {
                return 0;
            }
            else if (description.EndsWith("#")) {
                return 1;
            }

            return 2;
        }

        private int ValidateIngredients(string ingredient) {
            List<string> ingredientArguments = ingredient.Split().ToList();
            StringBuilder sb = new StringBuilder();
            int argumentCount = ingredientArguments.Count - 1;
            for (int i = 0; i < argumentCount; i++) {
                sb.Append(ingredientArguments[i]); // eggs-yolk
                if (i == argumentCount - 1) {
                    break;
                }
                sb.Append("-");
            }
            var check = recipeContext.Ingredients.ToList();
            try {
                check.Single(x => x.Name.ToLower() == sb.ToString().ToLower());
            }
            catch {
                return 0;
            }
            try {
                double.Parse(ingredientArguments[argumentCount]);
            }
            catch {
                return -1;
            }

            if (double.Parse(ingredientArguments[argumentCount]) <= 0) {
                return -1;
            }


            return 1;
        }

        private int ValidateRecipeName(string recipeName) {
            if (string.IsNullOrEmpty(recipeName)) {
                return -1;
            }
            try {
                recipeContext.Recipes.Single(x => x.Name == recipeName);
                return 0;
            }
            catch {
                return 1;
            }
        }

        private void Rate() {
            display.RatingCmdDisplay();
            AddRating();
            display.ReturnToMainMenuScreen();
            Start();
        }

        private void AddRating() {
            string recipeName = null;
            int validator = 0;
            int rating = 0;

            do {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0) {
                    result = "Successfully found recipe!";
                    break;
                }
                else if (validator == -1) {
                    result = "Name cannot be empty!";
                }
                else if (validator == 1) {
                    result = "Such recipe doesn't exist!";
                }

                display.PrintResult(result);

            } while (ValidateRecipeName(recipeName) != 0);

            do {
                try {
                    rating = display.GetRating();
                    if (rating < 0 || rating > 5) {
                        result = "Rating must be between 0 and 5!";
                        validator = -1;
                    }
                    else {
                        result = "Rating is valid.Adding to database.";
                        validator = 1;
                    }
                }

                catch {
                    result = "Rating must be an integer!";
                    validator = 0;
                }
                display.PrintResult(result);

            } while (validator != 1);

            Recipe recipeToRate = recipeContext.Recipes.Single(x => x.Name == recipeName);
            Rating ratingToAdd = new Rating();
            ratingToAdd.Score = rating;
            ratingToAdd.Recipe = recipeToRate;
            recipeContext.Ratings.Add(ratingToAdd);
            recipeContext.SaveChanges();
            result = "Rating added to database!";
            display.PrintResult(result);
        }

        private void Delete() {
            display.DeleteCmdDisplay();

            string recipeName = null;

            int validator = 0;

            Recipe recipe = new Recipe();

            StringBuilder sb = new StringBuilder();

            while (ValidateRecipeName(recipeName) != 0) {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0) {
                    result = "Successfully deleted recipe!";
                    break;
                }
                else if (validator == -1) {
                    result = "Name cannot be empty!";
                }
                else if (validator == 1) {
                    result = "Such recipe doesn't exist!";
                }

                display.PrintResult(result);

            }

            recipe.Name = recipeName;
            recipeContext.Remove(recipeContext.Recipes.Single(x => x.Name == recipe.Name));
            recipeContext.SaveChanges();

            sb.Append(result + "\n" + "Press ENTER to go to the main menu");
            display.PrintResult(sb.ToString());

            display.ReturnToMainMenuScreen();
            Start();
        }

        private void Search() {
            display.SearchCmdDisplay();
            string name = null;
            bool continueInput = true;

            while (continueInput == true) {
                name = display.GetRecipeName();
                if (ValidateRecipeName(name) == 1) {
                    result = "No such recipe in database!";
                }
                else if (ValidateRecipeName(name) == -1) {
                    result = "Name cannot be empty!";
                }
                else if (ValidateRecipeName(name) == 0) {
                    result = "Our turtles are searching in the database";
                    continueInput = false;
                }
                display.PrintResult(result);
            }

            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == name);

            result = RecipeOutput(recipe);
            display.PrintResult(result);

            display.ReturnToMainMenuScreen();
            Start();

        }

        private void Update() {
            string recipeName = null;
            string[] options = { "ingredients", "name", "description" };
            string choice = null;
            int validator = 0;

            display.UpdateCmdDisplay();

            do //Validation
            {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0) {
                    result = "Successfully found recipe!";
                }
                else if (validator == -1) {
                    result = "Name cannot be empty!";
                }
                else if (validator == 1) {
                    result = "Such recipe doesn't exist!";
                }

                display.PrintResult(result);

            } while (validator != 0);

            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == recipeName); // Gets the whole recipe from database
            display.PrintResult(RecipeOutput(recipe)); //Shows what the recipe contains
            display.PrintResult("\nWhat do you want to update?"); //Asking what to update
            while (!options.Contains(choice)) //Validates if the input is either ingredients, name or description
            {
                choice = Console.ReadLine().ToLower();
                if (options.Contains(choice)) {
                    break;
                }
                display.PrintResult("Invalid input!");
            }

            switch (choice) {
                case "name":
                    UpdateRecipeName(recipe);
                    break;
                case "ingredients":
                    UpdateIngredients(recipe);
                    break;
                case "description":
                    UpdateDescription(recipe);
                    break;
            }

            display.ReturnToMainMenuScreen();
            Start();

        }


        private void UpdateRecipeName(Recipe recipe) {
            string NewRecipeName = RecipeRead();
            recipe.Name = NewRecipeName;
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        private void UpdateIngredients(Recipe recipe) {
            string command = display.UpdateIngredientsScreen();
            string ingredients = recipe.Ingredients;

            switch (command) {
                case "add":
                    UpdateIngredientsAdd(recipe);
                    break;
                case "remove":
                    UpdateIngredientsRemove(recipe);
                    break;
                case "edit":
                    UpdateIngredientsEdit(recipe);
                    break;
            }
        }

        private void UpdateIngredientsEdit(Recipe recipe) {
            var allIngredients = recipe.Ingredients.Split(";").ToArray();
            string[] ingredient = null;
            int index = 0;
            int newQuantity =0;
            int ingredientNameIndex = 0;
            int ingredientQuantityIndex = 1;
            string updatedIngredient = null;
            StringBuilder sb = new StringBuilder();

            while (true) {
                try {
                    index = display.GetIngredientIndex() - 1;
                    newQuantity = display.GetQuantity();
                    break;
                }
                catch {
                    result = "Invalid input!";
                    display.PrintResult(result);
                }
            }

            ingredient = allIngredients[index].Split();
            ingredient[ingredientQuantityIndex] = newQuantity.ToString();
            updatedIngredient = ingredient[ingredientNameIndex] + " " + ingredient[ingredientQuantityIndex];

            for (int i = 0; i < allIngredients.Length; i++) {
                if(i == index) {
                    sb.Append(updatedIngredient);
                }
                else {
                    sb.Append(allIngredients[i]);
                }
                sb.Append(";");
            }
            recipe.Ingredients = sb.ToString().Remove(sb.Length - 1);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
            

        }

        private void UpdateIngredientsRemove(Recipe recipe) {
            var allIngredients = recipe.Ingredients.Split(";").ToArray();
            StringBuilder sb = new StringBuilder();
            int index = 0;
            result = RecipeOutput(recipe);
            display.PrintResult(result);
            while (true) {
                try {
                    index = display.GetIngredientIndex() - 1;
                    break;
                }
                catch {
                    result = "Invalid input!";
                    display.PrintResult(result);
                }
            }
            for (int i = 0; i < allIngredients.Length; i++) {
                if (i == index) {
                    continue;
                }
                sb.Append(allIngredients[i] + ";");
            }

            recipe.Ingredients = sb.ToString().Remove(sb.Length - 1);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        private void UpdateIngredientsAdd(Recipe recipe) {
            string ingredients = IngredientsRead();
            StringBuilder sb = new StringBuilder();
            sb.Append(recipe.Ingredients + ";" + ingredients);
            recipe.Ingredients = sb.ToString();
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();

        }

        private void UpdateDescription(Recipe recipe) {
            string description = DescriptionRead();
            recipe.Description = description;
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        private string RecipeOutput(Recipe recipe) {
            string ingredients = null;
            string description = null;
            double caloriesPerRecipe = 0;

            ingredients = recipe.Ingredients;
            description = recipe.Description;
            StringBuilder sb = new StringBuilder();
            double rating = 0;
            int ratingsCounter = 0;
            foreach (Rating ratingInDB in recipeContext.Ratings) {
                if (ratingInDB.Recipe == recipe) {
                    rating += ratingInDB.Score;
                    ratingsCounter++;
                }

            }
            rating /= ratingsCounter;
            sb.Append("\nName: " + recipe.Name + $"  Rating: {rating:F2}" + "\n" + "\nIngredients:\n");
            caloriesPerRecipe = CalculateCalories(ingredients);
            ingredients = IngredientsToPlainText(ingredients);
            sb.Append(ingredients + "\n");
            description = recipe.Description;



            sb.Append("Description:\n" + description + $"\n\nCalories for this recipe are: {caloriesPerRecipe:F2}");

            return sb.ToString();
        }

        private double CalculateCalories(string ingredients) {
            Ingredient checkIngredientInDatabase = new Ingredient();
            double calories = 0;
            int ingredientNameIndex = 0;
            int ingredientQuantityIndex = 1;
            var allIngredients = ingredients.Split(";").ToArray(); //separates the ingredients

            foreach (var ingredient in allIngredients) {
                var ingredientAndQuantity = ingredient.Split().ToArray();
                string ingredientName = ingredientAndQuantity[ingredientNameIndex];
                int ingredientQuantity = int.Parse(ingredientAndQuantity[ingredientQuantityIndex]);

                checkIngredientInDatabase = recipeContext.Ingredients.Single(x => x.Name == ingredientName);
                calories += (checkIngredientInDatabase.Calories * ingredientQuantity) / 100;
            }

            return calories;
        }

        private string IngredientsToPlainText(string ingredients) {
            List<string> allIngredients = ingredients.Split(";").ToList();
            StringBuilder sb = new StringBuilder();
            int i = 1;
            foreach (var ingredient in allIngredients) {
                List<string> ingredientAndQuantity = ingredient.Split().ToList();
                sb.Append(i + " " + ingredientAndQuantity[0] + " " + ingredientAndQuantity[1] + " ");
                Ingredient check = new Ingredient();
                string ingredientName = ingredientAndQuantity[0];
                string type = null;
                check = recipeContext.Ingredients.Single(x => x.Name == ingredientName);
                type = check.Type;
                sb.Append(type + "\n");
                i++;

            }
            string ingredientsInPLainText = sb.ToString();

            return ingredientsInPLainText;
        }
    }
}
