﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class Player : IPlayer
    {
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

        public Player (string name, int chips)
        {
            this.Name = name;
            this.Chips = chips;
            this.AmountToCall = 0;
            this.AmountBet = 0;
            this.InHand = true;

            this.HoleCards = new Card[2] { null, null };          
        }       

        public void Bet(ITable table, IConsole console)
        {
            console.WriteLine("Player has " + this.Chips.ToString() + " chips remaining");
            console.WriteLine("Minimum bet size is " + table.MinRaiseSize.ToString());

            var response = console.ReadLine();

            int amount;

            if (!int.TryParse(response, out amount))
            {
                console.WriteLine();
                console.WriteLine("That isn't a number, program exiting...");
                Environment.Exit(1);
            }

            if (amount <= this.Chips)
            {
                this.AmountBet = amount;
                this.Chips -= amount;
            }
            else throw new InvalidOperationException("Player does not have enough chips");            
        }

        public void Fold()
        {
            this.InHand = false;

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
            this.Chips -= blind;
            this.AmountBet = blind;
        }

        public void TakeCards(Card firstcard, Card secondcard)
        {
            this.HoleCards[0] = firstcard;
            this.HoleCards[1] = secondcard;
        }

        public bool Raise(ITable table, IConsole console)
        {
            int amount;
                                      
            var response = console.ReadLine();

            if (!int.TryParse(response, out amount))
            {                
                throw new InvalidOperationException("That is not a number, please try again");
            }

            if (amount > this.Chips) throw new InvalidOperationException("Player does not have enough chips");

            if (amount < table.MinRaiseSize) throw new InvalidOperationException("Raise is not big enough, should be at least " + table.MinRaiseSize.ToString());
                      
            this.AmountBet = amount;

            table.SetCurrentBet(amount);

            return true;
        }
    }
}
