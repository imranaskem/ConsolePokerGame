using System;
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
                                        "How much?"};
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
        }

        public void Action(IPlayer player, ITable table, IConsole console)
        {
            console.WriteLine("The pot is " + table.MainPot.ToString());
            console.WriteLine("Your holecards are: " + player.Hand);

            if (this.firstAction)
            {
                console.WriteLine(this.Say(1));

                var response = console.ReadLine();

                if (this.bet.Contains(response))
                {
                    console.WriteLine(this.Say(3));
                    console.WriteLine("Player has " + player.Chips.ToString() + " chips remaining");
                    player.Bet(console);

                    table.SetCurrentBet(player.AmountBet);

                    this.firstAction = false;
                }
            }
            else
            {
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

                }
            }
        }
    }
}
