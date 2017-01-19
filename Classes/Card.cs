using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame
{
    public class Card
    {
        public int Suit { get; private set; }
        public int Rank { get; private set; }
        public string SuitLetter { get; private set; }
        public string RankLetter { get; private set; }
        public CardStatus Status { get; private set; }

        public string CardName
        {
            get
            {
                return this.RankLetter + this.SuitLetter;
            }
        }

        public Card (int suit, int rank)
        {
            this.Suit = suit;
            this.Rank = rank;
            this.Status = CardStatus.NotYetSet;

            string[] RankLetters = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K" };
            string[] SuitLetters = { "d", "c", "h", "s" };

            if (rank <= 12 && rank >= 0)
            {
                this.RankLetter = RankLetters[rank];
            }
            else throw new ArgumentOutOfRangeException("Rank must be between 0 and 12");

            if (suit <= 3 && suit >= 0)
            {
                this.SuitLetter = SuitLetters[suit];
            }
            else throw new ArgumentOutOfRangeException("Suit must be between 0 and 3");
        }

        public void SetStatusToPlayer()
        {
            this.Status = CardStatus.Player;
        }

        public void SetStatusToFlop()
        {
            this.Status = CardStatus.Flop;
        }

        public void SetStatusToTurn()
        {
            this.Status = CardStatus.Turn;
        }

        public void SetStatusToRiver()
        {
            this.Status = CardStatus.River;
        }

        
    }
}
