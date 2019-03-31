using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public Recipe()
        {

        }
        public Recipe(string name, string ingredients, string description)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.Description = description;
        }
    }
}
