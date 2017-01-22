using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame
{
    public interface IPlayer
    {
        string Name { get; }
        bool InHand { get; }
        int Chips { get; }
        int AmountBet { get; }        
        Card[] HoleCards { get; }
        string Hand { get; }
        Position PlayerPosition { get; }

        int Bet(int minRaise);               
        int Call(int currentBet);
        IEnumerable<Card> Fold();
        void Blind(int blind);
        void TakeCards(Card firstcard, Card secondcard);
    }
}
