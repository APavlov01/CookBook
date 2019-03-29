using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBook
{
    public class Controller
    {
        Display display = new Display();
        private string command;
        public Controller()
        {

        }
        
        public void Start()
        {
            display.WelcomeScreen();
            command=display.GetCommand();
            if(command=="add"){ Console.Clear(); Add();}
            else if (command == "rate") { Console.Clear();Rate(); }
            //TODO: command doesnt work after invalid output
            //TODO: SQL queries
        }

        public void Add()
        {
            display.AddCmdDisplay();
            display.GetRecipeName();
            display.GetIngredients();
            display.GetDescription();
            
        }
        public void Rate()
        {
            display.AddRatingDisplay();
        }
    }
}
