using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame.Interfaces
{
    public interface IGameTracker
    {
        Deck Cards { get; }
        int ActionOnPlayer { get; }
        int MainPot { get; }
        int CurrentBet { get; }
        int MinRaiseSize { get; }   
        int NumberOfPlayers { get; }     
        List<IPlayer> Players { get; }
        List<Card> Board { get; }        
        string FullBoard { get; }

        void DealToPlayers();
        void Say(int index);
        void RoundOfAction(Position startingPlayer);
        void DealFlop();
        void DealTurnOrRiver();        
        void BlindsIn();             
    }
}
