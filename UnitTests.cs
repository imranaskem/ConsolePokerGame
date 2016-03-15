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
            Table table = new Table();

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper console = new TestConsoleWrapper(response);

            table.Setup(console);

            Assert.That(table.Players.Count, Is.EqualTo(4));
        }

        [Test]
        public static void PlayerCreation()
        {
            Table table = new Table();

            Dealer dealer = new Dealer();

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper console = new TestConsoleWrapper(response);

            table.Setup(console);

            Assert.That(table.MainPot, Is.EqualTo(0));
            Assert.That(table.Players[0].Name, Is.EqualTo("Player 1"));
            Assert.That(table.Players[0].Chips, Is.EqualTo(500));
            Assert.That(table.Players[0].HoleCards[0], Is.EqualTo(null));
            Assert.That(table.Players[0].HoleCards[1], Is.EqualTo(null));
        }

        [Test]
        public static void BlindTest()
        {
            Table table = new Table();

            Dealer dealer = new Dealer();

            ConsoleWrapper console = new ConsoleWrapper();

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            table.Setup(answers);

            table.BlindsIn(console, dealer);

            Assert.That(table.MainPot, Is.EqualTo(7));
            Assert.That(table.Players[0].Chips, Is.EqualTo(498));
            Assert.That(table.Players[1].Chips, Is.EqualTo(495));
        }

        [Test]
        public static void DealTest()
        {
            Table table = new Table();

            Dealer dealer = new Dealer();

            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            table.Setup(answers);

            dealer.DealToPlayers(table);           

            Assert.That(table.Players[0].HoleCards[0], Is.TypeOf(typeof(Card)));
        }
    }
}
