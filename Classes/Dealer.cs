﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class Dealer : IDealer
    {        
        private string[] speech = { "Blinds in please, small is 2 and big is 5",
                                        "Check or bet?",
                                        "Call, fold or raise?",
                                        "How much?",
                                        "How much would you like to raise? (Please enter the total raise size)"};
        private bool firstAction = false;
        private string[] check =  { "c", "C", "check", "Check" };
        private string[] call = { "c", "C", "call", "Call" };
        private string[] bet = { "b", "B", "bet", "Bet" };
        private string[] fold = { "f", "F", "fold", "Fold" };
        private string[] raise = { "r", "R", "raise", "Raise" };        

        public Deck Cards { get; private set; }        
        public int ActionOnPlayer { get; private set; }        

        public Dealer()
        {
            this.Cards = new Deck();
            this.Cards.MainDeck.Shuffle();
            this.ActionOnPlayer = 2;            
        }

        public void DealToPlayers(ITable table, IConsole console)
        {
            int players = table.Players.Count;
            int count = 0;

            foreach (Player player in table.Players)
            {              
                var firstcard = this.Cards.MainDeck[count];
                var secondcard = this.Cards.MainDeck[count + players];

                player.TakeCards(firstcard, secondcard);

                console.WriteLine(player.Name + " you've been dealt " + player.Hand);

                count++;                
            }

            this.Cards.MainDeck.RemoveRange(0, players * 2);
        }        

        public string Say(int index)
        {
            return this.speech[index];
        }

        public void DealFlop(ITable table, IConsole console)
        {
            var burncard = this.Cards.MainDeck[0];
            var firstcard = this.Cards.MainDeck[1];
            var secondcard = this.Cards.MainDeck[2];
            var thirdcard = this.Cards.MainDeck[3];

            table.AddFlop(firstcard, secondcard, thirdcard);

            console.WriteLine("Flop: " + firstcard.CardName + secondcard.CardName + thirdcard.CardName);

            this.Cards.DiscardDeck.Add(burncard);

            this.Cards.MainDeck.RemoveRange(0, 4);

            this.firstAction = true;
        }

        public void DealTurn(ITable table, IConsole console)
        {
            var burncard = this.Cards.MainDeck[0];
            var firstcard = this.Cards.MainDeck[1];

            table.AddTurn(firstcard);

            console.WriteLine("Flop: " + table.Flop);
            console.WriteLine("Turn: " + firstcard.CardName);

            this.Cards.DiscardDeck.Add(burncard);

            this.Cards.MainDeck.RemoveRange(0, 2);

            this.firstAction = true;
        }

        public void DealRiver(ITable table, IConsole console)
        {
            var burncard = this.Cards.MainDeck[0];
            var firstcard = this.Cards.MainDeck[1];

            table.AddRiver(firstcard);

            console.WriteLine("Board: " + table.Turn);
            console.WriteLine("River: " + firstcard.CardName);

            this.Cards.DiscardDeck.Add(burncard);

            this.Cards.MainDeck.RemoveRange(0, 2);

            this.firstAction = true;
        }

        public void MoveActionToNextPlayer(ITable table)
        {
            this.ActionOnPlayer++;

            if (this.ActionOnPlayer > (table.Players.Count - 1)) this.ActionOnPlayer = 0;
        }

        public void Action(IPlayer player, ITable table, IConsole console)
        {
            console.WriteLine();
            console.WriteLine(player.Name);
            console.WriteLine("Your holecards are: " + player.Hand);
            console.WriteLine("The pot is " + table.MainPot.ToString());

            if (this.firstAction)
            {
                console.WriteLine(this.Say(1));

                var response = console.ReadLine();

                if (this.check.Contains(response))
                {
                    return;
                }

                if (this.bet.Contains(response))
                {
                    console.WriteLine(this.Say(3));
                    console.WriteLine("Player has " + player.Chips.ToString() + " chips remaining");

                    bool parsed = true;

                    do
                    {
                        try
                        {
                            player.Bet(table, console);
                        }

                        catch (NotEnoughChipsException ex)
                        {
                            parsed = false;
                            console.WriteLine(ex.Message);
                        }
                    } while (!parsed);

                    this.firstAction = false;
                }
                else throw new TextInputIncorrectException("Command was incorrect please try again");
            }
            else
            {
                if (!this.firstAction)
                {
                    if (player.AmountToCall > 0) console.WriteLine("Amount to call is " + player.AmountToCall.ToString());

                    console.WriteLine(this.Say(2));

                    var response = console.ReadLine();

                    if (this.fold.Contains(response))
                    {
                        this.Cards.DiscardDeck.AddRange(player.HoleCards);
                        player.Fold();
                    }

                    if (this.call.Contains(response))
                    {
                        player.Call();
                    }

                    if (this.raise.Contains(response))
                    {
                        console.WriteLine(this.Say(4));
                        console.WriteLine("Player has " + player.Chips.ToString() + " chips remaining");
                        console.WriteLine("Minimum raise size is " + table.MinRaiseSize.ToString());
                        console.WriteLine();

                        bool parsed = true;

                        do
                        {
                            try
                            {
                                player.Raise(table, console);
                            }

                            catch (NotEnoughChipsException ex)
                            {
                                parsed = false;
                                console.WriteLine(ex.Message);
                                console.WriteLine("Please try again");
                            }

                        } while (!parsed);
                    }                    
                }
                else throw new TextInputIncorrectException("Command was incorrect please try again");
            }
        }
    }
}
