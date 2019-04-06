using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    /// <summary>
    /// This class is for defining the features of a rating.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// The ID of a rating assigned as primary key in the database.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Тhe score of a rating ranging from 0 to 5.
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The reference to the rated recipe in the 'Recipes' table of the database.
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// The constructor of the class.
        /// </summary>
        /// <param name="score">Тhe score of the rating.</param>
        /// <param name="recipe">The rated recipe.</param>
        public Rating(double score, Recipe recipe)
        {
            this.Score = score;
            this.Recipe = recipe;
        }

        /// <summary>
        /// The empty constructor of the class.
        /// </summary>
        public Rating()
        {

        }

    }
}
