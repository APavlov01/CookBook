using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Controller
    {
        Display display = new Display();

        private string command;

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
            else if (command == "delete") { Console.Clear(); Search(); }
            //TODO: command doesnt work after invalid output
            //TODO: SQL queries
        }

        public void Add()
        {
            string recipeName = null;

            List<Ingredient> ingredients = new List<Ingredient>();

            string ingredientArgs = null;

            string description = null;

            display.AddCmdDisplay();
            do
            {
                recipeName = display.GetRecipeName();
                //TO DO display result -karatov

            } while (!ValidateRecipeName(recipeName));

            do
            {
                ingredientArgs = display.GetIngredients();

                if (ValidateIngredients(ingredientArgs))
                {
                    var ingredient = IngredientParsing(ingredientArgs);
                    ingredients.Add(ingredient);
                }

            } while (!ingredientArgs.ToLower().Equals("end"));

            do
            {
                description = display.GetDescription();

            } while (ValidateDescription(description));
            
        }

        private bool ValidateDescription(string description)
        {
            return !string.IsNullOrEmpty(description);
        }

        private bool ValidateIngredients(object ingredient)
        {
            //TO DO ingredient validation return true;
            throw new NotImplementedException();
        }

        private bool ValidateRecipeName(string recipeName)
        {
            return !string.IsNullOrEmpty(recipeName);
        }

        public void Rate()
        {
            display.RatingCmdDisplay();
            display.GetRecipeName();
            display.GetRating();
        }
        public void Delete()
        {
            display.DeleteCmdDisplay();
            display.GetRecipeName();
        }
        public void Search()
        {
            display.SearcheCmdDisplay();
            display.GetRecipeName();
        }

        private Ingredient IngredientParsing(string ingredientArguments)
        {
            //TO DO ingredient parsing
            return null;
        }
    }
}
