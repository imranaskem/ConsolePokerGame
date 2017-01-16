using ConsolePokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame.Tests
{
    public class TestConsoleWrapper : IConsole
    {
        public List<string> LinesToRead
        {
            get; set;
        }

        public TestConsoleWrapper(List<string> testlines)
        {
            this.LinesToRead = testlines;
        }

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
            string result = LinesToRead[0];
            LinesToRead.RemoveAt(0);
            return result;
        }

        public int GetNumberInput()
        {
            int amount;

            var response = this.ReadLine();

            if (!int.TryParse(response, out amount))
            {
                throw new NotEnoughChipsException(
                    "That is not a number, please try again",
                    NotEnoughChipsException.Reason.InputIncorrect);
            }

            return amount;
        }
    }
}
