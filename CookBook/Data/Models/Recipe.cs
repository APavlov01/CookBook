using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    class Recipe
    {
        public string Name { get; set; }
        public Ingredient MainIngredient { get; set; }
        public string Description { get; set; }
        public Recipe()
        {

        }
        public Recipe(string name, Ingredient ingredient, string description)
        {
            this.Name = name;
            this.MainIngredient = ingredient;
            this.Description = description;
        }
    }
}
