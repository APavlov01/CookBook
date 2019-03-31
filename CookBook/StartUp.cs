using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CookBook.Data;

namespace CookBook
{
    class StartUp
    { 
        public static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Start();
            //RecipeContext context = new RecipeContext();
            //Rating rating = new Rating();
            //rating.Id = 1;
            //context.Ratings.Remove(rating);
            //context.SaveChanges();
        }
    }
}
