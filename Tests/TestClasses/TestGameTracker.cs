using ConsolePokerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Interfaces;

namespace ConsolePokerGame.Tests
{
    public class TestGameTracker : IGameTracker
    {
        private readonly int sb = 2;
        private readonly int bb = 5;

        private string[] speech = { "Welcome to the console poker game. When you are asked a question please respond with the letter or word of your selection.",
            "How many players today?",
            "Blinds in please, small is 2 and big is 5",
            "Check or bet?",
            "Call, fold or raise?",
            "How much?",
            "How much would you like to raise? (Please enter the total raise size)"};
        private bool firstAction = false;
        private string[] check = { "c", "C", "check", "Check" };
        private string[] call = { "c", "C", "call", "Call" };
        private string[] bet = { "b", "B", "bet", "Bet" };
        private string[] fold = { "f", "F", "fold", "Fold" };
        private string[] raise = { "r", "R", "raise", "Raise" };

        public int ActionOnPlayer { get; private set; }
        public int CurrentBet { get; private set; }
        public int MainPot { get; private set; }
        public int MinRaiseSize { get; private set; }
        public string Flop { get; private set; }
        public string Turn { get; private set; }
        public string FullBoard { get; private set; }
        public Card[] Board { get; private set; }
        public Deck Cards { get; private set; }
        public IConsole _console { get; private set; }
        public List<IPlayer> Players { get; private set; }

        public TestGameTracker(IConsole console)
        {
            this.ActionOnPlayer = 0;
            this.CurrentBet = 0;
            this.MainPot = 0;
            this.MinRaiseSize = 0;
            this.Flop = null;
            this.Turn = null;
            this.FullBoard = null;
            this._console = console;
            this.Cards = new Deck();
            this.Cards.MainDeck.Shuffle();
            this.Board = new Card[5];
            this.Players = new List<IPlayer>();

            this.Say(0);
        }


        public void RoundOfAction()
        {
            throw new NotImplementedException();
        }

        public void AddFlop(Card firstcard, Card secondcard, Card thirdcard)
        {
            throw new NotImplementedException();
        }

        public void AddRiver(Card river)
        {
            throw new NotImplementedException();
        }

        public void AddTurn(Card turn)
        {
            throw new NotImplementedException();
        }

        public void BlindsIn()
        {
            this.DefineNumberOfPlayers();

            this.Say(2);

            foreach (var player in this.Players)
            {
                if (player.PlayerPosition == Position.SmallBlind)
                {
                    player.Blind(this.sb);
                }

                if (player.PlayerPosition == Position.BigBlind)
                {
                    player.Blind(this.bb);
                }
            }
        }

        public void DealFlop()
        {
            throw new NotImplementedException();
        }

        public void DealRiver()
        {
            throw new NotImplementedException();
        }

        public void DealToPlayers()
        {
            throw new NotImplementedException();
        }

        public void DealTurnOrRiver()
        {
            throw new NotImplementedException();
        }

        public void Say(int index)
        {
            this._console.WriteLine(this.speech[index]);
        }

        public void SetCurrentBet(int bet)
        {
            throw new NotImplementedException();
        }

        private void DefineNumberOfPlayers()
        {
            this.Say(1);

            int players = this._console.GetNumberInput();

            for (int i = 1; i <= players; i++)
            {
                if (this.Players.Count > 4)
                {
                    this.Players.Add(new Player("Player " + i.ToString(), 500, 3, this._console));
                }
                else if (this.Players.Count == (this.Players.Count - 1))
                {
                    this.Players.Add(new Player("Player " + i.ToString(), 500, 4, this._console));
                }
                else
                {
                    this.Players.Add(new Player("Player " + i.ToString(), 500, this.Players.Count, this._console));
                }
            }
        }
    }
}
