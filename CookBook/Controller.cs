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
            this.command=display.GetCommand();
        }
    }
}
