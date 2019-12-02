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




}
