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
        private IConsole _consoleWrap;
        private string[] speech = { "Added their small blind of",
            "Added their big blind of",
            "Called the bet of",
            "Bet",
            "Raised to" };

        public Position PlayerPosition { get; private set; }
        public string Name { get; private set; }
        public bool InHand { get; private set; }
        public int Chips { get; private set; }
        public int AmountBet { get; private set; }
        public int AmountToCall { get; private set; }
        public Card[] HoleCards { get; private set; }

        public string Hand
        {
            get
            {
                if (this.HoleCards.Contains(null)) throw new InvalidOperationException("Player does not have cards");
                else
                {
                    string hand = this.HoleCards[0].CardName + this.HoleCards[1].CardName;
                    return hand;
                }
            }
        }

        public Player (string name, int chips, int position, IConsole console)
        {
            this.Name = name;
            this.Chips = chips;
            this.AmountToCall = 0;
            this.AmountBet = 0;
            this.InHand = true;

            this._consoleWrap = console;         

            if (Enum.IsDefined(typeof(Position), position))
            {                
                this.PlayerPosition = (Position) position;
            }
            else throw new ArgumentOutOfRangeException("position", $"Enum does not contain a value at {position}" );

            this.HoleCards = new Card[2] { null, null };          
        }       

        public void Bet(int bet)
        {
            this._consoleWrap.WriteLine($"Player has {this.Chips} chips remaining");
            this._consoleWrap.WriteLine($"Minimum bet size is {bet}");

            int amount = this._consoleWrap.GetNumberInput();

            if (amount <= this.Chips)
            {
                this.AmountBet = amount;
                this.Chips -= amount;                
            }
            else throw new NotEnoughChipsException("Player does not have enough chips", NotEnoughChipsException.Reason.PlayerDoesNotHaveEnoughChips);            
        }

        public void Fold()
        {
            this.InHand = false;
            this.AmountBet = 0;
            this.AmountToCall = 0;

            Array.Clear(this.HoleCards, 0, 2);            
        }

        public void SetCall(int bet)
        {
            int callAmount = bet - this.AmountBet;
            this.AmountToCall = callAmount;
        }

        public void Call()
        {
            int callAmount = this.AmountToCall - this.AmountBet;
            this.Chips -= callAmount;
            this.AmountBet = this.AmountToCall;
            this.AmountToCall = 0;
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
        }

        public void Raise(int minRaise)
        {
            int amount = this._consoleWrap.GetNumberInput();

            if (amount > this.Chips)
            {
                throw new NotEnoughChipsException(
                    "Player does not have enough chips",
                    NotEnoughChipsException.Reason.PlayerDoesNotHaveEnoughChips);
            }

            if (amount < minRaise)
            {
                throw new NotEnoughChipsException(
                    $"Raise is not big enough, should be at least {bet}",
                    NotEnoughChipsException.Reason.RaiseNotBigEnough);
            }
                                  
            this.AmountBet = amount;            
        }

        private void Say(int index, int bet = 0)
        {
            this._consoleWrap.WriteLine($"{this.Name}: {this.speech[index]}");
        }
    }
}
