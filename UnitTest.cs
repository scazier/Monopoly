using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Monopoly
{

    public class UnitTest
    {
        static int all = 0;
        // This will be used to count all the tests
        static int errors = 0;
        // This will be used to count the number of incorrect unit tests

        public static void displayResult(String feature, bool result)
        {
            all++;
            Console.Write("\t"+feature+": ");
            if (result)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed");
                errors++;
            }
            Console.ResetColor();
        }

        public static void final()
        {
            Console.Write("\nResult: "+all+" - ");
            Console.ForegroundColor = ConsoleColor.Green;
            int correct = all - errors;
            Console.Write("OK: "+correct+" - ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Failed: "+errors+"\n");
            Console.ResetColor();
        }

        public static void CheckBoard(bool verbose=false)
        {
            /*
                This function is unit test for the previous function "CreateBoard".
                It check if the ringed linked list is well created.
                It makes two rounds of the list and check if each next id is an increment
                by one of the previous one except for the id 1 where the previous id is 40.
            */
            Street first = Game.CreateBoard();
            Street actual = first;

            int cpt = 0;
            bool result = true;
            while (cpt != 2)
            {
                if (cpt == 0 && verbose){ Console.WriteLine(actual.toString()); }
                if (actual.ID == 40)
                {
                    cpt++;
                    if (actual.next.ID != 1) { result = false; }
                }
                else
                {
                    if (actual.ID != actual.next.ID - 1) { result = false; }
                }
                actual = actual.next;
            }
            displayResult("Board", result);

        }

        public static void CheckCreationPlayers(bool verbose=false)
        {
            bool result = true;
            List<String> names = new List<String>(){"Pierre", "Paul", "Jack"};
            Player players = Game.createPlayers(names.Count, names);

            int cpt = 0;
            while (cpt != 2)
            {
                if (cpt == 0 && verbose) { Console.WriteLine(players.toString()); }
                if (players.ID == names.Count)
                {
                    cpt++;
                    if (players.next.ID != 1){ result = false; }
                }
                else
                {
                    if (players.next.ID != players.ID+1){ result = false; }
                }
                players = players.next;
            }
            displayResult("Creation Players", result);
        }

        public static void CheckThrown()
        {
            bool result = true;
            Player pl = new Player(0, "Test");
            pl.ThrownDice.Enqueue("1,1");
            pl.ThrownDice.Enqueue("2,2");
            pl.ThrownDice.Enqueue("3,3");
            if (!GameController.checkLastThrown(pl)) { result = false; }
            pl.ThrownDice.Dequeue();
            pl.ThrownDice.Dequeue();
            pl.ThrownDice.Dequeue();
            pl.ThrownDice.Enqueue("1,2");
            pl.ThrownDice.Enqueue("1,2");
            pl.ThrownDice.Enqueue("1,2");
            if (GameController.checkLastThrown(pl)) { result = false; }

            displayResult("Three Double Thrown", result);
        }

        public static void CheckBuy()
        {
            bool result = true;
            Player pl = new Player(0, "Test");
            pl.Position = Game.CreateBoard();
            while (pl.Position.Basic_price == -1)
            {
                pl.Position = pl.Position.next;
            }
            int diff = pl.Money - pl.Position.Basic_price;
            Banker banker = Banker.CreateBanker();
            pl.Buy();

            if (pl.Money != diff || banker.Money != pl.Position.Basic_price || pl.Position.Id_buyer != pl.ID || pl.Id_purchased[0] != pl.Position.ID)
            {
                result = false;
            }

            displayResult("Buy", result);
        }

        public static void CheckAllColors()
        {
            bool result = true;
            Player pl = new Player(36, "Test");
            pl.Position = Game.CreateBoard();

            pl.Move(1); // Boulevard de BelleVille
            pl.Buy();
            if (GameController.AllColors(pl)) { result = false; }
            pl.Move(2); // Rue LeCourbe
            pl.Buy();
            if (!GameController.AllColors(pl)) { result = false; }

            displayResult("All same colors", result);
        }

        public static void CheckUniformConstruction()
        {
            bool result = true;
            Player pl = new Player(0, "Test");
            pl.Position = Game.CreateBoard();

            pl.Move(1);
            pl.Position.Nb_house++;
            if (!GameController.UniformConstruction(pl)) { result = false; }
            pl.Position.Nb_house++;
            if (GameController.UniformConstruction(pl)) { result = false; }

            displayResult("Uniform Construction", result);
        }

        public static void CheckGoToJail()
        {
            bool result = true;
            Player pl = new Player(0, "Test");
            pl.Position = Game.CreateBoard();
            pl.GoToJail();

            if (!pl.Position.Name.Equals("Prison") && !pl.InJail)
            {
                result = false;
            }

            displayResult("GoToJail", result);
        }

        public static void Test(bool verbosity=false)
        {
            Console.WriteLine("UNIT TEST: \n");

            CheckBoard(verbosity);
            CheckCreationPlayers(verbosity);
            CheckThrown();
            CheckBuy();
            CheckAllColors();
            CheckUniformConstruction();
            CheckGoToJail();

            final();
        }
    }
}
