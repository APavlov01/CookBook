using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        public double Score { get; set; }
        public Recipe Recipe { get; set; }
        public Rating(double score, Recipe recipe)
        {
            this.Score = score;
            this.Recipe = recipe;
        }
        public Rating()
        {

        }

    }
}
