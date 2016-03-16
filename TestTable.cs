using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class TestTable : ITable
    {        
        private readonly int sb = 2;
        private readonly int bb = 5;

        public int MainPot { get; set; }
        public int CurrentBet { get; set; }
        public int SBPlayer { get; set; }
        public int BBPlayer { get; set; }
        public List<IPlayer> Players { get; set; }
        public Card[] Board { get; set; }

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

        public int MinRaiseSize
        {
            get
            {
                int minRaiseSize = this.bb;

                var sortedlist = this.Players.OrderByDescending(a => a.AmountBet);

                int count = 0;
                int firstbet = 0;
                int secondbet = 0;
                int difference = 0;

                foreach (IPlayer player in sortedlist)
                {                   
                    if (count == 0) firstbet = player.AmountBet;

                    secondbet = player.AmountBet;
                    
                    if (count == 1) difference = firstbet - secondbet;

                    if (difference > (firstbet - secondbet)) difference = firstbet - secondbet;

                    minRaiseSize = difference + firstbet;

                    count++;
                }

                if (minRaiseSize < this.bb) return this.bb;

                return minRaiseSize;
            }
        }

        public TestTable(List<IPlayer> players)
        {
            this.Players = players;
            this.MainPot = 0;
            this.CurrentBet = 0;
            this.SBPlayer = 0;
            this.BBPlayer = 1;
            this.Board = new Card[5];
        }

        public TestTable(int players)
        {
            this.Players = new List<IPlayer>();
            this.MainPot = 0;
            this.CurrentBet = 0;
            this.SBPlayer = 0;
            this.BBPlayer = 1;
            this.Board = new Card[5];                       

            for (int i = 1; i <= players; i++)
            {
                this.Players.Add(new TestPlayer("Player " + i.ToString(), 500));
            }
        }

        public void BlindsIn(IConsole console, IDealer dealer)
        {
            throw new NotImplementedException();
        }

        public void AddToPot()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentBet(int bet)
        {
            throw new NotImplementedException();
        }

        public void AddFlop(Card firstcard, Card secondcard, Card thirdcard)
        {
            throw new NotImplementedException();
        }

        public void AddTurn(Card turn)
        {
            throw new NotImplementedException();
        }

        public void AddRiver(Card river)
        {
            throw new NotImplementedException();
        }
    }
}
