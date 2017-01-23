using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using ConsolePokerGame;
using ConsolePokerGame.Classes;
using ConsolePokerGame.Enums;

namespace ConsolePokerGame.Tests
{
    [TestFixture]
    public class UnitTests
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

            var gt = new GameTracker(console);

            Assert.That(gt.Players.Count, Is.EqualTo(4));
        }

        [Test]
        public static void PlayerCreation()
        {         
            List<string> response = new List<string> { "4" };

            TestConsoleWrapper console = new TestConsoleWrapper(response);

            var gt = new GameTracker(console);

            var count = 1;

            var arrayCount = 0;

            var arrayOfPositions = new Position[] { Position.SmallBlind, Position.BigBlind, Position.UnderTheGun, Position.Dealer };

            Assert.That(gt.MainPot, Is.EqualTo(0));

            foreach (var player in gt.Players)
            {
                Assert.That(player.Name, Is.EqualTo($"Player {count}"));
                Assert.That(player.Chips, Is.EqualTo(500));
                Assert.That(player.HoleCards[0], Is.EqualTo(null));
                Assert.That(player.HoleCards[1], Is.EqualTo(null));
                Assert.That(player.InHand, Is.EqualTo(true));
                Assert.That(player.PlayerPosition, Is.EqualTo(arrayOfPositions[arrayCount]));

                count++;
                arrayCount++;
            }            
        }

        [Test]
        public static void BlindTest()
        {   
            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            var gt = new GameTracker(answers);
                        
            gt.BlindsIn();

            var smallBlind = gt.Players.Single(p => p.PlayerPosition == Position.SmallBlind);
            var bigBlind = gt.Players.Single(p => p.PlayerPosition == Position.BigBlind);

            Assert.That(smallBlind.Chips, Is.EqualTo(498));
            Assert.That(bigBlind.Chips, Is.EqualTo(495));
        }

        [Test]
        public static void DealTest()
        {   
            List<string> response = new List<string> { "4" };

            TestConsoleWrapper answers = new TestConsoleWrapper(response);

            var gt = new GameTracker(answers);
                        
            gt.DealToPlayers();           

            Assert.That(gt.Players[0].HoleCards[0], Is.TypeOf(typeof(Card)));
        }

        [Test]
        public static void MinRaiseSize()
        {
            
        }

        [Test]
        public static void PlayerRaise()
        {
            
        }

        [Test]
        public static void PlayerRaiseNotEnoughChips()
        {
                   
        }

        [Test]
        public static void PlayerRaiseNotBigEnough()
        {
            
        }

        [Test]
        public static void PlayerRaiseInputIncorrect()
        {
            
        }
    }
}
