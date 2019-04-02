using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Controller
    {
        private Display display = new Display();
        private RecipeContext recipeContext = new RecipeContext();
        private string command;
        private string result;
        private double caloriesPerRecipe = 0;
        public Controller()
        {

        }
        
        public void Start()
        {
            display.WelcomeScreen();
            command=display.GetCommand();
            if(command=="add"){ Console.Clear(); Add();}
            else if (command == "rate") { Console.Clear();Rate(); }
            else if (command == "delete") { Console.Clear();Delete(); }
            else if (command == "search") { Console.Clear(); Search(); }
            else if (command == "update") { Console.Clear(); Update(); }
        }
        private void Add()
        {
            string recipeName = null;
            string ingredients = null;            
            string description = null;
            

            display.AddCmdDisplay();
            recipeName = RecipeRead();
            ingredients = IngredientsRead();            
            description = DescriptionRead();


            Recipe recipe = new Recipe
            {
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

        private string RecipeRead()
        {
            string recipeName = null;
            int validator = 0;

            do
            {
                recipeName = display.GetRecipeName().ToLower();
                recipeName= recipeName.First().ToString().ToUpper()+ recipeName.Substring(1);

                validator = ValidateRecipeName(recipeName);

                if (validator == 0)
                {
                    result = "Name already used!";
                }

                else if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1)
                {
                    result = "Successfully added recipe name!\n";
                    break;
                }

                display.PrintResult(result);
                //TO DO display result -karatov =====> DONE

            } while (ValidateRecipeName(recipeName) != 1);

            display.PrintResult(result);

            return recipeName;
        }

        private string IngredientsRead()
        {
            string ingredientArgs = "";
            int validator = 0;
            bool continueReading = true;
            string ingredients = "";
            List<string> ingredientsToParse = new List<string>();

            int ingredientCount = 0;

            do
            {

                ingredientArgs = display.GetIngredients();

                if (ingredientCount == 0 && ingredientArgs.ToLower().Equals("end"))
                {
                    result = "Please enter atleast one ingredient.";
                    display.PrintResult(result);
                }

                else if (!ingredientArgs.ToLower().Equals("end"))
                {

                    validator = ValidateIngredients(ingredientArgs);

                    if (validator == 1)
                    {
                        result = "Successfuly added ingredient!\n";
                        ingredientCount++;
                        ingredientsToParse.Add(ingredientArgs);
                        //var ingredient = IngredientParsing(ingredientArgs);
                        //ingredients.Add(ingredient);
                    }

                    else if (validator == -1)
                    {
                        result = "Invalid quantity input!\n";
                    }

                    else if (validator == 0)
                    {
                        result = "Ingredient doesn't exist!\n";
                    }
                    display.PrintResult(result);
                }

                else
                {
                    continueReading = false;
                    break;
                }

            } while (continueReading);

            result = "Finished adding ingredients!\n";

            display.PrintResult(result);
            ingredients = IngredientParse(ingredientsToParse);
            return ingredients;  //TO DO implement IngredientParsing() method 
        }

        private string IngredientParse(List<string> ingredientArgs)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            foreach (var ingredient in ingredientArgs)
            {
                List<string> ingredientToParse = ingredient.Split().ToList();
                int argumentsCount = ingredientToParse.Count - 1;
                for (int i = 0; i < argumentsCount; i++)
                {
                    sb.Append(ingredientToParse[i]);
                    if (i==argumentsCount-1)
                    {
                        break;
                    }
                    sb.Append("-");
                }
                counter++;
                sb.Append("|" + ingredientToParse[argumentsCount]);
                if(ingredientArgs.Count-counter>0)
                { sb.Append(";");
                }

            }
            string ingredientParsed = sb.ToString().ToLower();
            return ingredientParsed;
        }

        private string DescriptionRead()
        {
            int paragraphCount = 1;
            int validator = 0;
            string description = null;

            StringBuilder sb = new StringBuilder();
            do
            {
                string paragraph = display.GetDescription();

                validator = ValidateDescription(paragraph, paragraphCount);
                
                if (validator == 1)
                {
                    result = "Successfully added description!";
                }
                else if (validator == -1 || validator == 0)
                {
                    result = "Please enter a valid paragraph.";
                    display.PrintResult(result);
                    continue;
                }

                sb.Append(paragraph + "\n");
                paragraphCount++;

            } while (validator != 1);

            description = sb.ToString();
            description=description.Remove(description.Length - 2);
            display.PrintResult(result);

            return description;
        }

        private int ValidateDescription(string description, int paragraphCount)
        {
            if (string.IsNullOrEmpty(description))
            {
                return -1;
            }
            else if (description.Equals("#") && paragraphCount == 1)
            {
                return 0;
            }
            else if (description.EndsWith("#"))
            {
                return 1;
            }

            return 2;
        }

        private int ValidateIngredients(string ingredient)
        {
            List<string> ingredientArguments = ingredient.Split().ToList();
            StringBuilder sb = new StringBuilder();
            int argumentCount = ingredientArguments.Count - 1;
            for (int i = 0; i < argumentCount; i++)
            {
                sb.Append(ingredientArguments[i]); // eggs-yolk
                if (i == argumentCount - 1)
                {
                    break;
                }
                sb.Append("-");
            }
            var check = recipeContext.Ingredients.ToList();
            try
            {
                check.Single(x => x.Name.ToLower() == sb.ToString().ToLower());
            }
            catch
            {
                return 0;
            }
            try
            {
                double.Parse(ingredientArguments[argumentCount]);
            }
            catch
            {
                return -1;
            }

            if (double.Parse(ingredientArguments[argumentCount]) <= 0)
            {
                return -1;
            }
            
            
            return 1;
        }

        private int ValidateRecipeName(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName))
            {
                return -1;
            }
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

        private void Rate()
        {
            display.RatingCmdDisplay();
            AddRating();
            display.PrintResult("\nPress ENTER to go to the main menu");
            Console.ReadKey();
            Console.Clear();
            Start();
        }

        private void AddRating()
        {
            string recipeName = null;
            int validator = 1;
            
            do
            {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0)
                {
                    result = "Successfully found recipe!";
                    break;
                }

                if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1)
                {
                    result = "Such recipe doesn't exist!";
                }
                
               
                display.PrintResult(result);

            } while (ValidateRecipeName(recipeName) != 0);
            int rating = 0;
            do
            {
                
                try
                {
                    rating = display.GetRating();
                    if(rating<0||rating>5)
                    {
                        result = "Rating must be between 0 and 5!";
                        validator = -1;
                    }
                    else
                    {
                        result = "Rating is valid.Adding to database.";
                        validator = 1;
                    }

                }
                catch 
                {
                    result = "Rating must be an integer!";
                    validator = 0;
                }
                display.PrintResult(result);

            } while (validator != 1);

            Recipe recipeToRate= recipeContext.Recipes.Single(x=>x.Name==recipeName);
            Rating ratingToAdd = new Rating();
            ratingToAdd.Score = rating;
            ratingToAdd.Recipe =recipeToRate;
            recipeContext.Ratings.Add(ratingToAdd);
            recipeContext.SaveChanges();
            result = "Rating added to database!";
            display.PrintResult(result);
        }

        private void Delete()
        {
            display.DeleteCmdDisplay();

            string recipeName = null;

            int validator = 0;

            Recipe recipe = new Recipe();

            StringBuilder sb = new StringBuilder();

            do
            {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0)
                {
                    result = "Successfully deleted recipe!";
                    break;
                }

                if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1)
                {
                    result = "Such recipe doesn't exist!";
                }

                display.PrintResult(result);

            } while (ValidateRecipeName(recipeName) != 0);

            recipe.Name = recipeName;
            recipeContext.Remove(recipeContext.Recipes.Single(x => x.Name == recipe.Name));
            recipeContext.SaveChanges();

            sb.Append(result + "\n" + "Press ENTER to go to the main menu");
            display.PrintResult(sb.ToString());

            Console.ReadKey();
            Console.Clear();
            Start();
        }

        private void Search()
        {
            display.SearchCmdDisplay();
            string name = null;
            bool continueInput = true;
            while (continueInput == true)
            {
                name = display.GetRecipeName();
                if (ValidateRecipeName(name) == 1)
                {
                    result = "No such recipe in database!";
                }
                else if (ValidateRecipeName(name) == -1)
                {
                    result = "Name cannot be empty!";
                }
                else if (ValidateRecipeName(name) == 0)
                {
                    result = "Our turtles are searching in the database";
                    continueInput = false;
                }
                display.PrintResult(result);
            }

            Console.Clear();

            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == name);

            result = RecipeOutput(recipe);
            caloriesPerRecipe = 0;
            display.PrintResult(result);
            display.PrintResult("\nPress ENTER to go to the main menu");
            Console.ReadKey();
            Console.Clear();
            Start();

        }

        private void Update()
        {
            display.UpdateCmdDisplay();
            string recipeName = null;
            string[] options = {"ingredients", "name", "description"};
            string choice = null;
            int validator = 0;
            do
            {
                recipeName = display.GetRecipeName().ToLower();

                validator = ValidateRecipeName(recipeName);

                if (validator == 0)
                {
                    result = "Successfully found recipe!";
                }

                if (validator == -1)
                {
                    result = "Name cannot be empty!";
                }

                else if (validator == 1)
                {
                    result = "Such recipe doesn't exist!";
                }

                display.PrintResult(result);

            } while (validator != 0);//Validation

            Recipe recipe = recipeContext.Recipes.Single(x => x.Name == recipeName); // Gets the whole recipe from database
            display.PrintResult(RecipeOutput(recipe)); //Shows what the recipe contains
            display.PrintResult("\nWhat do you want to update?"); //Asking what to update
            
            do
            {
                choice = Console.ReadLine();
                if(options.Contains(choice.ToLower()))
                {
                    break;
                }
                display.PrintResult("Invalid input!");
            } while (!options.Contains(choice.ToLower())); //Validates if the input is either ingredients, name or description

            string NewRecipeName = null;
            string ingredients = null;
            string description = null;
            if(choice.ToLower() == "ingredients") // Updates ingredients if the choice is "ingredients"
            {
                display.PrintResult("Enter new ingredients:");
                ingredients = IngredientsRead();
                recipe.Ingredients = ingredients;
                recipeContext.Recipes.Update(recipe);
                recipeContext.SaveChanges();
            }
            if (choice.ToLower() == "name") // Updates ingredients if the choice is "name"
            {
                display.PrintResult("Enter new name:");
                NewRecipeName=RecipeRead();
                recipe.Name = NewRecipeName;
                recipeContext.Recipes.Update(recipe);
                recipeContext.SaveChanges();
            }
            if (choice.ToLower() == "description")
            {
                display.PrintResult("Enter new description:"); // Updates ingredients if the choice is "description"
                description = DescriptionRead();
                recipe.Description = description;
                recipeContext.Recipes.Update(recipe);
                recipeContext.SaveChanges();
            }
            display.PrintResult("\nPress ENTER to go to the main menu"); //Returns to main menu
            Console.ReadKey();
            Console.Clear();
            Start();

        }

        private string RecipeOutput(Recipe recipe)
        {
            string ingredients = null;
            string description = null;

            ingredients = recipe.Ingredients;
            description = recipe.Description;
            StringBuilder sb = new StringBuilder();
            double rating = 0;
            int ratingsCounter = 0;
            foreach ( Rating ratingInDB in recipeContext.Ratings)
            {
                if(ratingInDB.Recipe==recipe)
                {
                    rating += ratingInDB.Score;
                    ratingsCounter++;
                }
                
            }
            rating /= ratingsCounter;
            sb.Append("\nName: "+ recipe.Name + $"  Rating: {rating:F2}" +"\n" + "\nIngredients:\n");
            ingredients = IngredientsToPlainText(ingredients);
            sb.Append(ingredients + "\n");
            description = recipe.Description;
            
            sb.Append("Description:\n" + description +$"\n\nCalories for this recipe are: {caloriesPerRecipe:F2}");


            return sb.ToString();
        }

        private string IngredientsToPlainText(string ingredients)
        {
            List<string> allIngredients = ingredients.Split(";").ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var ingredient in allIngredients)
            {
                List<string> ingredientAndQuantity = ingredient.Split("|").ToList();
                sb.Append("- " + ingredientAndQuantity[0] + " " + ingredientAndQuantity[1] + " ");
                Ingredient check = new Ingredient();
                            check.Name = ingredientAndQuantity[0];
                string type = null;
                foreach (Ingredient checkInDatabase in recipeContext.Ingredients)
                {
                    if(check.Name==checkInDatabase.Name)
                    {
                        check = checkInDatabase;
                        caloriesPerRecipe += (check.Calories * double.Parse(ingredientAndQuantity[1])) / 100;
                        break;
                        
                    }
                    
                }
                type = check.Type;
                sb.Append(type + "\n");
                
            }
            string ingredientsInPLainText = sb.ToString();

            return ingredientsInPLainText;
        }
    }
}
