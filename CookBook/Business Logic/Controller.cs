using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook {

    /// <summary>
    /// This class conatins all the logic used in the application.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Initialization of the Display class so Display methods can be called.
        /// </summary>
        private Display display = new Display();

        /// <summary>
        /// Initialization of the RecipeContext class so the database can be accessed and modified.
        /// </summary>
        private RecipeContext recipeContext = new RecipeContext();

        /// <summary>
        /// Store for the command variable which is entered by the user.
        /// </summary>
        private string command;

        /// <summary>
        /// Store for the result variable which is assigned differently depending on it's use and then given as an argument in the Display function called PrintResult.
        /// </summary>
        private string result;

        /// <summary>
        /// The class constructor.
        /// </summary>
        public Controller()
        {

        }

        /// <summary>
        /// This method uses the entered from the user command and depending on the command type the function connected with the command is called
        /// For example if the command 'add' is entered the Add() function is called.
        /// </summary>
        public void Start() {
            display.WelcomeScreen(); //Calls a function from Display class which displays the user interface for the Main menu.
            //Until the user enters a valid command the loop keeps checking.
            while (true)
            {
                //Assigns the value of the user input to the command variable.
                command = display.GetCommand();
                switch (command)
                {
                    case "add": //If the command 'add' is entered by the user the function Add() is called.
                        Add();
                        break;
                    case "rate": //If the command 'rate' is entered by the user the function Rate() is called.
                        Rate();
                        break;
                    case "search": //If the command 'search' is entered by the user the function Add() is called.
                        Search();
                        break;
                    case "update": //If the command 'update' is entered by the user the function Update() is called.
                        Update();
                        break;
                    case "delete": //If the command 'delete' is entered by the user the function Delete() is called.
                        Delete();
                        break;
                    case "top 5": //If the command 'top 5' is entered by the user the function Top5() is called.
                        Top5();
                        break;
                    case "show all": //If the command 'show all' is entered by the user the function ShowAll() is called.
                        ShowAll();
                        break;
                    case "exit": //If the command 'exit' is entered by the user the program closes.
                        Environment.Exit(0);
                        break;
                
                    default: //If none of the commands above are entered, a corresponding message is assigned to the 'result' string and displayed.
                        result = "Invalid option. Enter new command";
                        display.PrintResult(result);
                        break;

                }
            }
        }


        /// <summary>
        /// This method adds the recipe which is made by the user in the database.
        /// In the method the user has to enter the name, ingredients and description of a recipe.
        /// Each input has its own validation in a separate method.
        /// </summary>
        public void Add() {

            //Assigning variables
            string recipeName = null;
            string ingredients = null;
            string description = null;
            double callories = 0;


            display.AddCmdDisplay(); //Calls a function from Display class which displays the user interface for the Add function.
            recipeName = RecipeRead(); //Calls a function from the current class which returns the name of the recipe which the user entered.
            ingredients = IngredientsRead(); //Calls a function from the current class which returns the ingredients of the recipe which the user entered.
            description = DescriptionRead(); //Calls a function from the current class which returns the description of the recipe which the user entered.
            callories = CalculateCalories(ingredients); //Calls a function which returns the calculated calories from all the ingredients in the recipe.


            Recipe recipe = new Recipe //Creating an object from class Recipe and assigning the values the user entered to its properties.
            {
                Name = recipeName, //Assigning property 'Name' of 'recipe' with the value of 'recipeName'.
                Ingredients = ingredients, //Assigning property 'Ingredients' of 'recipe' with the value of 'ingredients'.
                Description = description, //Assigning property 'Description' of 'recipe' with the value of 'description'.
                Calories = callories //Assigning property 'Calories' of 'recipe' with the value of 'callories'.
            };

            recipeContext.Recipes.Add(recipe); //Accessing the database context 'RecipeContext' class, then accessing the table from the database 'Recipes' and adds a new recipe.
            recipeContext.SaveChanges(); //Saving the recipe to the database so it's not deleted after debugging.
            display.ReturnToMainMenuScreen(); //Calls a function which allows the user to return to the main screen by pressing any key.
            Start(); //Returning to main screen.
        }

        /// <summary>
        /// This method reads the recipe name entered by the user and checks if the name is valid in various ways.
        /// </summary>
        /// <returns>Returns the name of the recipe entered by the user when it's a valid name</returns>
        public string RecipeRead()
        {
            //Assigning variables
            string recipeName = null;
            int validator = 0;

            do //Until the user enters a valid recipe name the loop keeps checking the input
            {
                recipeName = display.GetRecipeName().Trim().ToLower(); //Assigning the user input to the variable 'recipeName'
                
                if (ValidateRecipeName(recipeName) != -1) //If the entered recipe name is not empty the first letter of the recipe name is made capital letter.
                {
                    //If the input by the user contains any additional spaces, they are removed.
                    recipeName= string.Join(" ", recipeName.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    //If the user enters an escape command whenever, he is returned to the main menu.
                    EscapeToMain(recipeName.ToLower());
                    recipeName = recipeName.First().ToString().ToUpper() + recipeName.Substring(1);
                }

                validator = ValidateRecipeName(recipeName); //Assigning the return of the ValidateRecipeName(string) method to the variable 'validator'.

                if (validator == 0) //If the name entered by the user is already used, to the 'result' string is assigned the corresponding message
                {
                    result = "Name already used!";
                }

                else if (validator == -1) //If the name entered by the user is empty, to the 'result' string is assigned the corresponding message
                {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1) //If the name entered by the user is unused and valid, to the 'result' string is assigned the corresponding message
                {
                    result = "Successfully added recipe name!\n";
                }

                display.PrintResult(result); //Displaying the message assigned to the 'result' string in the console.

            } while (ValidateRecipeName(recipeName) != 1);
            
            return recipeName; //Returns the valid recipe name.
        }

        /// <summary>
        /// This method reads the recipe ingredients entered by the user and checks if the ingredients are valid in various ways.
        /// </summary>
        /// <returns>Returns the valid ingredients for the recipe entered by the user.</returns>
        public string IngredientsRead()
        {
            //Assigning variables
            string ingredientArgs = "";
            int validator = 0;
            string ingredients = "";
            List<string> ingredientsToParse = new List<string>();
            int ingredientCount = 0;


            while (true) //Until the user enters valid ingredients the loop keeps checking the input.
            {

                ingredientArgs = display.GetIngredients().ToLower().Trim(); // Assigning the user input to the 'ingredientArgs' string.
                EscapeToMain(ingredientArgs);

                if (ingredientCount == 0 && ingredientArgs.Equals("end")) //If the user hasn't entered any ingredints and types 'end' a corresponding message is displayed for the invalid input!
                {
                    result = "Please enter atleast one ingredient.";
                    display.PrintResult(result);
                }
                
                else if (!ingredientArgs.Equals("end")) //Until the 'end' command is typed the the entered ingredients are added to the list of strings 'ingredientsToParse'.
                {

                    validator = ValidateIngredients(ingredientArgs); //Assigning the return of the ValidateIngredients(string) method to the variable 'validator'.

                    if (validator == 1) //If the entered ingredient exists in the database it's added to the list list of strings 'ingredientsToParse'.
                    {
                        result = "Successfuly added ingredient!\n";
                        ingredientCount++;
                        //If the input by the user contains any additional spaces, they are removed.
                        ingredientArgs = string.Join(" ", ingredientArgs.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                        ingredientsToParse.Add(ingredientArgs);
                    }

                    else if (validator == -1) //If the entered ingredient quantity is entered incorrectly a corresponding message is assigned to the 'result' string.
                    {
                        result = "Invalid quantity input!\n";
                    }

                    else if (validator == 0) //If the entered ingredient doesn't exist in the database a corresponding message is assigned to the 'result' string.
                    {
                        result = "Ingredient doesn't exist!\n";
                    }
                    display.PrintResult(result); //Displaying the 'result' string in the console.
                }
                else if (ingredientArgs.Equals("end"))//If the 'end' command is entered after successfully adding atleast one ingredient the process of adding ingredients to the recipe is finished.
                {
                    break;
                }
                //else 
                //{ 
                //    break;
                //}

            }

            //Displaying that the process of adding ingredients to the recipe is finished.
            result = "Finished adding ingredients!\n";
            display.PrintResult(result);

            ingredients = IngredientParse(ingredientsToParse); //Transforming the list of strings 'ingredientsToParse' to a single string.
            return ingredients; //Returns the valid parsed ingredients
        }

        /// <summary>
        /// Transforming the list of strings 'ingredientsArgs' to a single string.
        /// </summary>
        /// <param name="ingredientArgs">A list of ingredients and their quantity.</param>
        /// <returns>Returns the parsed ingredients.</returns>
        public string IngredientParse(List<string> ingredientArgs)
        {
            //Assigning variables
            StringBuilder sb = new StringBuilder();
            int counter = 0;

            //For each ingredient in the list the string builder adds the name and the quantity of the ingredient.
            //If there is more than one ingredient a ';' is added for the purpose of separation.
            foreach (var ingredient in ingredientArgs)
            {
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

            string ingredientParsed = sb.ToString().ToLower(); //Parsing the stringbuilder to string.
            return ingredientParsed; //Returning the single string which contains the ingredients
        }

        /// <summary>
        /// This method reads the recipe description entered by the user and checks if the description is valid in various ways.
        /// </summary>
        /// <returns>Returns the valid description.</returns>
        public string DescriptionRead()
        {
            //Assigning variables
            int paragraphCount = 1;
            int validator = 0;
            string description = null;
            StringBuilder sb = new StringBuilder();

            //Until the user enters a valid description the console is displaying a corresponding message and checks again.
            do
            {
                //Assigning the user input to the 'paragraph' string. 
                string paragraph = display.GetDescription();
                EscapeToMain(paragraph.Trim().ToLower());

                validator = ValidateDescription(paragraph, paragraphCount);

                //If the description is valid a corresponding message is assigned to the 'result' string.
                if (validator == 1)
                {
                    result = "Successfully added description!";
                }
                //If the description is invalid a corresponding message is assigned to the 'result' string.
                else if (validator == -1 || validator == 0) {
                    result = "Please enter a valid paragraph.";
                    display.PrintResult(result);
                    continue;
                }

                sb.Append(paragraph + "\n");
                paragraphCount++;

            } while (validator != 1);

            //Transforming the Stringbuilder to a single string.
            description = sb.ToString();
            //Removing the last two elemtents of the string. In the case it removes the '#' and the ENTER.
            description = description.Remove(description.Length - 2);
            //Displaying the corresponding message assigned to the 'result' string.
            display.PrintResult(result);
            //Returning the valid description
            return description;
        }

        /// <summary>
        /// This method validates the recipe description entered by the user in various ways.
        /// </summary>
        /// <param name="description">A string of the entered by the user description.</param>
        /// <param name="paragraphCount">An integer showing how many paragraphs are in the description.</param>
        /// <returns>Returns a corresponding integer whether the description is valid or not.</returns>
        public int ValidateDescription(string description, int paragraphCount)
        {
            //If the entered description is empty and has no elements the method returns -1;
            if (string.IsNullOrEmpty(description))
            {
                return -1;
            }
            //If the entered description has only '#' and no other elements, the method returns 0;
            else if (description.Equals("#") && paragraphCount == 1)
            {
                return 0;
            }
            //If the entered description has '#' in the end, the method returns 1;
            else if (description.EndsWith("#"))
            {
                return 1;
            }
            return 2;
        }

        /// <summary>
        /// This method validates the recipe ingredients entered by the user in various ways.
        /// </summary>
        /// <param name="ingredient">A string of the entered by the user ingredients.</param>
        /// <returns>Returns a corresponding integer whether the ingredient is valid or not.</returns>
        public int ValidateIngredients(string ingredient)
        {
            //Assigning variables
            List<string> ingredientArguments = ingredient.Split().ToList();
            if (ingredientArguments[0] == "")
            {
                return 0;
            }
            ingredientArguments.RemoveAll(x => x == "");
            ingredientArguments.RemoveAll(x => x == " ");
            StringBuilder sb = new StringBuilder();
            int argumentCount = ingredientArguments.Count - 1;
            //Assigning the table 'Ingredients' from database to the variable 'check' transforming it to a list.
            var check = recipeContext.Ingredients.ToList();


            //Separates the words in the ingredient name by adding '-' between them.
            sb.Append(ingredientArguments[0]);
           
            for (int i = 1; i < argumentCount; i++)
            {
                sb.Append("-");
                sb.Append(ingredientArguments[i]);
            }
            //If the ingredient doesn't exist in the table 'Ingredients' in database the method returns 0;
            try
            {
                check.Single(x => x.Name.ToLower() == sb.ToString().ToLower());
                //If the ingredient quantity is invalid returns -1;
                try
                {
                    double.Parse(ingredientArguments[argumentCount]);

                }
                catch
                {
                   
                    return -1;
                }

                //If the ingredient quantity is negative number returns -1;
                if (double.Parse(ingredientArguments[argumentCount]) <= 0)
                {
                    return -1;
                }
            }
            catch
            {
                sb.Append("-" + ingredientArguments[argumentCount]);
                try
                {
                    check.Single(x => x.Name.ToLower() == sb.ToString().ToLower());
                    return -1;
                }
                catch
                {
                }
                return 0;
            }
            
          


            //After the validation the ingredient is accepted as valid and the method returns 1;
            return 1;
        }

        /// <summary>
        /// This method validates the recipe name entered by the user in various ways.
        /// </summary>
        /// <param name="recipeName">A string containing the entered name by the user.</param>
        /// <returns>Returns a corresponding integer whether the ingredient is valid or not.</returns>
        public int ValidateRecipeName(string recipeName)
        {
            //If the entered name has no elements and it's empty, the method returns -1;
            if (string.IsNullOrEmpty(recipeName)) {
                return -1;
            }

            //If the name already exists in the database, the method returns 0, if not returns 1.
            try
            {
                recipeContext.Recipes.Single(x => x.Name == recipeName);
                return 0;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// This method rates a certain recipe chosen by the user.
        /// </summary>
        public void Rate()
        {
            //Calls a method from Display class which displays the user interface of the rating menu.
            display.RatingCmdDisplay();
            //Calls a method which adds a rating to an existing recipe chosen by the user.
            AddRating();
            
            //Allows the user to return to the main menu of the program.
            display.ReturnToMainMenuScreen();
            Start();
        } 

        /// <summary>
        /// This method adds a rating to an existing recipe chosen by the user.
        /// </summary>
        public void AddRating()
        {
            //Assigning variables.
            string recipeName = null;
            int validator = 0;
            int rating = 0;

            //Until the user enters a valid recipe the loop keeps checking the input.
            do
            {
                recipeName = display.GetRecipeName().Trim().ToLower();
                EscapeToMain(recipeName);
                validator = ValidateRecipeName(recipeName);

                //If the recipe name entered by the user exists in the database, a corresponding message is assigned to the 'result' string and the loop stops.
                if (validator == 0)
                {
                    result = "Successfully found recipe!";
                    break;
                }

                //If the recipe name entered by the user has no elements or it's empty a corresponding message is assigned to the 'result'.
                else if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                //If the recipe name entered by the user doesn't exist in the database, a corresponding message is assigned to the 'result'.
                else if (validator == 1)
                {
                    result = "Such recipe doesn't exist!";
                }

                //Displaying the message in the 'result' string.
                display.PrintResult(result);

            } while (ValidateRecipeName(recipeName) != 0);

            //Until the user enters a valid rating value the loop keeps checking the input.
            do
            {
                try
                {
                    string inputRating = display.GetRating().Trim();
                    EscapeToMain(inputRating.ToLower());
                    rating = int.Parse(inputRating);
                    //If the rating in less than 0 and more than 5 a corresponding message is assigned to the 'result'.
                    if (rating < 0 || rating > 5)
                    {
                        result = "Rating must be between 0 and 5!";
                        validator = -1;
                    }

                    //If the rating in valid a corresponding message is assigned to the 'result'.
                    else
                    {
                        result = "Rating is valid.Adding to database.";
                        validator = 1;
                    }
                }

                //If the rating is not an integer a corresponding message is assigned to the 'result'
                catch
                {
                    result = "Rating must be an integer!";
                    validator = 0;
                }
                display.PrintResult(result);

            } while (validator != 1);

            //Connecting the valid recipe name to the recipe in the database table 'Recipes'.
            Recipe recipeToRate = recipeContext.Recipes.Single(x => x.Name == recipeName);
            //Assigning the rating entered by the user and the related recipe to Rating properties.
            Rating ratingToAdd = new Rating
            {
                Score = rating,
                Recipe = recipeToRate
            };

            //Adding the rating for the recipe to the 'Ratings' table in the database.
            recipeContext.Ratings.Add(ratingToAdd);
            //Saving changes to the database.
            recipeContext.SaveChanges();

            //Displaying that the rating has been added to the recipe.
            result = "Rating added to database!";
            display.PrintResult(result);
        }

        /// <summary>
        /// This method deletes an existing recipe chosen by the user.
        /// </summary>
        public void Delete()
        {
            //Displaying user interface for the Delete method.
            display.DeleteCmdDisplay();

            //Assigning variables.
            string recipeName = null;
            int validator = 0;
            Recipe recipe = new Recipe();
            StringBuilder sb = new StringBuilder();

            //Checks if the recipe exists in the database
            while (ValidateRecipeName(recipeName) != 0)
            {
                //Assigning the user input to the 'recipeName' string.
                recipeName = display.GetRecipeName().Trim().ToLower();
                EscapeToMain(recipeName);
                if (ValidateRecipeName(recipeName) != -1) //If the entered recipe name is not empty the first letter of the recipe name is made capital letter.
                {
                    //If the input by the user contains any additional spaces, they are removed.
                    recipeName = string.Join(" ", recipeName.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    recipeName = recipeName.First().ToString().ToUpper() + recipeName.Substring(1);
                }
                validator = ValidateRecipeName(recipeName);
                
                //If the recipe exists in the database a corresponding message is assigned to the 'result' string and the loop stops.
                if (validator == 0)
                {
                    result = "Successfully deleted recipe!";
                    break;
                }

                //If the recipe entered by the user has no elements or is empty a corresponding message is assigned to the 'result' string.
                else if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                //If the recipe entered by the user doesn't exist in the database a corresponding message is assigned to the 'result' string.
                else if (validator == 1)
                {
                    result = "Such recipe doesn't exist!";
                }

                //Displays the message from the 'result' string.
                display.PrintResult(result);

            }
            
                
            
            //Assigning the valid name to a Recipe property.
            recipe.Name = recipeName;
            
            recipeContext.RemoveRange(recipeContext.Ratings.Where(x => x.Recipe.Name == recipe.Name));
            //Deleting the recipe from the database.
            recipeContext.Remove(recipeContext.Recipes.Single(x => x.Name == recipe.Name));
            //Saving changes to the database.
            recipeContext.SaveChanges();
            display.PrintResult(result);
            //Allows the user to return to the main menu of the program.
            display.ReturnToMainMenuScreen();
            Start();
        }

        /// <summary>
        /// This method searches for an existing recipe chosen by the user.
        /// </summary>
        public void Search()
        {
            //Displays the user interface for the search method.
            display.SearchCmdDisplay();

            //Assigning variables.
            string name = null;
            bool continueInput = true;
            int validator = 0;

            //Until the user enters a valid recipe name, the loop keeps checking the input.
            while (continueInput == true)
            {
                //Assigning the user input to the 'name' string.
                name = display.GetRecipeName().Trim().ToLower();
                EscapeToMain(name);
                if (ValidateRecipeName(name) != -1) //If the entered recipe name is not empty the first letter of the recipe name is made capital letter.
                {
                    //If the input by the user contains any additional spaces, they are removed.
                    name = string.Join(" ", name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    name = name.First().ToString().ToUpper() + name.Substring(1);
                }
                validator = ValidateRecipeName(name);
                //If the entered recipe doesn't exist in the database a corresponding message is assigned to the 'result' string.
                if (validator == 1)
                {
                    result = "No such recipe in database!";
                }

                //If the entered recipe has no elements or it's empty a corresponding message is assigned to the 'result' string.
                else if (validator == -1) {
                    result = "Name cannot be empty!";
                }

                //If the entered recipe exists in the database a corresponding message is assigned to the 'result' string and the loop stops.
                else if (validator == 0) {
                    result = "Our turtles are searching in the database";
                    continueInput = false;
                }

                //Displays the message from the 'result' string.
                display.PrintResult(result);
            }

            //Assigning an object Recipe with an existing recipe in database.
            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == name);

            //Displaying the features of the recipe(name, rating, ingredients, description).
            result = RecipeOutput(recipe);
            display.DisplayRecipe(result);
            
            //Allows the user to return to the main menu of the program.
            display.ReturnToMainMenuScreen();
            Start();

        }

        /// <summary>
        /// This method updates an existing recipe 
        /// </summary>
        public void Update()
        {
            //Assigning variables.
            string recipeName = null;
            string[] options = { "ingredients", "name", "description" };
            string choice = null;
            int validator = 0;

            display.UpdateCmdDisplay();

            //Validates if the recipe name entered by the user is valid.
            do 
            {
                recipeName = display.GetRecipeName().ToLower().Trim();
                //If the 'escape' command is entered whenever , the user is returned to the main menu and the process is terminated.
                EscapeToMain(recipeName);
                if (ValidateRecipeName(recipeName) != -1) //If the entered recipe name is not empty the first letter of the recipe name is made capital letter.
                {
                    //If the input by the user contains any additional spaces, they are removed.s
                    recipeName = string.Join(" ", recipeName.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                    recipeName = recipeName.First().ToString().ToUpper() + recipeName.Substring(1);
                }
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

            //Gets the whole recipe from database.
            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == recipeName); 
            //Shows what the recipe contains.
            display.PrintResult(RecipeOutput(recipe)); 
            //Asking what to update.
            display.PrintResult("\nWhat do you want to update?"); 

            //Validates if the input is either ingredients, name or description.
            while (!options.Contains(choice)) 
            {
                choice = Console.ReadLine().ToLower().Trim();
                EscapeToMain(choice);
                if (options.Contains(choice))
                {
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

            //Allows the user to return to the main menu.
            display.ReturnToMainMenuScreen();
            Start();

        }

        /// <summary>
        /// This method updates the name of an existing recipe.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateRecipeName(Recipe recipe) 
        {
            //Entering a new name
            string NewRecipeName = RecipeRead();
            //Assigning the new name to the Recipe Name property.
            recipe.Name = NewRecipeName;
            //Updates the recipe name in the database.
            recipeContext.Recipes.Update(recipe);
            //Saves changes in databases so the update is not lost.
            recipeContext.SaveChanges();
        }

        /// <summary>
        /// This method updates the ingredients of an existing recipe.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateIngredients(Recipe recipe)
        {
            //Displaying the user interface for the update ingredients.
            string command = display.UpdateIngredientsScreen().Trim().ToLower();
            EscapeToMain(command);
            //Assigning the recipe ingredients to a variable.
            string ingredients = recipe.Ingredients;

            //Depending on the command a different method for update is called.
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

        /// <summary>
        /// This method updates the ingredients by editing a specific ingredient chosen by the user.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateIngredientsEdit(Recipe recipe)
        {
            //Assigning variables.
            var allIngredients = recipe.Ingredients.Split(";").ToArray();
            string[] ingredient = null;
            int index = 0;
            int ingredientNameIndex = 0;
            int ingredientQuantityIndex = 1;
            string updatedIngredient = null;
            StringBuilder sb = new StringBuilder();

            //Until a valid ingredient index is entered the loop keeps checking
            while (true)
            {
                try
                {
                    string inputIndex = display.GetIngredientIndex().Trim();
                    EscapeToMain(inputIndex.ToLower());

                    index =  int.Parse(inputIndex) - 1;
                    //Check if the index entered is in the range.
                    if (index < allIngredients.Count() && index >= 0 )
                    {
                        ingredient = allIngredients[index].Split();
                        ingredient[ingredientQuantityIndex] = display.GetQuantity().Trim();
                        EscapeToMain(ingredient[1].ToLower());
                        string check = ingredient[0] + " " + ingredient[1];
                        //Until a valid quantity is entered the loop keeps checking.
                        while (ValidateIngredients(check)!=1)
                        {
                            result = "Invalid quantity!";
                            display.PrintResult(result);
                            ingredient[ingredientQuantityIndex] = display.GetQuantity();
                            check = ingredient[0] + " " + ingredient[1];
                            if (ValidateIngredients(check) == 1)
                            {
                                result = "Updating database";
                                break;
                            }
                        }
                        break;
                    }
                }
                catch
                {
                }
                result = "Invalid input!";
                display.PrintResult(result);
            }
            //Variable keeping the information for the new ingredient
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
            result = $"Ingredient {ingredient[ingredientNameIndex]}`s quantity has been updated!";
            display.PrintResult(result);
            recipeContext.SaveChanges();


        }

        /// <summary>
        /// This method updates the ingredients by removing a specific ingredient chosen by the user.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateIngredientsRemove(Recipe recipe) {
            //Assigning the variables
            var allIngredients = recipe.Ingredients.Split(";").ToArray();
            StringBuilder sb = new StringBuilder();
            int index = 0;
            //If the recipe has only one ingredient the user cannot delete it, because a recipe cannot have zero ingredients.
            if (allIngredients.Length==1)
            {
                result = "You cannot delete ingredients because there is only one!";
                display.PrintResult(result);
                UpdateIngredients(recipe);
            }
            //Otherwise if the recipe has more than one ingredient, the user is allowed to remove.
            while (true) {
                try {
                    string inputIndex = display.GetIngredientIndex().Trim();
                    
                    EscapeToMain(inputIndex.ToLower());
                    index = int.Parse(inputIndex) - 1;
                    if (index < allIngredients.Count() && index >= 0)
                    {
                        break;
                        
                    }
                }
                catch {}
                result = "Invalid input!";
                display.PrintResult(result);
            }
            for (int i = 0; i < allIngredients.Length; i++) {
                if (i == index) {
                    continue;
                }
                sb.Append(allIngredients[i] + ";");
            }
            //Saving changes to the database.
            recipe.Ingredients = sb.ToString().Remove(sb.Length - 1);
            recipe.Calories = CalculateCalories(recipe.Ingredients);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();

            result = $"Ingredient was removed.";
            display.PrintResult(result);
        }

        /// <summary>
        /// This method updates the ingredients by removing a specific ingredient chosen by the user.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateIngredientsAdd(Recipe recipe)
        {
            string ingredients = IngredientsRead();
            StringBuilder sb = new StringBuilder();
            sb.Append(recipe.Ingredients + ";" + ingredients);
            recipe.Ingredients = sb.ToString();
            recipe.Calories = CalculateCalories(recipe.Ingredients);
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();

        }

        /// <summary>
        /// This method updates the description of an existing recipe chosen by the user.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        public void UpdateDescription(Recipe recipe) {
            string description = DescriptionRead();
            recipe.Description = description;
            recipeContext.Recipes.Update(recipe);
            recipeContext.SaveChanges();
        }

        /// <summary>
        /// This method displays all the features of an existing recipe
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        /// <returns>Returns all the features of an existing recipe transformed into a string.</returns>
        public string RecipeOutput(Recipe recipe)
        {
            //Assigning variables
            string ingredients = null;
            string description = null;
            StringBuilder sb = new StringBuilder();

            //Assigning the properties of the chosen recipe to the variables.
            ingredients = recipe.Ingredients;
            description = recipe.Description;

            //Calculating the calories for the recipe
            double rating = CalculateRating(recipe);

            //Adding each feature of the recipe to the string builder
            sb.Append("\nName: " + recipe.Name + $"  Rating: {rating:F2}" + "\n" + "\nIngredients:\n");
            ingredients = IngredientsToPlainText(ingredients);
            sb.Append(ingredients + "\n");
            sb.Append("Description:\n" + recipe.Description + $"\n\nCalories for this recipe are: {recipe.Calories:F2}");

            //Returning the transformed result of the StringBuilder
            return sb.ToString();
        }

        /// <summary>
        /// This method calculates the rating of an existing recipe.
        /// If there's more than one rating for a single recipe the method calculates the average.
        /// </summary>
        /// <param name="recipe">The recipe chosen by the user.</param>
        /// <returns>Returns the average of ratings for a recipe chosen by the user.</returns>
        public double CalculateRating(Recipe recipe)
        {
            double rating = 0;

            var ratings = recipeContext.Ratings.Where(x => x.Recipe.Name == recipe.Name).ToArray();
            rating = ratings.Sum(x => x.Score);
            rating /= ratings.Length;
            
            return rating;
        }

        /// <summary>
        /// This method calculates the calories of an existing recipe.
        /// </summary>
        /// <param name="ingredients">Ingredients in the recipe.</param>
        /// <returns>Returns the sum of the calories of all ingredients in the recipe.</returns>
        public double CalculateCalories(string ingredients)
        {
            //Assigning variables.
            Ingredient checkIngredientInDatabase = new Ingredient();
            double calories = 0;
            int ingredientNameIndex = 0;
            int ingredientQuantityIndex = 1;

            //Separates the ingredients.
            var allIngredients = ingredients.Split(";").ToArray(); 

            //For each ingredient in the recipe, its calories is taken and multiplied by its quantity in the recipe.
            //After calclating for one ingredient the loop calculates for the rest as the already calculated calories are added in a sum variable 'calories'.
            foreach (var ingredient in allIngredients) {
                var ingredientAndQuantity = ingredient.Split().ToArray();
                string ingredientName = ingredientAndQuantity[ingredientNameIndex];
                double ingredientQuantity = double.Parse(ingredientAndQuantity[ingredientQuantityIndex]);

                checkIngredientInDatabase = recipeContext.Ingredients.Single(x => x.Name == ingredientName);
                calories += (checkIngredientInDatabase.Calories * ingredientQuantity) / 100;
            }

            //Returning the total calories for the recipe.
            return calories;
        }

        /// <summary>
        /// This method takes the ingredients and puts them in a specific context of display.
        /// </summary>
        /// <param name="ingredients">Ingredients in the recipe.</param>
        /// <returns>Returns the ingredients in a specific context of display.</returns>
        public string IngredientsToPlainText(string ingredients)
        {
            //Assigning variables.
            List<string> allIngredients = ingredients.Split(";").ToList();
            StringBuilder sb = new StringBuilder();
            int i = 1;

            //Each ingredient in the string 'ingredients' is put in a specific context using StringBuilder
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

            //Returns the specific context from the StringBuilder as a string.
            return sb.ToString();
        }

        /// <summary>
        /// This method shows the top five recipes with sorted by rating.
        /// </summary>
        public void Top5()
        {
            //Assigning the variables.
            var recipes = recipeContext.Recipes;
            StringBuilder sb = new StringBuilder();
            Dictionary<string, double> recipesAndRating = new Dictionary<string, double>();

            //Every recipe in the table Recipes in the database has its rating calculated and added to the dictionary.
            foreach (var recipe in recipes) {
                string recipeName = recipe.Name;
                double rating = CalculateRating(recipe);
                recipesAndRating.Add(recipeName, rating);
            }

            //The dictionary is ordered by descending to the best 5 rating recipes are shown.
            recipesAndRating = recipesAndRating.OrderByDescending(x => x.Value).Take(5).ToDictionary(x => x.Key, x => x.Value);

            //Displaying the top 5 recipes in the dictionary.
            foreach (var recipe in recipesAndRating) {
                string recipeName = recipe.Key;
                double rating = recipe.Value;
                sb.Append("- " + recipeName + ", Rating: " + $"{rating:F2}" + Environment.NewLine);
            }
            display.DisplayTop5(sb.ToString());

            //Allows the user to return to the main menu.
            display.ReturnToMainMenuScreen();
            Start();
        }

        /// <summary>
        /// This method shows all the recipes in the database.
        /// </summary>
        public void ShowAll()
        {
            //Assigning variables.
            var allRecipes = recipeContext.Recipes.ToArray();
            StringBuilder output = new StringBuilder();
            int counter = 1;

            //Displays all recipes in the database.
            foreach (var recipe in allRecipes) {
                string recipeName = recipe.Name;
                output.Append(counter + ". " + recipeName + Environment.NewLine);
                counter++;
            }
            display.DisplayAllRecipes(output.ToString());

            //Allows the user to return to the main menu.
            display.ReturnToMainMenuScreen();
            Start();
        }

        /// <summary>
        /// This method allows the user to terminate any process he is in and return to the main menu.
        /// </summary>
        /// <param name="command">The command entered by the user.</param>
        private void EscapeToMain(string command)
        {
            if (command == "escape")
            {
                result = "Process terminated.";
                display.PrintResult(result);
                display.ReturnToMainMenuScreen();
                Start();
            }
        }
    }
    
}