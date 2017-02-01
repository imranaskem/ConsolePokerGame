using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsolePokerGame.Interfaces;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame.Classes
{
    public class GameTracker : IGameTracker
    {        
        private IConsole _console { get; set; }

        private readonly int sb = 2;
        private readonly int bb = 5;

        private string[] speech = { "Welcome to the console poker game. When you are asked a question please respond with the letter or word of your selection.",
            "How many players today? (Max of 8)",
            "Blinds in please, small is 2 and big is 5",
            "Check or bet?",
            "Call, fold or raise?",
            "How much?",
            "How much would you like to raise? (Please enter the total raise size)"};        
        private string[] check = { "c", "C", "check", "Check" };
        private string[] call = { "c", "C", "call", "Call" };
        private string[] bet = { "b", "B", "bet", "Bet" };
        private string[] fold = { "f", "F", "fold", "Fold" };
        private string[] raise = { "r", "R", "raise", "Raise" };

        public int ActionOnPlayer { get; private set; }  
        public int CurrentBet { get; private set; }
        public int MainPot { get; private set; }
        public int MinRaiseSize { get; private set; }
        public int NumberOfPlayers { get; private set; }        
        public string FullBoard { get; private set; }
        public List<Card> Board { get; private set; }
        public Deck Cards{ get; private set; }
        public List<IPlayer> Players { get; private set; }

        public GameTracker(IConsole console)
        {
            this.ActionOnPlayer = 0;
            this.CurrentBet = 0;
            this.MainPot = 0;
            this.MinRaiseSize = 0;            
            this.FullBoard = null;
            this._console = console;
            this.Cards = new Deck();
            this.Cards.MainDeck.Shuffle();
            this.Board = new List<Card>();
            this.Players = new List<IPlayer>();

            this.Say(0);
            this.DefineNumberOfPlayers();
        }


        public void RoundOfAction(Position startingPlayer)
        {
            for (var i = startingPlayer; i <= Position.Dealer; i++)
            {
                var currentPlayer = this.Players
                    .Single(p => p.PlayerPosition == i && p.InHand == true);

                if (this.CurrentBet > 0)
                {
                    this.ActionFacingABet(currentPlayer);
                }
                else
                {
                    this.ActionWithNoPreviousBet(currentPlayer);
                }
            }
        }   
        
        private void ActionFacingABet(IPlayer player)
        {
            this.Say(4);

            var response = this._console.ReadLine();

            if (this.fold.Contains(response))
            {
                var foldedCards = player.Fold();

                foreach (var card in foldedCards)
                {
                    this.Cards.DiscardDeck.Add(card);
                }

                return;
            }

            if (this.call.Contains(response))
            {
                this.MainPot += player.Call(this.CurrentBet);

                return;
            }

            if (this.raise.Contains(response))
            {
                var raise = player.Bet(this.MinRaiseSize);

                this.CurrentBet = raise;

                this.MainPot += raise;

                return;
            }
        }

        private void ActionWithNoPreviousBet(IPlayer player)
        {
            this.Say(3);

            var response = this._console.ReadLine();

            if (this.check.Contains(response))
            {
                player.Check();
            }

            if (this.bet.Contains(response))
            {
                var bet = player.Bet(this.MinRaiseSize, false);

                this.CurrentBet = bet;

                this.MainPot += bet;
            }
        }   

        public void BlindsIn()
        {
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

            this.CurrentBet = this.bb;
            this.MinRaiseSize = this.bb - this.sb;
        }

        public void DealFlop()
        {
            var burnCard = this.Cards.MainDeck[0];
            var firstCard = this.Cards.MainDeck[1];
            var secondCard = this.Cards.MainDeck[2];
            var thirdCard = this.Cards.MainDeck[3];

            this.Board.Add(firstCard);
            this.Board.Add(secondCard);
            this.Board.Add(thirdCard);

            this.FullBoard = $"{firstCard} {secondCard} {thirdCard} ";

            this.Cards.DiscardDeck.Add(burnCard);

            this.Cards.MainDeck.RemoveRange(0, 4);

            this.MinRaiseSize = this.bb;
        }

        public void DealTurnOrRiver()
        {
            var burnCard = this.Cards.MainDeck[0];
            var card = this.Cards.MainDeck[1];

            this.Board.Add(card);

            this.FullBoard += $"{card} ";

            this.Cards.DiscardDeck.Add(burnCard);

            this.Cards.MainDeck.RemoveRange(0, 2);

            this.MinRaiseSize = this.bb;
        }        

        public void DealToPlayers()
        {
            int players = this.Players.Count;
            int count = 0;

            foreach (var player in this.Players)
            {
                var firstcard = this.Cards.MainDeck[count];
                firstcard.SetStatusToPlayer();

                var secondcard = this.Cards.MainDeck[count + players];
                secondcard.SetStatusToPlayer();

                player.TakeCards(firstcard, secondcard);

                this._console.WriteLine($"{player.Name} you've been dealt {player.Hand}");

                count++;
            }

            this.Cards.MainDeck.RemoveRange(0, players * 2);
        }

        public void Say(int index)
        {
            this._console.WriteLine(this.speech[index]);
        }        

        private void DefineNumberOfPlayers()
        {
            this.Say(1);

            this.NumberOfPlayers = this._console.GetNumberInput();

            for (int i = 1; i <= this.NumberOfPlayers; i++)
            {
                if (this.Players.Count == (this.NumberOfPlayers - 1))
                {
                    this.Players.Add(new Player("Player " + i.ToString(), 500, Position.Dealer, this._console));
                }
                else
                {
                    var position = (Position) this.Players.Count;
                    this.Players.Add(new Player("Player " + i.ToString(), 500, position, this._console));
                }
            }
        }
    }
}
