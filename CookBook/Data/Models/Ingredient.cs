using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    /// <summary>
    /// This class is for defining the features of a single ingredient.
    /// </summary>
    public class Ingredient
    {
        //Assigning properties.
        /// <summary>
        /// The ID of an ingredient assigned as primary key in the database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// The name of an ingredient with max length of 50 characters.
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The type of the quantity of the ingredient.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The calories the ingredient contains.
        /// </summary>
        public double Calories { get; set; }

        /// <summary>
        /// The empty constructor of the class.
        /// </summary>
        public Ingredient()
        {

        }

        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="name">Name of the ingredient.</param>
        /// <param name="type">Type of the quantity of the ingredient.</param>
        /// <param name="calories">Calories of the ingredient.</param>
        public Ingredient(string name, string type, double calories)
        {
            this.Calories = calories;
            this.Name = name;
            this.Type = type;
        }
    }
}
