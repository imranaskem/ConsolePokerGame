using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame.Interfaces
{
    public interface IGameTracker
    {
        Deck Cards { get; }
        int ActionOnPlayer { get; }
        int MainPot { get; }
        int CurrentBet { get; }
        List<IPlayer> Players { get; }
        Card[] Board { get; }
        string Flop { get; }
        string Turn { get; }
        string FullBoard { get; }
        int MinRaiseSize { get; }        

        void DealToPlayers();
        void Say(int index);
        void Action();
        void DealFlop();
        void DealTurn();
        void DealRiver();
        void BlindsIn();
        void SetCurrentBet(int bet);
        void AddFlop(Card firstcard, Card secondcard, Card thirdcard);
        void AddTurn(Card turn);
        void AddRiver(Card river);
    }
}
