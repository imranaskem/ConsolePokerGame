using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public interface IConsole
    {
        void Write(string message);
        void WriteLine();
        void WriteLine(string message);
        string ReadLine();
    }
}
