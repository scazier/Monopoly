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
                Console.Clear();
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

            public static String FindNameByID(Street tmpBoard, int id)
            {
                while (tmpBoard.ID != id)
                {
                    tmpBoard = tmpBoard.next;
                }
                return tmpBoard.Name;
            }
    }

    public class GameView
    {
        private String view;

        public String View
        {
            get { return this.view; }
            set { this.view = value; }
        }

        public GameView()
        {
            this.view = "";
        }

        String baseView =
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
            "|________________|________________|________________|________________|________________|________________|________________|________________|________________|________________|_________________|";


        public static String getHouseHotel(Street tmpBoard, int id)
        {
            String output = "";
            while (tmpBoard.ID != id)
            {
                tmpBoard = tmpBoard.next;
            }
            if (tmpBoard.Nb_house != -1)
            {
                output = ", House(s): "+tmpBoard.Nb_house + ", Hotel: " + tmpBoard.Hotel;
            }
            return output;
        }

        public void applyPosition(Player pl)
        {
            Player tmpPlayer = pl;
            //String fullGame = baseView;
            int id_start = pl.ID;
            int cptLine = 0;
            String[] game = baseView.Split('\n');

            do
            {
                String playerAttribute = "Player "+tmpPlayer.ID+"-> Name: "+tmpPlayer.Name+" - Pawn: "+tmpPlayer.emojiPawn()+" - Money: "+tmpPlayer.Money;
                game[9+tmpPlayer.ID] = game[9+tmpPlayer.ID].Substring(0,55) + playerAttribute +
                                        game[9+tmpPlayer.ID].Substring(55 + playerAttribute.Length, game[13+tmpPlayer.ID].Length - 55 - playerAttribute.Length);


                if (tmpPlayer.Id_purchased.Count != 0)
                {
                    //The purchased boxes will are displayed on two columns
                    int index = 0;
                    while (index < tmpPlayer.Id_purchased.Count)
                    {
                        String properties = Game.FindNameByID(tmpPlayer.Position, tmpPlayer.Id_purchased[index]) + " -> Owner: " + tmpPlayer.emojiPawn() + getHouseHotel(tmpPlayer.Position, tmpPlayer.Id_purchased[index]);
                        game[16 + cptLine] = game[16 + cptLine].Substring(0,54) + properties +
                                             game[16 + cptLine].Substring(54+properties.Length, game[16 + cptLine].Length - properties.Length - 54);
                        index++;
                        cptLine++;
                    }
                }

                tmpPlayer = tmpPlayer.next;
            } while (tmpPlayer.ID != id_start);


            do
            {
                // Display yhr position of the players
                int pos = tmpPlayer.Position.ID;
                int idPlayer = tmpPlayer.ID;
                int viewLine = 0;
                int idCharLine = 3 + (idPlayer - 1) * 2;

                if (pos >= 1 && pos <= 11)
                {
                    viewLine = 50;
                    idCharLine += 1 + (11 - pos) * 17;
                }

                if (pos >= 21 && pos <= 31)
                {
                    viewLine = 6;
                    idCharLine += 1 + (pos - 21) * 17;
                }

                if (pos > 11 && pos < 21 || pos > 31 && pos <= 40)
                {
                    if (pos > 31 && pos <= 40) { idCharLine += 10 * 17; }

                    switch (pos)
                    {
                        case 12:
                        case 40:
                            viewLine = 42;
                            break;

                        case 13:
                        case 39:
                            viewLine = 38;
                            break;

                        case 14:
                        case 38:
                            viewLine = 34;
                            break;

                        case 15:
                        case 37:
                            viewLine = 30;
                            break;

                        case 16:
                        case 36:
                            viewLine = 26;
                            break;


                        case 17:
                        case 35:
                            viewLine = 22;
                            break;

                        case 18:
                        case 34:
                            viewLine = 18;
                            break;

                        case 19:
                        case 33:
                            viewLine = 14;
                            break;

                        case 20:
                        case 32:
                            viewLine = 10;
                            break;
                    }
                }

                game[viewLine] = game[viewLine].Substring(0, idCharLine-1) + tmpPlayer.emojiPawn() + game[viewLine].Substring(idCharLine+1, game[viewLine].Length - idCharLine - 1);
                tmpPlayer = tmpPlayer.next;
            } while (tmpPlayer.ID != id_start);

            for (int i=0; i<game.Length; i++)
            {
                Console.WriteLine(game[i]);
            }
            //return game;
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

        public static int[] Dice(Player pl)
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

            return new int[] {dice1, dice2};
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

                do
                {
                    // We search all the train stations the owner has
                    if (tmpBoard.Name.Substring(0,4).Equals("Gare") && tmpBoard.Id_buyer == idOwner)
                    {
                        nbTrainStation++;
                    }
                    tmpBoard = tmpBoard.next;
                }while (tmpBoard.ID != id_start);

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
                    if (tmpBoard.Name.Substring(0,4).Equals("Compagnie") && tmpBoard.Id_buyer == idOwner)
                    {
                        nbPublicCompany++;
                    }
                    tmpBoard = tmpBoard.next;
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
                if (actualPosition.Name.Substring(0, 6).Equals("Impôts") || actualPosition.Name.Substring(0, 4).Equals("Taxe"))
                {
                    actualPlayer.Money -= actualPosition.Basic_price;
                }
                else
                {
                    // If the player can buy this box
                    if (!actualPlayer.FirstRound)
                    {
                        // The player cannot buy on the first round
                        if (actualPosition.Id_buyer == 0)
                        {
                            char action = 'a';
                            while (!action.Equals('y') && !action.Equals('Y') && !action.Equals('n') && !action.Equals('N'))
                            {
                                Console.Write("Do you want to buy this property? [y/n]");
                                String input = Console.ReadLine();
                                if (input.Length == 1) { action = Convert.ToChar(input); }
                                // If id_buyer = 0, nobody own this box
                            }

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
                                    char actionAdd = 'a';
                                    while (!actionAdd.Equals('y') && !actionAdd.Equals('Y') && !actionAdd.Equals('n') && !actionAdd.Equals('N'))
                                    {
                                        Console.Write("Do you want to add an element (house, hotel)? [y/n]");
                                        String input = Console.ReadLine();
                                        if (input.Length == 1) { actionAdd = Convert.ToChar(input); }
                                    }

                                    if (actionAdd.Equals('y') || actionAdd.Equals('Y'))
                                    {
                                        int answer = -1;
                                        while (answer != 0 && answer != 1 && answer != 2)
                                        {
                                            Console.WriteLine("n0 - Continue\n1 - Add House\n2 - Replace by Hotel");
                                            Console.WriteLine(">>> ");
                                        }
                                        answer = Convert.ToInt32(Console.ReadLine());
                                        switch (answer)
                                        {
                                            case 0:
                                                break;

                                            case 1:
                                                actualPlayer.AddHouse();
                                                Console.WriteLine("House added on " + actualPlayer.Position.Name);
                                                break;

                                            case 2:
                                                actualPlayer.AddHotel();
                                                Console.WriteLine("Hotel added on " + actualPlayer.Position.Name);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
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
            }
            else
            {
                if (actualPosition.Name.Equals("Caisse de communauté"))
                {
                    actualPlayer.Card("community");
                }
                else if (actualPosition.Name.Equals("Chance"))
                {
                    actualPlayer.Card("chance");

                }
                else if (actualPosition.Name.Equals("Allez en Prison"))
                {
                    actualPlayer.GoToJail();
                    actualPlayer.InJail = true;
                }
                else if (actualPosition.Name.Equals("Parc Gratuit"))
                {
                    // Gratuit
                }
            }
        }

        public Player updatePlayers(Player pl, int idPlayer)
        {
            Player tmpPlayer = pl;

            while (tmpPlayer.next.ID != idPlayer)
            {
                tmpPlayer = tmpPlayer.next;
            }

            tmpPlayer.next = tmpPlayer.next.next;
            return tmpPlayer;
        }

        public void Round(Player pl)
        {
            int[] resultDices = {1, 0};
            int cptNumberDouble = 0;

            do
            {
                if (cptNumberDouble == 3)
                {
                    pl.GoToJail();
                    pl.InJail = true;
                }

                if ( resultDices[0] == resultDices[1])
                {
                    cptNumberDouble++;
                }
                resultDices = Dice(pl);
                int sumDices = resultDices[0]+resultDices[1];
                Console.WriteLine("Result Dice of "+pl.Name+": "+resultDices[0]+" and "+resultDices[1]);
                pl.Move(sumDices);
                if (pl.Position.Basic_price != -1 && !pl.FirstRound) { displayGame(); }
                Action(pl, sumDices);
                displayGame();

                // Chance card and jail

                if (pl.Money <= 0) { updatePlayers(pl, pl.ID); }

                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
            }while (resultDices[0] == resultDices[1]);
        }

        public void playGame()
        {
            while (this.model.Players.next.ID != this.model.Players.ID)
            {
                if (!this.model.Players.InJail)
                {
                    // while there is more than one player, we continue the game
                    Round(this.model.Players);
                    this.model.Players = this.model.Players.next;
                }
                else
                {
                    this.model.Players.InJail = false;
                }
            }
            Console.WriteLine("Winner is: " + this.model.Players.Name);
        }

        public void displayGame()
        {
            this.view.applyPosition(this.model.Players);
        }
    }
}
