using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class ConsoleWrapper : IConsole
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public int GetNumberInput()
        {
            int amount;

            var response = this.ReadLine();

            if (!int.TryParse(response, out amount))
            {
                throw new TextInputIncorrectException("That is not a number, please try again");
            }

            return amount;
        }
    }
}
