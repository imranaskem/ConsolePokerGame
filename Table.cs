using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class Table : ITable
    {
        private string instructions = "Welcome to the console poker game. When you are asked a question please respond with the letter or word of your selection.";
        private string numberOfPlayers = "How many players today?";
        private int sb = 2;
        private int bb = 5;

        public int MainPot { get; private set; }
        public int CurrentBet { get; private set; }
        public int SBPlayer { get; private set; }
        public int BBPlayer { get; private set; }
        public List<Player> Players { get; private set; }
        public Card[] Board { get; private set; }

        public string Flop
        {
            get
            {
                if (this.Board.Contains(null)) throw new InvalidOperationException("No community cards exist");
                else
                {
                    return this.Board[0].CardName + this.Board[1].CardName + this.Board[2].CardName;
                }
            }
        }

        public string Turn
        {
            get
            {
                if (this.Board.Contains(null)) throw new InvalidOperationException("No community cards exist");
                else
                {
                    return this.Flop + this.Board[3].CardName;
                }
            }
        }

        public string FullBoard
        {
            get
            {
                if (this.Board.Contains(null)) throw new InvalidOperationException("No community cards exist");
                else
                {
                    return this.Turn + this.Board[4].CardName;
                }
            }
        }

        public Table(IConsole console)
        {
            this.Players = new List<Player>();
            this.MainPot = 0;
            this.CurrentBet = 0;
            this.SBPlayer = 0;
            this.BBPlayer = 1;
            this.Board = new Card[5];

            int players;

            console.WriteLine(this.instructions);
            console.WriteLine(this.numberOfPlayers);
            console.WriteLine();

            var response = console.ReadLine();

            if (!int.TryParse(response, out players))
            {
                console.WriteLine();
                console.WriteLine("That isn't a number, program exiting...");
                Environment.Exit(1);
            }

            for (int i = 1; i <= players; i++)
            {
                this.Players.Add(new Player("Player " + i.ToString(), 500));
            }
        }       

        public void BlindsIn(IConsole console, IDealer dealer)
        {
            console.WriteLine(dealer.Say(0));
            console.WriteLine(this.Players[this.SBPlayer].Name + " small blind please");
            console.WriteLine(this.Players[this.BBPlayer].Name + " big blind please");

            this.Players[this.SBPlayer].Blind(this.sb);
            this.Players[this.BBPlayer].Blind(this.bb);

            this.SetCurrentBet(this.bb);            
        }        

        public void AddToPot()
        {
            foreach (Player player in this.Players)
            {
                this.MainPot += player.AmountBet; 
            }
        }

        public void AddFlop(Card firstcard, Card secondcard, Card thirdcard)
        {
            this.Board[0] = firstcard;
            this.Board[1] = secondcard;
            this.Board[2] = thirdcard;
        }

        public void AddTurn(Card turn)
        {
            this.Board[3] = turn;
        }

        public void AddRiver(Card river)
        {
            this.Board[4] = river;
        }

        public void SetCurrentBet(int bet)
        {
            this.CurrentBet = bet;

            this.SetCallAmountsForAllPlayers();
        }

        private void SetCallAmountsForAllPlayers()
        {
            foreach (Player player in this.Players)
            {
                player.SetCall(this.CurrentBet);
            }
        }
    }
}
