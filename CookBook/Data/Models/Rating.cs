using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    class Rating
    {
        public double Score { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        public Rating()
        {

        }
        public Rating(double score)
        {
            this.Score = score;
        }

    }
}
