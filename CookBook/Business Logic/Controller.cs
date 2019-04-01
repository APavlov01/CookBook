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
            //TODO: command doesnt work after invalid output
            //TODO: SQL queries
        }

        private void Add()
        {
            string recipeName = null;
            string ingredients = null;            
            string description = null;
            

            display.AddCmdDisplay();
            recipeName = recipeRead();
            ingredients = IngredientsRead();            
            description = DescriptionRead();


            Recipe recipe = new Recipe();
            recipe.Name = recipeName;
            recipe.Ingredients = ingredients;
            recipe.Description = description;

            recipeContext.Recipes.Add(recipe);

            recipeContext.SaveChanges();
            Console.Clear();
            Start();
        }

        private string recipeRead()
        {
            string recipeName = null;
            int validator = 0;

            do
            {
                recipeName = display.GetRecipeName();

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

                else if (!ingredientArgs.Equals("end"))
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
            ingredients = ingredientParse(ingredientsToParse);
            return ingredients;  //TO DO implement IngredientParsing() method 
        }

        private string ingredientParse(List<string> ingredientArgs)
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

            try
            {
                double.Parse(ingredientArguments[argumentCount - 1]);
                return -1;

            }
            catch { }
            
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
            //TO DO ingredient validation return true; =====> DONE
            //TO DO if consists in database =====> DONE
            return 1;
        }

        private int ValidateRecipeName(string recipeName)
        {
            foreach(Recipe recipe in recipeContext.Recipes)
            {
                if (recipe.Name.ToLower()==recipeName.ToLower())
                {
                    return 0;
                }
            }
            if(string.IsNullOrEmpty(recipeName))
            {
                return -1;
            }
            else return 1;
        }

        private void Rate()
        {
            display.RatingCmdDisplay();
            display.GetRecipeName();
            display.GetRating();
        }
        private void Delete()
        {
            display.DeleteCmdDisplay();
            display.GetRecipeName();
        }

        private void Search()
        {
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

            result = recipeOutput(recipe);

            display.PrintResult(result);
            Console.ReadKey();
            Console.Clear();
            Start();
            
        }

        private string recipeOutput(Recipe recipe)
        {
            string ingredients = null;
            string description = null;

            ingredients = recipe.Ingredients;
            description = recipe.Description;
            StringBuilder sb = new StringBuilder();
            sb.Append(recipe.Name + "\n");
            ingredients = ingredientsToPlainText(ingredients);
            sb.Append(ingredients + "\n");
            description = recipe.Description;
            sb.Append(description + "\n" + "Press ENTER to go to the main menu.");

            return sb.ToString();
        }

        private string ingredientsToPlainText(string ingredients)
        {
            List<string> allIngredients = ingredients.Split(";").ToList();
            StringBuilder sb = new StringBuilder();
            foreach (var ingredient in allIngredients)
            {
                List<string> ingredientAndQuantity = ingredient.Split("|").ToList();
                sb.Append(ingredientAndQuantity[0] + " " + ingredientAndQuantity[1]+ " ");
                Ingredient check = new Ingredient();
                            check.Name = ingredientAndQuantity[0];
                string type = null;
                foreach (Ingredient checkInDatabase in recipeContext.Ingredients)
                {
                    if(check.Name==checkInDatabase.Name)
                    {
                        check = checkInDatabase;
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
