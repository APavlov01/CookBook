using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookBook;

namespace Tests
{
    [TestFixture]
    public class ControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Tests for all cases in ValidateRecipeName function in Controller
        [Test]
        public void ValidateRecipeNameTestNotInDataBase()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("ehe");
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateRecipeNameTestEmpty()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateRecipeNameTestContainsInDatabase()
        {
            Controller controller = new Controller();
            var result = controller.ValidateRecipeName("soup");
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }


        //Tests for all cases in ValidateDescription function in Controller
        [Test]
        public void ValidateDescriptionTestEmpty()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("",0);
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateDescriptionTestStartsInvalid()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("#", 1);
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateDescriptionTestValidInput()
        {
            Controller controller = new Controller();
            var result = controller.ValidateDescription("Put the spaghetti in the watter!#", 1);
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }


        //Tests for all cases in ValidateIngredients function in Controller
        [Test]
        public void ValidateIngredientsTestInvalidIngredient()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomat 4");
            var expectedresult = 0;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateIngredientsTestInvalidQuantity()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato -4");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateIngredientsTestInvalidQuantity2()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato ehe");
            var expectedresult = -1;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void ValidateIngredientsTestValid()
        {
            Controller controller = new Controller();
            var result = controller.ValidateIngredients("tomato 5");
            var expectedresult = 1;
            Assert.AreEqual(expectedresult, result);
        }


        //Tests for calculations
        [Test]
        public void CalculateRatingTest()
        {
            Controller controller = new Controller();
            RecipeContext recipeContext = new RecipeContext();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            Rating rating = new Rating(4, recipe);
            var result = controller.CalculateRating(recipe);
            var expectedresult = 4;
            Assert.AreEqual(expectedresult, result);
        }
        [Test]
        public void CalculateCaloriesTest()
        {
            Controller controller = new Controller();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            var result = controller.CalculateCalories(recipe.Ingredients);
            var expectedresult = 0.54;
            Assert.AreEqual(expectedresult, result);
        }


        //Test if the ingredients transfer to plain text
        [Test]
        public void IngredientsToPlainTextTest()
        {
            Controller controller = new Controller();
            Recipe recipe = new Recipe("egg", "tomato 3", "description#");
            var result = controller.IngredientsToPlainText(recipe.Ingredients);
            var expectedresult = "1 tomato 3 g\n";
            Assert.AreEqual(expectedresult, result);
        }


        //Test if the ingredients are in the format "<ingredient1-ingredient1substring> <quantity>;<ingredient2-ingredient2substring> <quantity>;..."
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


        //Test if the recipe displays its features correctly
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