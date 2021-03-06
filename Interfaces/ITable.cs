﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    public interface ITable
    {
        int MainPot { get; }
        int CurrentBet { get; }
        List<IPlayer> Players { get; }
        Card[] Board { get; }
        string Flop { get; }
        string Turn { get; }
        string FullBoard { get; }
        int MinRaiseSize { get; }
               
        void BlindsIn(IConsole console, IDealer dealer);        
        void SetCurrentBet(int bet);
        void AddFlop(Card firstcard, Card secondcard, Card thirdcard);
        void AddTurn(Card turn);
        void AddRiver(Card river);
    }
}
