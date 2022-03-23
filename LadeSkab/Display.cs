using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkab.Interfaces;

namespace LadeSkab
{
    public class Display : IDisplay
    {
        public string LogResult { get; set; } = "";
        public void WriteLine(string message)
        {
            LogResult = message;
            Console.WriteLine(message);
        }

        
    }
}
