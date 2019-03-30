using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CookBook
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public string Type { get; set; }
        public double Calories { get; set; }
        public Ingredient()
        {

        }
        public Ingredient(string name, string type, double calories)
        {
            this.Calories = calories;
            this.Name = name;
            this.Type = type;
        }
    }
}
