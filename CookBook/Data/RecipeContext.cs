using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CookBook.Data;

namespace CookBook
{
    /// <summary>
    /// This class is used for defining the tables of the database and connecting to it.
    /// </summary>
    public class RecipeContext:DbContext
    {
        /// <summary>
        /// This is the empty constructor of the class.
        /// </summary>
        public RecipeContext()
        {
                
        }

        /// <summary>
        /// This is the constructor of the class.
        /// </summary>
        /// <param name="options">The options which DbContext provides.</param>
        public RecipeContext(DbContextOptions options):base(options)
        {

        }
        /// <summary>
        /// This is the 'Recipes' table of the database.
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }

        /// <summary>
        /// This is the 'Ingredients' table of the database.
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// This is the 'Ratings' table of the database.
        /// </summary>
        public DbSet<Rating> Ratings { get; set; }

        /// <summary>
        /// This is the configuration method for the database.
        /// </summary>
        /// <param name="optionsBuilder">The optionsBuilder which DbContext provides.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.CONNECTION_STRING);
            }
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// This is the modelcreating method for the database.
        /// </summary>
        /// <param name="modelBuilder">The variable through which the ModelBuilder provides with its functionalities.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
