using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    /// <summary>
    /// This class is for defining the features of a recipe.
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// The ID of an ingredient assigned as primary key in the database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of an ingredient.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The ingredients containing in the recipe.
        /// </summary>
        public string Ingredients { get; set; }

        /// <summary>
        /// The description of the recipe.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The calories the recipe contains.
        /// </summary>
        public double Calories { get; set; }

        /// <summary>
        /// The empty constructor of the class.
        /// </summary>
        public Recipe()
        {

        }

        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="name">The name of a recipe.</param>
        /// <param name="ingredients">The ingredients of a recipe.</param>
        /// <param name="description">The description of a recipe.</param>
        public Recipe(string name, string ingredients, string description)
        {
            this.Name = name;
            this.Ingredients = ingredients;
            this.Description = description;
        }
    }
}
