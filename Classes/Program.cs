using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePokerGame
{
    class Program
    {       
        static void Main(string[] args)
        {           
            ConsoleWrapper console = new ConsoleWrapper();

            Table table = new Table(console);

            Dealer dealer = new Dealer();                       

            //Pre-flop

            table.BlindsIn(console, dealer);

            dealer.DealToPlayers(table, console);

            do
            {
                int actionTracker = 0;

                foreach (IPlayer player in table.Players)
                {
                    if (dealer.ActionOnPlayer == actionTracker && player.InHand)
                    {
                        bool parsed = true;

                        do
                        {
                            try
                            {
                                dealer.Action(player, table, console);
                                dealer.MoveActionToNextPlayer(table);
                            }
                            catch (TextInputIncorrectException ex)
                            {
                                console.WriteLine(ex.Message);
                                parsed = false;
                            }
                        } while (!parsed);
                    }
                    actionTracker++;
                }
            } while (table.RoundOver > 0);

            //Flop

            dealer.DealFlop(table, console);

            do
            {
                int actionTracker = 0;

                foreach (IPlayer player in table.Players)
                {
                    if (dealer.ActionOnPlayer == actionTracker && player.InHand)
                    {
                        console.WriteLine(table.Flop);

                        bool parsed = true;

                        do
                        {
                            try
                            {
                                dealer.Action(player, table, console);
                                dealer.MoveActionToNextPlayer(table);
                            }
                            catch (TextInputIncorrectException ex)
                            {
                                console.WriteLine(ex.Message);
                                parsed = false;
                            }
                        } while (!parsed);
                    }

                    actionTracker++;
                }
            } while (table.RoundOver > 0);

            //Turn

            dealer.DealTurn(table, console);

            do
            {
                int actionTracker = 0;

                foreach (IPlayer player in table.Players)
                {
                    if (dealer.ActionOnPlayer == actionTracker && player.InHand)
                    {
                        console.WriteLine(table.Turn);

                        bool parsed = true;

                        do
                        {
                            try
                            {
                                dealer.Action(player, table, console);
                                dealer.MoveActionToNextPlayer(table);
                            }
                            catch (TextInputIncorrectException ex)
                            {
                                console.WriteLine(ex.Message);
                                parsed = false;
                            }
                        } while (!parsed);
                    }

                    actionTracker++;
                }
            } while (table.RoundOver > 0);

            //River

            dealer.DealRiver(table, console);

            do
            {
                int actionTracker = 0;

                foreach (IPlayer player in table.Players)
                {
                    if (dealer.ActionOnPlayer == actionTracker && player.InHand)
                    {
                        console.WriteLine(table.FullBoard);

                        bool parsed = true;

                        do
                        {
                            try
                            {
                                dealer.Action(player, table, console);
                                dealer.MoveActionToNextPlayer(table);
                            }
                            catch (TextInputIncorrectException ex)
                            {
                                console.WriteLine(ex.Message);
                                parsed = false;
                            }
                        } while (!parsed);
                    }

                    actionTracker++;
                }
            } while (table.RoundOver > 0);
        }
    }
}
