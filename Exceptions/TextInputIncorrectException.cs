using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class TextInputIncorrectException : InvalidOperationException
    {
        public TextInputIncorrectException() : base()
        {

        }

        public TextInputIncorrectException(string message) : base(message)
        {

        }

        public TextInputIncorrectException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
