using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ConsolePokerGame
{
    [TestFixture]
    class UnitTests
    {
        [Test]
        public static void DeckCreation()
        {
            Deck deck = new Deck();

            Assert.That(deck.MainDeck.Count, Is.EqualTo(52));
            Assert.That(deck.MainDeck[0].RankLetter, Is.EqualTo("A"));
            Assert.That(deck.MainDeck[0].SuitLetter, Is.EqualTo("d"));
        }

        [Test]
        public static void SetupTest()
        {          
            List<string> response = new List<string> { "4" };

            TestConsoleWrapper console = new TestConsoleWrapper(response);

            Table table = new Table(console);            

            Assert.That(table.Players.Count, Is.EqualTo(4));
        }

        [Test]
        public static void PlayerCreation()
        {         
            List<string> response = new List<string> { "4" };

            TestConsoleWrapper console = new TestConsoleWrapper(response);

            Table table = new Table(console);            

            Assert.That(table.MainPot, Is.EqualTo(0));
            Assert.That(table.Players[0].Name, Is.EqualTo("Player 1"));
            Assert.That(table.Players[0].Chips, Is.EqualTo(500));
            Assert.That(table.Players[0].HoleCards[0], Is.EqualTo(null));
            Assert.That(table.Players[0].HoleCards[1], Is.EqualTo(null));
        }

        [Test]
        public static void BlindTest()
        {           
            Dealer dealer = new Dealer();           

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            Table table = new Table(answers);
                        
            table.BlindsIn(answers, dealer);

            Assert.That(table.Players[0].Chips, Is.EqualTo(498));
            Assert.That(table.Players[1].Chips, Is.EqualTo(495));
        }

        [Test]
        public static void DealTest()
        {
            

            Dealer dealer = new Dealer();

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            Table table = new Table(answers);
                        
            dealer.DealToPlayers(table, answers);           

            Assert.That(table.Players[0].HoleCards[0], Is.TypeOf(typeof(Card)));
        }

        [Test]
        public static void MinRaiseSize()
        {
            List<IPlayer> players = new List<IPlayer>();

            players.Add(new TestPlayer(10));
            players.Add(new TestPlayer(30));
            players.Add(new TestPlayer(50));
            players.Add(new TestPlayer(20));

            TestTable table = new TestTable(players);

            Assert.That(table.MinRaiseSize, Is.EqualTo(70));
        }

        [Test]
        public static void PlayerRaise()
        {
            List<string> response = new List<string> { "80" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            List<IPlayer> players = new List<IPlayer>();

            players.Add(new TestPlayer(10));
            players.Add(new TestPlayer(20));
            players.Add(new TestPlayer(30));
            players.Add(new TestPlayer(50));

            TestTable table = new TestTable(players);

            table.Players[3].Raise(table, answers);

            Assert.That(table.Players[3].AmountBet, Is.EqualTo(80));
            Assert.That(table.Players[0].AmountToCall, Is.EqualTo(70));
        }
    }
}
