using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Data
{
    /// <summary>
    /// This class is used for connecting to the database of the application.
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// This string contains a command line used to connect to the database.
        /// </summary>
        public const string CONNECTION_STRING = 
            "Server=.\\SQLEXPRESS;Database=CookBook;Integrated Security=true";

    }
}
