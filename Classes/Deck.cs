using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public class Deck
    {
        public List<Card> MainDeck { get; private set; }
        public List<Card> DiscardDeck { get; private set; }

        public Deck()
        {
            this.MainDeck = new List<Card>();
            this.DiscardDeck = new List<Card>();

            for (int s = 0; s < 4; s++)
            {
                for (int r = 0; r < 13; r++)
                {
                    Card nextCard = new Card(s, r);
                    this.MainDeck.Add(nextCard);
                }
            }                        
        }
    }
}
