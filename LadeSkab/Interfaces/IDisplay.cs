using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkab.Interfaces
{
    public interface IDisplay
    {
        void WriteLine(string message);
        string LogResult { get; set; }
    }
}
