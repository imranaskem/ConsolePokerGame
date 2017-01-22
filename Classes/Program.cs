using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Classes;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame
{
    class Program
    {       
        static void Main(string[] args)
        {           
            ConsoleWrapper console = new ConsoleWrapper();

            GameTracker gameTracker = new GameTracker(console);

            //Setup

            gameTracker.BlindsIn();

            gameTracker.DealToPlayers();

            //Pre-flop
            gameTracker.RoundOfAction(Position.UnderTheGun);

            //Flop
            gameTracker.RoundOfAction(Position.SmallBlind);

            //Turn
            gameTracker.RoundOfAction(Position.SmallBlind);

            //River
            gameTracker.RoundOfAction(Position.SmallBlind);

        }
    }
}
