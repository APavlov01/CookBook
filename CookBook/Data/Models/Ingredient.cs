using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public string Type { get; set; }
        public double Calories { get; set; }
        public Ingredient()
        {

        }
        public Ingredient(string name, double quantity, string type, double calories)
        {
            this.Calories = calories;
            this.Name = name;
            this.Quantity = quantity;
            this.Type = type;
        }
    }
}
