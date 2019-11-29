using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Monopoly
{
    public class Game
    {
            private int jackpot;
            private Street board;
            private Player players;

            public int Jackpot
            {
                get { return this.jackpot; }
                set { this.jackpot = value; }
            }

            public Street Board
            {
                get { return this.board; }
                set { this.board = value; }
            }

            public Player Players
            {
                get { return this.players; }
                set { this.players = value; }
            }

            public Game()
            {
                this.jackpot = 0;
                this.board = CreateBoard();
                this.players = getPlayers();
            }

            public static Street CreateBoard()
            {
                /*
                 * This method build the game with all the boxes according to the classes Case, Buyable, Street
                 * The output is a ringed linked list
                 */
                Street first = null;
                try
                {
                    String[] lines = File.ReadAllLines(@"data_cases.txt");

                    Street actual = null;
                    for (int id_line=0; id_line<lines.Length; id_line++)
                    {
                        String line = lines[id_line];
                        // Is it the last line of data_case.txt which is the resource link?
                        if (!line[0].Equals('#'))
                        {
                            String[] data = line.Split(",");
                            // Is it the first line of data_case.txt which describe the data below

                            if (!data[0].Equals("id"))
                            {
                                int id = Convert.ToInt32(data[0]);
                                Street temp = null;
                                switch (data.Length)
                                {
                                    case 2:
                                        if (id == 0)
                                        {
                                            temp = CaseFactory.getCase("Case",id+1, data[1]);
                                            first = actual = temp;
                                            // This section is to set the beginning of the ringed chained list
                                        }
                                        else
                                        {
                                            temp = CaseFactory.getCase("Case", id+1, data[1]);
                                            // Structure: type, id, name
                                        }
                                        break;

                                    case 3:
                                        temp = CaseFactory.getCase("Buyable", id+1, data[1], 0, Convert.ToInt32(data[2]));
                                        // Structure: type, id, name, id_buyer, basic_price
                                        break;

                                    case 12:
                                        List<int> price_house = new List<int>();
                                        for (int i=4; i<9; i++)
                                        {
                                            price_house.Add(Convert.ToInt32(data[i]));
                                        }
                                        temp = CaseFactory.getCase("Street", id+1, data[1], 0, Convert.ToInt32(data[2]), data[3], price_house, Convert.ToInt32(data[9]), Convert.ToInt32(data[10]), Convert.ToInt32(data[11]));
                                        // Structure: type, id, name, id_buyer, basic_price, price_house, price_hotel
                                        break;
                                }
                                actual.next = temp;
                                actual = actual.next;
                            }

                        }
                    }
                    actual.next = first;
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                return first;
            }

            public static Player getPlayers()
            {
                /*
                 * This method is just a wrapper for createPlayers()
                 */
                int nb_players = 0;
                while (nb_players < 2 || nb_players > 6)
                {
                    Console.Write("Enter the number of players (between 2 and 6): ");
                    String answer = Console.ReadLine();
                    char answer_char = Char.Parse(answer);
                    int ASCII_value = (int)answer_char;
                    // Check if the input is a number
                    if (ASCII_value > 47 && ASCII_value < 58)
                    {
                        nb_players = Convert.ToInt32(answer);
                    }
                    else
                    {
                        Console.WriteLine("Enter a number!!");
                    }
                }

                List<String> names = new List<String>();

                for (int id_player=0; id_player<nb_players; id_player++)
                {
                    Console.Write("Name of player "+Convert.ToString(id_player+1)+": ");
                    String tmpName = Console.ReadLine();
                    names.Add(tmpName);
                }

                return createPlayers(nb_players, names);
            }

            public static Player createPlayers(int nb_players, List<String> names)
            {
                /*
                 * This method create all the player (between 2 and 6 players).
                 * The output is also a ringed linked list as the Board
                 */
                Player firstPlayer = null;
                Player actualPlayer = null;
                for (int i=0; i<nb_players; i++)
                {
                    Player tmpPlayer = null;

                    String tmpName = names[i];

                    if (i == 0)
                    {
                        tmpPlayer = new Player(i+1, tmpName);
                        firstPlayer = actualPlayer = tmpPlayer;
                    }
                    else
                    {
                        tmpPlayer = new Player(i+1, tmpName);
                        actualPlayer.next = tmpPlayer;
                        actualPlayer = actualPlayer.next;
                    }
                }
                actualPlayer.next = firstPlayer;
                return firstPlayer;
            }
    }

    public class GameView
    {
        public void displayGame()
        {
            String game =
            "_____________________________________________________________________________________________________________________________________________________________________________________________\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|      Parc      |     Avenue     |     Chance     |   Boulevard    |     Avenue     |     Gare du    |    Faubourg    |    Place de    | Compagnie des  |     Rue la     | Allez en Prison |\n"+
            "|     Gratuit    |    Matignon    |                |  MalesHerbes   |  Henri-Martin  |      Nord      |  Saint-Honoré  |    la Bourse   |      eaux      |     Fayette    |                 |\n"+
            "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|                |      220 €     |                |      220 €     |      240 €     |      200 €     |      260 €     |      260 €     |      150 €     |      280 €     |                 |\n"+
            "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n"+
            "|                |____________________________                                                                                              ______________________________|                 |\n"+
            "|                |        Place Pigalle       |                                                                                             |     Avenue de Breteuil      |                 |\n"+
            "|      200 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |   Boulevard Saint-Michel   |                                                                                             |         Avenue Foch         |                 |\n"+
            "|      180 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |     Caisse de communauté   |                                                                                             |     Caisse de communauté    |                 |\n"+
            "|                |____________________________|                                                                                             |_____________________________|                 |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |        Avenue Mozart       |                                                                                             |   Boulevard des capucines   |                 |\n"+
            "|      180 €     |____________________________|                                                                                             |_____________________________|       300 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |          Gare Lyon         |                                                                                             |      Gare Saint-Lazare      |                 |\n"+
            "|      200 €     |____________________________|                                                                                             |_____________________________|       200 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |       Rue de Paradis       |                                                                                             |           Chance            |                 |\n"+
            "|      160 €     |____________________________|                                                                                             |_____________________________|                 |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |      Avenue de Neuilly     |                                                                                             |  Avenue des Champs-Elysées  |                 |\n"+
            "|      140 €     |____________________________|                                                                                             |_____________________________|       350 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |   Compagnie d'électricité  |                                                                                             |        Taxe de Luxe         |                 |\n"+
            "|      150 €     |____________________________|                                                                                             |_____________________________|       100 €     |\n"+
            "|________________|                                                                                                                                                        |_________________|\n"+
            "|                |____________________________                                                                                               _____________________________|                 |\n"+
            "|                |  Boulevard de la Villette  |                                                                                             |       Rue de la Paix        |                 |\n"+
            "|      140 €     |____________________________|                                                                                             |_____________________________|       400 €     |\n"+
            "|________________|________________________________________________________________________________________________________________________________________________________|_________________|\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|      Prison    |   Avenue de    |    Rue des     |     Chance     |     Rue de     |      Gare      |      Impôt     |       Rue      |     Caisse     |  Boulevard de  |      Départ     |\n"+
            "|                | la République  |   Courcelles   |                |    Vaugirard   |  Montparnasse  |  sur le revenu |     LeCourbe   |  de communauté |   BelleVille   |       <--       |\n"+
            "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|                |                |                |                |                |                |                |                |                |                |                 |\n"+
            "|                |      120 €     |      100 €     |                |      100 €     |      200 €     |      200 €     |       60 €     |                |      60 €      |                 |\n"+
            "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|\n";
            Console.WriteLine(game);
            // String game =
            // "_______________________________________________________________________________________________________________________________________________________________________________________________________________________________________\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|        Parc        |       Avenue       |       Chance       |     Boulevard      |       Avenue       |       Gare du      |      Faubourg      |      Place de      |   Compagnie des    |       Rue la       |   Allez en Prison  |\n"+
            // "|       Gratuit      |      Matignon      |                    |    MalesHerbes     |    Henri-Martin    |        Nord        |    Saint-Honoré    |      la Bourse     |        eaux        |       Fayette      |                    |\n"+
            // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|                    |        220 €       |                    |        220 €       |        240 €       |        200 €       |        260 €       |        260 €       |        150 €       |        280 €       |                    |\n"+
            // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
            // "|                    |____________________________                                                                                                                                  ______________________________|                    |\n"+
            // "|                    |        Place Pigalle       |                                                                                                                                 |     Avenue de Breteuil      |                    |\n"+
            // "|        200 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |   Boulevard Saint-Michel   |                                                                                                                                 |         Avenue Foch         |                    |\n"+
            // "|        180 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |     Caisse de communauté   |                                                                                                                                 |     Caisse de communauté    |                    |\n"+
            // "|                    |____________________________|                                                                                                                                 |_____________________________|                    |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |        Avenue Mozart       |                                                                                                                                 |   Boulevard des capucines   |                    |\n"+
            // "|        180 €       |____________________________|                                                                                                                                 |_____________________________|        300 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |          Gare Lyon         |                                                                                                                                 |      Gare Saint-Lazare      |                    |\n"+
            // "|        200 €       |____________________________|                                                                                                                                 |_____________________________|        200 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |       Rue de Paradis       |                                                                                                                                 |           Chance            |                    |\n"+
            // "|        160 €       |____________________________|                                                                                                                                 |_____________________________|                    |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |      Avenue de Neuilly     |                                                                                                                                 |  Avenue des Champs-Elysées  |                    |\n"+
            // "|        140 €       |____________________________|                                                                                                                                 |_____________________________|        350 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |   Compagnie d'électricité  |                                                                                                                                 |        Taxe de Luxe         |                    |\n"+
            // "|        150 €       |____________________________|                                                                                                                                 |_____________________________|        100 €       |\n"+
            // "|____________________|                                                                                                                                                                                            |____________________|\n"+
            // "|                    |____________________________                                                                                                                                   _____________________________|                    |\n"+
            // "|                    |  Boulevard de la Villette  |                                                                                                                                 |       Rue de la Paix        |                    |\n"+
            // "|        140 €       |____________________________|                                                                                                                                 |_____________________________|        400 €       |\n"+
            // "|____________________|____________________________________________________________________________________________________________________________________________________________________________________________|____________________|\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|        Prison      |     Avenue de      |      Rue des       |       Chance       |       Rue de       |        Gare        |        Impôt       |         Rue        |       Caisse       |    Boulevard de    |       Départ       |\n"+
            // "|                    |   la République    |     Courcelles     |                    |      Vaugirard     |    Montparnasse    |    sur le revenu   |       LeCourbe     |    de communauté   |     BelleVille     |         <--        |\n"+
            // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |                    |\n"+
            // "|                    |        120 €       |        100 €       |                    |        100 €       |        200 €       |        200 €       |         60 €       |                    |         60 €       |                    |\n"+
            // "|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|____________________|\n";
            //
        }
    }

    public class GameController
    {
        private Game model;
        private GameView view;

        public GameController(Game model, GameView view)
        {
            this.model = model;
            this.view = view;
        }


        public void initializePlayers()
        {
            /*
             * This method just set the position of each player on the first box on the board
             */
            Street Board = this.model.Board;
            Player pl = this.model.Players;
            pl.Position = Board;
            while (pl.next.ID != 1)
            {
                pl = pl.next;
                pl.Position = Board;
            }
        }

        public static int Dice(Player pl)
        {
            /*
             * Simule the roll of dices and return the number of boxes to go
             */
            Random r = new Random();
            // Number between 1 and 6
            int dice1 = r.Next(1,7);
            int dice2 = r.Next(1,7);
            String actualRound = dice1+","+dice2;

            if (pl.ThrownDice.Count == 3)
            {
                pl.ThrownDice.Dequeue();
            }
            pl.ThrownDice.Enqueue(actualRound);

            return dice1+dice2;
        }

        public static bool checkLastThrown(Player pl)
        {
            /*
             * Check if the last three throws were doubles to know if the
             * is going in jail or not.
             */
            bool goInJail = false;
            Queue<String> tmpThrown = new Queue<String>(pl.ThrownDice);
            // We do a deep copy of the queue to affect only tmpThrown

            if (tmpThrown.Count == 3)
            {
                int isDouble, cpt;
                isDouble = cpt = 0;

                while (cpt != 3)
                {
                    String diceRound = tmpThrown.Dequeue();
                    String[] vals = diceRound.Split(',');
                    int val1 = Convert.ToInt32(vals[0]);
                    int val2 = Convert.ToInt32(vals[1]);
                    if (val1 == val2) { isDouble++; }
                    cpt++;
                }
                if (isDouble == 3) { goInJail = true; }
            }
            return goInJail;
        }

        public static bool AllColors(Player pl)
        {
            /*
             * This methods is used to know if a player own all the properties
             * of a same color. The color is defined by the position of the player
             */
             if (!pl.Position.Color.Equals(""))
             {
                 String color = pl.Position.Color;
                 int id_start = pl.Position.ID;

                 Street tmpBoard = pl.Position;
                 // We create another Board to not alter the real one

                 bool result = true;
                 while (tmpBoard.next.ID != 1)
                 {
                     if (tmpBoard.Color.Equals(color))
                     {
                         if (tmpBoard.Id_buyer != pl.ID)
                         {
                             result = false;
                         }
                     }
                     tmpBoard = tmpBoard.next;
                 }
                 return result;
             }
             return false;
        }

        public static bool UniformConstruction(Player pl)
        {
            /*
             * This method check if the constructions of the player is uniform
             * A player can not add a second house if all the other properties
             * with the same colors have one or two house(s)
             */
            Street tmpBoard = pl.Position;
            bool result = true;
            int tmpNbHouse = pl.Position.Nb_house;
            int id_start = pl.Position.ID;

            while (tmpBoard.next.ID != id_start)
            {
                if (tmpBoard.Color.Equals(pl.Position.Color))
                {
                    if ((tmpNbHouse - tmpBoard.Nb_house) > 1)
                    {
                        result = false;
                    }
                }
                tmpBoard = tmpBoard.next;
            }

            return result;
        }


        public static Player PayRent(Player pl, int idOwner, String type, int numberDice=0)
        {
            int amount = 0;
            int idPl = pl.ID;

            if (type.Equals("Street"))
            {
                if (!pl.Position.Hotel) { amount = pl.Position.PriceRentHouse[pl.Position.Nb_house]; }
                else { amount = pl.Position.PriceRentHotel; }
            }
            else if (type.Equals("Train"))
            {
                Street tmpBoard = pl.Position;
                int nbTrainStation = 1;
                int id_start = tmpBoard.ID;

                while (tmpBoard.next.ID != id_start)
                {
                    // We search all the train stations the owner has
                    if (tmpBoard.Name.Substring(0,4).Equals("Gare") && tmpBoard.Id_buyer == idOwner)
                    {
                        nbTrainStation++;
                    }
                }
                switch (nbTrainStation)
                {
                    case 1:
                        amount = 25;
                        break;

                    case 2:
                        amount = 50;
                        break;

                    case 3:
                        amount = 100;
                        break;

                    case 4:
                        amount = 200;
                        break;
                }
            }
            else if (type.Equals("Company"))
            {
                Street tmpBoard = pl.Position;
                int nbPublicCompany = 1;
                int id_start = tmpBoard.ID;

                while (tmpBoard.next.ID != id_start)
                {
                    // We search all the train stations the owner has
                    if (tmpBoard.Name.Substring(0,4).Equals("Gare") && tmpBoard.Id_buyer == idOwner)
                    {
                        nbPublicCompany++;
                    }
                }

                if (nbPublicCompany == 1) { amount = 4 * numberDice; }
                else { amount = 10 * numberDice; }

                //Console.WriteLine("The amount of the rent is: "+amount+"€");
            }

            pl.Money -= amount;
            while (pl.ID != idOwner){ pl = pl.next;}

            pl.Money += amount;
            while (pl.ID != idPl){ pl = pl.next; }

            return pl;
        }

        public static void Action(Player pl, int numberDice)
        {
            Player actualPlayer = pl; // Peut être deep copy à faire
            Street actualPosition = actualPlayer.Position;

            if (actualPosition.Basic_price != -1)
            {
                // If the player can buy this box
                if (!actualPlayer.FirstRound)
                {
                    // The player cannot buy on the first round
                    if (actualPosition.Id_buyer == 0)
                    {
                        Console.Write("Do you want to buy this property? [y/n]");
                        char action = Convert.ToChar(Console.ReadLine());
                        // If id_buyer = 0, nobody own this box
                        if (action.Equals('y') || action.Equals('Y'))
                        {
                            actualPlayer.Buy();
                        }
                    }
                    else
                    {
                        if (actualPosition.Id_buyer == actualPlayer.ID)
                        {
                            // If the actual player bought thi box before
                            if (!actualPosition.Name.Substring(0,4).Equals("Gare") && !actualPosition.Name.Substring(0,6).Equals("Impôts") &&  !actualPosition.Name.Substring(0,4).Equals("Taxe") && !actualPosition.Name.Substring(0,9).Equals("Compagnie"))
                            {
                                Console.Write("Do you want to add an element (house, hotel)? [y/n]");
                                char action = Convert.ToChar(Console.ReadLine());
                                if (action.Equals('y') || action.Equals('Y'))
                                {
                                    Console.WriteLine("1 - Add House\n2 - Replace by Hotel\0 - Continue");
                                    int answer = Convert.ToInt32(Console.ReadLine());
                                    switch (answer)
                                    {
                                        case 1:
                                            actualPlayer.AddHouse();
                                            break;

                                        case 2:
                                            actualPlayer.AddHotel();
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            // loyer don't forget colors
                            if (actualPosition.Nb_house != -1)
                            {
                                actualPlayer = PayRent(actualPlayer, actualPlayer.Position.Id_buyer, "Street");
                            }
                            else
                            {
                                if (actualPosition.Name.Substring(0,4).Equals("Gare"))
                                {
                                    actualPlayer = PayRent(actualPlayer, actualPlayer.Position.Id_buyer, "Train");
                                }
                                else if (actualPosition.Name.Substring(0,9).Equals("Compagnie"))
                                {
                                    actualPlayer = PayRent(actualPlayer, actualPlayer.Position.Id_buyer, "Company", numberDice);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (actualPosition.Name.Equals("Caisse de communauté"))
                {
                    // Communaute
                }
                else if (actualPosition.Name.Equals("Chance"))
                {
                    // CHance
                }
                else if (actualPosition.Name.Equals("Allez en Prison"))
                {
                    actualPlayer.GoToJail();
                }
                else if (actualPosition.Name.Equals("Parc Gratuit"))
                {
                    // Gratuit
                }
            }
        }

        public void updateView()
        {
            this.view.displayGame();
        }
    }
}
