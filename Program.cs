using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    class Program
    {
        static void Main(string[] args)
        {           
            ConsoleWrapper console = new ConsoleWrapper();

            Table table = new Table(console);

            Dealer dealer = new Dealer();            

            //Pre-flop

            table.BlindsIn(console, dealer);

            dealer.DealToPlayers(table, console);

            //Flop

            //Turn

            //River
        }
    }
}
