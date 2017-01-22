using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame
{
    public class Player : IPlayer
    {
        private IConsole _consoleWrap { get; set; }

        private string[] speech = { "Added their small blind of",
            "Added their big blind of",
            "Folds",
            "Called the bet of",
            "Checks",
            "Bet",
            "Raised to" };

        public Position PlayerPosition { get; private set; }
        public string Name { get; private set; }
        public bool InHand { get; private set; }
        public int Chips { get; private set; }
        public int AmountBet { get; private set; }        
        public Card[] HoleCards { get; private set; }
        public string Hand { get; private set; }

        public Player (string name, int chips, Position position, IConsole console)
        {
            this.Name = name;
            this.Chips = chips;            
            this.AmountBet = 0;
            this.InHand = true;

            this._consoleWrap = console;         

            if (Enum.IsDefined(typeof(Position), position))
            {                
                this.PlayerPosition = position;
            }
            else throw new ArgumentOutOfRangeException("position", $"Enum does not contain a value at {position}" );

            this.HoleCards = new Card[2] { null, null };          
        }       

        public int Bet(int minRaise)
        {
            this._consoleWrap.WriteLine($"Player has {this.Chips} chips remaining");
            this._consoleWrap.WriteLine($"Minimum bet size is {minRaise}");
            this._consoleWrap.WriteLine($"You can go all in regardless of the minimum bet size by inputting {this.Chips}");

            int amount = 0;

            var parsed = true;

            do
            {
                try
                {
                    amount = this.IsBetLegal(minRaise);
                    parsed = true;
                }
                catch (NotEnoughChipsException e)
                {
                    this._consoleWrap.WriteLine(e.Message);
                    parsed = false;
                }

            } while (parsed);     

            this.AmountBet = amount;
            this.Chips -= amount;

            this.Say(6, amount);

            return amount;
        }

        private int IsBetLegal(int minRaise)
        {
            int amount = this._consoleWrap.GetNumberInput();

            if (amount == this.Chips)
            {
                this._consoleWrap.WriteLine($"{this.Name} is all in");
                return amount;
            }

            if (amount > this.Chips)
            {
                throw new NotEnoughChipsException("Player does not have enough chips", NotEnoughChipsException.Reason.PlayerDoesNotHaveEnoughChips);
            }

            if (amount < minRaise)
            {
                throw new NotEnoughChipsException("Raise size too small", NotEnoughChipsException.Reason.RaiseNotBigEnough);
            }

            return amount;
        }

        public IEnumerable<Card> Fold()
        {
            this.InHand = false;
            this.AmountBet = 0;            

            var cards = this.HoleCards;

            Array.Clear(this.HoleCards, 0, 2);

            this.Say(2);

            return cards;
        }        

        public int Call(int currentBet)
        {
            int callAmount = currentBet - this.AmountBet;
            this.Chips -= callAmount;
            this.AmountBet = currentBet;

            this.Say(3, callAmount);

            return callAmount;        
        }        

        public void Blind(int blind)
        {
            this.Say((int) this.PlayerPosition, blind);
            this.Chips -= blind;
            this.AmountBet = blind;
        }

        public void TakeCards(Card firstcard, Card secondcard)
        {
            this.HoleCards[0] = firstcard;
            this.HoleCards[1] = secondcard;

            this.Hand = $"{firstcard} {secondcard}";
        }        

        private void Say(int index, int bet = 0)
        {
            if (bet == 0)
            {
                this._consoleWrap.WriteLine($"{this.Name}: {this.speech[index]}");
            }
            else
            {
                this._consoleWrap.WriteLine($"{this.Name}: {this.speech[index]} {bet}");
            }
        }
    }
}
