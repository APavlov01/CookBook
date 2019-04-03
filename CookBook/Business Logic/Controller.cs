using System;
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
                case "top 5":
                    Top5();
                    break;
                case "show all":
                    ShowAll();
                    break;
            }
        }



        public void Add() {
            string recipeName = null;
            string ingredients = null;
            string description = null;
            double callories = 0;


            display.AddCmdDisplay();
            recipeName = RecipeRead();
            ingredients = IngredientsRead();
            description = DescriptionRead();
            callories = CalculateCalories(ingredients);


            Recipe recipe = new Recipe {
                Name = recipeName,
                Ingredients = ingredients,
                Description = description,
                Calories = callories
            };

            recipeContext.Recipes.Add(recipe);

            recipeContext.SaveChanges();
            display.ReturnToMainMenuScreen();
            Start();
        }

        public string RecipeRead() {
            string recipeName = null;
            int validator = 0;

            do {
                recipeName = display.GetRecipeName().ToLower();
                if (ValidateRecipeName(recipeName) != -1) {
                    recipeName = recipeName.First().ToString().ToUpper() + recipeName.Substring(1);
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

        public string IngredientsRead() {
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
            return ingredients;
        }

        public string IngredientParse(List<string> ingredientArgs) {
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
        } //Tested

        public string DescriptionRead() {
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

        public int ValidateDescription(string description, int paragraphCount) {
            if (string.IsNullOrEmpty(description)) {
                return -1;
            }
            else if (description.Equals("#") && paragraphCount == 1) {
                return 0;
            }
            else if (description.EndsWith("#")) {
                return 1;
            }

            return 2;//Unit test for that or Nah???If yes ====> else return 1;
        } //Tested

        public int ValidateIngredients(string ingredient) {
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
        } //Tested

        public int ValidateRecipeName(string recipeName) {
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
        }  //Tested

        public void Rate() {
            display.RatingCmdDisplay();
            AddRating();
            display.ReturnToMainMenuScreen();
            Start();
        } 

        public void AddRating() {
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

        public void Delete() {
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

        public void Search() {
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
            display.DisplayRecipe(result);

            display.ReturnToMainMenuScreen();
            Start();

        }

        public void Update() {
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

        public void UpdateRecipeName(Recipe recipe) 
        {
            string NewRecipeName = RecipeRead();
            recipe.Name = NewRecipeName;
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        public void UpdateIngredients(Recipe recipe)
        {
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

        public void UpdateIngredientsEdit(Recipe recipe) {
            var allIngredients = recipe.Ingredients.Split(";").ToArray();
            string[] ingredient = null;
            int index = 0;
            int newQuantity = 0;
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
                if (i == index) {
                    sb.Append(updatedIngredient);
                }
                else {
                    sb.Append(allIngredients[i]);
                }
                sb.Append(";");
            }
            recipe.Ingredients = sb.ToString().Remove(sb.Length - 1);
            recipe.Calories = CalculateCalories(recipe.Ingredients);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();


        }

        public void UpdateIngredientsRemove(Recipe recipe) {
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
            recipe.Calories = CalculateCalories(recipe.Ingredients);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        public void UpdateIngredientsAdd(Recipe recipe) {
            string ingredients = IngredientsRead();
            StringBuilder sb = new StringBuilder();
            sb.Append(recipe.Ingredients + ";" + ingredients);
            recipe.Ingredients = sb.ToString();
            recipe.Calories = CalculateCalories(recipe.Ingredients);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();

        }

        public void UpdateDescription(Recipe recipe) {
            string description = DescriptionRead();
            recipe.Description = description;
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        public string RecipeOutput(Recipe recipe) {
            string ingredients = null;
            string description = null;

            ingredients = recipe.Ingredients;
            description = recipe.Description;
            StringBuilder sb = new StringBuilder();
            double rating = CalculateRating(recipe);

            sb.Append("\nName: " + recipe.Name + $"  Rating: {rating:F2}" + "\n" + "\nIngredients:\n");
            ingredients = IngredientsToPlainText(ingredients);
            sb.Append(ingredients + "\n");
            description = recipe.Description;

            sb.Append("Description:\n" + description + $"\n\nCalories for this recipe are: {recipe.Calories:F2}");

            return sb.ToString();
        } //Tested

        public double CalculateRating(Recipe recipe) {
            double rating = 0;
            int ratingsCounter = 0;
            foreach (Rating ratingInDB in recipeContext.Ratings) {
                if (ratingInDB.Recipe == recipe) {
                    rating += ratingInDB.Score;
                    ratingsCounter++;
                }

            }
            rating /= ratingsCounter;

            return rating;
        } //Gonotest

        public double CalculateCalories(string ingredients) {
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
        } //Tested

        public string IngredientsToPlainText(string ingredients) {
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
        } //Tested

        public void Top5() {
            var recipes = recipeContext.Recipes;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, double> recipesAndRating = new Dictionary<string, double>();

            foreach (var recipe in recipes) {
                string recipeName = recipe.Name;
                double rating = CalculateRating(recipe);
                recipesAndRating.Add(recipeName, rating);
            }

            recipesAndRating = recipesAndRating.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);

            foreach (var recipe in recipesAndRating) {
                string recipeName = recipe.Key;
                double rating = recipe.Value;
                sb.Append("- " + recipeName + ", Rating: " + rating + Environment.NewLine);
            }
            display.DisplayTop5(sb.ToString());
            display.ReturnToMainMenuScreen();
            Start();
        }

        public void ShowAll() {
            var allRecipes = recipeContext.Recipes.ToArray();
            StringBuilder output = new StringBuilder();
            int counter = 1;

            foreach (var recipe in allRecipes) {
                string recipeName = recipe.Name;
                output.Append(counter + ". " + recipeName + Environment.NewLine);
                counter++;
            }
            display.DisplayAllRecipes(output.ToString());
            display.ReturnToMainMenuScreen();
            Start();
        }
    }
}