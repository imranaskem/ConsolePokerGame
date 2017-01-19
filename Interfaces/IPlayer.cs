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
        int AmountToCall { get; }
        Card[] HoleCards { get; }
        string Hand { get; }
        Position PlayerPosition { get; }

        void Bet(int bet);
        void Raise(int bet);
        void SetCall(int bet);
        void Call();
        void Fold();
        void Blind(int blind);
        void TakeCards(Card firstcard, Card secondcard);
    }
}
