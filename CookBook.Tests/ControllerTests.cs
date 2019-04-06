using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook;

namespace Tests
{
    /// <summary>
    /// This class contains all the tests for the controller methods.
    /// </summary>
    [TestFixture]
    public class ControllerTests
    {
        /// <summary>
        /// Test if the recipe name is not in the database.
        /// </summary>
        [Test]
        public void ValidateRecipeNameTestNotInDataBase()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("ehe");
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the recipe name is empty.
        /// </summary>
        [Test]
        public void ValidateRecipeNameTestEmpty()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the recipe name is in the database.
        /// </summary>
        [Test]
        public void ValidateRecipeNameTestContainsInDatabase()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("soup");
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }


        /// <summary>
        /// Test if the recipe description is empty.
        /// </summary>
        [Test]
        public void ValidateDescriptionTestEmpty()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("",0);
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the recipe description starts invalid.
        /// </summary>
        [Test]
        public void ValidateDescriptionTestStartsInvalid()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("#", 1);
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the recipe description is valid
        /// </summary>
        [Test]
        public void ValidateDescriptionTestValidInput()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("Put the spaghetti in the watter!#", 1);
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }


        /// <summary>
        /// Test if the ingredient has invalid name.
        /// </summary>
        [Test]
        public void ValidateIngredientsTestInvalidIngredient()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomat 4");
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the ingredient has invalid quantity.
        /// </summary>
        [Test]
        public void ValidateIngredientsTestInvalidQuantity()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato -4");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the ingredient has invalid quantity.
        /// </summary>
        [Test]
        public void ValidateIngredientsTestInvalidQuantity2()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato ehe");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        /// <summary>
        /// Test if the ingredient has valid name and quantity.
        /// </summary>
        [Test]
        public void ValidateIngredientsTestValid()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato 5");
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }


        /// <summary>
        /// Test if the calculating rating method works correctly.
        /// </summary>
        [Test]
        public void CalculateRatingTest()
        {
            Controller controller = new Controller();
            RecipeContext recipeContext = new RecipeContext();
            Recipe recipe = new Recipe("soup", "tomato 3", "vari domati i si ti");
            Rating rating = new Rating(4, recipe);
            double result = controller.CalculateRating(recipe);
            double expectedResult = 4;
            Assert.AreEqual(expectedResult, result);
            
        }
        /// <summary>
        /// Test if the calculating calories method works correctly.
        /// </summary>
        [Test]
        public void CalculateCaloriesTest()
        {
            Controller controller = new Controller();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            var result = controller.CalculateCalories(recipe.Ingredients);
            var expectedresult = 0.54;
            Assert.AreEqual(expectedresult, result);
        }


        /// <summary>
        /// Test if the ingredients transfer to plain text.
        /// </summary>
        [Test]
        public void IngredientsToPlainTextTest()
        {
            Controller controller = new Controller();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            var result = controller.IngredientsToPlainText(recipe.Ingredients);
            var expectedresult = "1 tomato 3 g\n";
            Assert.AreEqual(expectedresult, result);
        }

        /// <summary>
        /// Test if the ingredients are in the format "<ingredient1-ingredient1substring> <quantity>;<ingredient2-ingredient2substring> <quantity>;..."
        /// </summary>
        [Test]
        public void IngredientsParsingTest()
        {
            Controller controller = new Controller();
            List<string> ingredients = new List<string>();
            ingredients.Add("tomato 3");
            ingredients.Add("olive oil 3");
            var result = controller.IngredientParse(ingredients);
            var expectedresult = "tomato 3;olive-oil 3";
            Assert.AreEqual(expectedresult, result);
        }


        /// <summary>
        /// Test if the recipe displays its features correctly.
        /// </summary>
        [Test]
        public void RecipeOutputTest()
        {
            Controller controller = new Controller();
            StringBuilder sb = new StringBuilder();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            Rating rating = new Rating((double)4, recipe);
            sb.Append("\nName: " + recipe.Name + $"  Rating: {controller.CalculateRating(recipe):F2}" + "\n" + "\nIngredients:\n");
            sb.Append($"1 {recipe.Ingredients} g" + "\n");
            sb.Append("\nDescription:\n" + recipe.Description + $"\n\nCalories for this recipe are: {recipe.Calories:F2}");
            var result = controller.RecipeOutput(recipe);
            var expectedresult = sb.ToString() ;
            Assert.AreEqual(expectedresult, result);
        }
    }
}