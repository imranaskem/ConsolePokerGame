using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class NotEnoughChipsException : InvalidOperationException
    {
        public enum Reason { PlayerDoesNotHaveEnoughChips, RaiseNotBigEnough, InputIncorrect };

        public Reason reason { get; private set; }              

        public NotEnoughChipsException(Reason reason) : base()
        {
            this.reason = reason;
        }

        public NotEnoughChipsException(string message, Reason reason) : base(message)
        {
            this.reason = reason;
        }

        public NotEnoughChipsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
