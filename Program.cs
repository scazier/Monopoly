using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Monopoly
{
    class Program
    {
        public static void Start()
        {
            Console.WriteLine("\nFrise Début\n");
            Game model = new Game();
            GameView view = new GameView();
            GameController controller = new GameController(model, view);

            Street Board = model.Board;
            Player players = model.Players;
            Banker banker = Banker.CreateBanker();

            //banker.Operations.Add(new Exchange(2, "-400"));
            //Console.WriteLine("banker: "+banker.toString());

            controller.initializePlayers();

            players.Move(1);
            players.Buy();

            //Console.WriteLine(players.toString()+"\n");

            players = players.next;
            players.Move(1);
            players.GoToJail();
            //players = PayRent(players, players.Position.Id_buyer, "Street");
            //Console.WriteLine(players.toString());
            Console.Clear();
            view.displayGame();
            //Cards card = Cards.GetCards();
            //Console.WriteLine(card.toString());

        }


        static void Main(string[] args)
        {
            // Stack<int> s = new Stack<int>();
            // s.Push(2);
            // s.Push(3);
            // s.Push(4);
            //
            // IEnumerator enumerator = s.GetEnumerator();
            // while (enumerator.MoveNext())
            // {
            //     Console.WriteLine("Stack: "+enumerator.Current);
            // }
            //
            // Player pls = Game.getPlayers();
            // Player act = pls;
            //while (act.ID!=4){ Console.WriteLine(act.toString()); act = act.next; }

            if (args.Length == 0)
            {
                Start();
                //Deck deck = Deck.CreateDeck();
            }
            else
            {
                if (args[0].Equals("--test"))
                {
                    if (args.Length == 2 && args[1].Equals("--v"))
                    {
                        UnitTest.Test(true);
                    }
                    else
                    {
                        UnitTest.Test();
                    }
                }
            }
            Console.WriteLine("Build succeed!!!");
        }
    }
}
