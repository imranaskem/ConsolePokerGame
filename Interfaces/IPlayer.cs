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

        int Bet(int minRaise, bool facingABet = true);               
        int Call(int currentBet);
        IEnumerable<Card> Fold();
        void Blind(int blind);
        void Check();
        void TakeCards(Card firstcard, Card secondcard);
    }
}
