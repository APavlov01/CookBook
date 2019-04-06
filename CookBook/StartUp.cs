using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using CookBook.Data;

namespace CookBook
{
    /// <summary>
    /// This class is used to start the program.
    /// </summary>
    public class StartUp
    { 
        public static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Start();
        }
    }
}
