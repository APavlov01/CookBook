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
            else if (command == "delete") { Console.Clear();Delete(); }
            else if (command == "delete") { Console.Clear(); Search(); }
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
            display.RatingCmdDisplay();
            display.GetRecipeName();
            display.GetRating();
        }
        public void Delete()
        {
            display.DeleteCmdDisplay();
            display.GetRecipeName();
        }
        public void Search()
        {
            display.SearcheCmdDisplay();
            display.GetRecipeName();
        }
    }
}
