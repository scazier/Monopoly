using System;

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
        int dice1 = r.Next(1, 7);
        int dice2 = r.Next(1, 7);
        String actualRound = dice1 + "," + dice2;

        if (pl.ThrownDice.Count == 3)
        {
            pl.ThrownDice.Dequeue();
        }
        pl.ThrownDice.Enqueue(actualRound);

        return dice1 + dice2;
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


    public static Player PayRent(Player pl, int idOwner, String type, int numberDice = 0)
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
                if (tmpBoard.Name.Substring(0, 4).Equals("Gare") && tmpBoard.Id_buyer == idOwner)
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
                if (tmpBoard.Name.Substring(0, 4).Equals("Gare") && tmpBoard.Id_buyer == idOwner)
                {
                    nbPublicCompany++;
                }
            }

            if (nbPublicCompany == 1) { amount = 4 * numberDice; }
            else { amount = 10 * numberDice; }

            //Console.WriteLine("The amount of the rent is: "+amount+"€");
        }

        pl.Money -= amount;
        while (pl.ID != idOwner) { pl = pl.next; }

        pl.Money += amount;
        while (pl.ID != idPl) { pl = pl.next; }

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
                        if (!actualPosition.Name.Substring(0, 4).Equals("Gare") && !actualPosition.Name.Substring(0, 6).Equals("Impôts") && !actualPosition.Name.Substring(0, 4).Equals("Taxe") && !actualPosition.Name.Substring(0, 9).Equals("Compagnie"))
                        {
                            Console.Write("Do you want to add an element (house, hotel)? [y/n]");
                            char action = Convert.ToChar(Console.ReadLine());
                            if (action.Equals('y') || action.Equals('Y'))
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
                            if (actualPosition.Name.Substring(0, 4).Equals("Gare"))
                            {
                                actualPlayer = PayRent(actualPlayer, actualPlayer.Position.Id_buyer, "Train");
                            }
                            else if (actualPosition.Name.Substring(0, 9).Equals("Compagnie"))
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
        public void updateView()
        {
            this.view.displayGame();
        }
    }
}