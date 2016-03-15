using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public interface IDealer
    {    
        Deck Cards { get; }
        int ActionOnPlayer { get; }

        void DealToPlayers(ITable table, IConsole console);        
        string Say(int index);
        void Action(IPlayer player, ITable table, IConsole console);
        void DealFlop(ITable table, IConsole console);
        void DealTurn(ITable table, IConsole console);
        void DealRiver(ITable table, IConsole console);
    }
}
