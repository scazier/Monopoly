using System;
using System.Collections;
using System.Collections.Generic;

namespace Monopoly
{
    public class Player
    {

        private int id;
        private String name;
        private int money;
        private Street position;
        private Queue<String> thrownDice;
        private List<int> id_purchased;
        private bool firstRound;
        private bool inJail;
        private bool cardJail;
        public Player next {get; set; }

        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public int Money
        {
            get { return this.money; }
            set {this.money = value; }
        }

        public Street Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Queue<String> ThrownDice
        {
            get { return this.thrownDice; }
            set { this.thrownDice = value; }
        }

        public List<int> Id_purchased
        {
            get { return this.id_purchased; }
            set { this.id_purchased = value; }
        }

        public bool FirstRound
        {
            get { return this.firstRound; }
            set { this.firstRound = value; }
        }

        public bool InJail
        {
            get { return this.inJail; }
            set { this.inJail = value; }
        }

        public bool CardJail
        {
            get { return this.cardJail; }
            set { this.cardJail = value; }
        }

        public Player(int id, String name)
        {
            this.id = id;
            this.name = name;
            this.money = 1500;
            this.position = null;
            this.thrownDice = new Queue<String>();
            this.id_purchased = new List<int>();
            this.firstRound = true;
            this.inJail = false;
            this.cardJail = false;
            this.next = null;
        }

        public void Move(int step)
        {
            int cpt = 0;
            if (step > 0 && step < 13)
            {
                while (cpt != step)
                {
                    this.Position = this.Position.next;
                    if (this.Position.ID == 1)
                    {
                        this.Money += 200;
                        // If the player goes through the starting box, he win 200€
                        if (this.FirstRound) { this.FirstRound = false; }
                    }
                    cpt++;
                }
                if (this.Position.ID == 1)
                {
                    this.Money += 200;
                    // If the player after the move stop on the starting box, he win 400€
                }
            }
        }

        public void Buy()
        {
            if (this.Position.Basic_price != -1)
            {
                if (this.Position.Id_buyer == 0)
                {
                    if (this.Money >= this.Position.Basic_price)
                    {
                        Banker banker = Banker.CreateBanker();
                        // It's the same banker as in the class Game because this class is a singleton pattern
                        this.Money -= this.Position.Basic_price; // Update the player money
                        banker.Money += this.Position.Basic_price; // Update the banker money
                        banker.Operations.Add(new Exchange(this.ID, "+"+Convert.ToString(this.Position.Basic_price)));
                        this.Position.Id_buyer = this.ID;
                        this.Id_purchased.Add(this.Position.ID);
                    }
                }
            }
        }

        public void AddHouse()
        {
            if (this.Position.Id_buyer == this.ID)
            {
                if (GameController.AllColors(this))
                // If all the properties with the same colors are our propeties we can add a house
                {
                    if (!this.Position.Hotel)
                    {
                        Banker banker = Banker.CreateBanker();
                        if (this.Position.Nb_house == 0 || (this.Position.Nb_house < 4 && GameController.UniformConstruction(this)))
                        {
                            this.Position.Nb_house++;
                            this.Money -= this.Position.PriceHouse;
                            banker.Money += this.Position.PriceHouse;
                            banker.Operations.Add(new Exchange(this.ID, "+"+this.Position.PriceHouse));
                        }
                    }
                }
            }
        }

        public void AddHotel()
        {
            Banker banker = Banker.CreateBanker();
            if (this.Position.Id_buyer == this.ID)
            {
                if (GameController.AllColors(this))
                {
                    if (this.Position.Nb_house == 4)
                    {
                        banker.Money += this.Position.PriceHotel;
                        this.Money -= this.Position.PriceHotel;
                        banker.Operations.Add(new Exchange(this.ID, "+"+this.Position.PriceHotel));
                        this.Position.Nb_house = 0;
                        this.Position.Hotel = true;
                    }
                }
            }
        }

        public void GoToJail()
        {
            Street tmpBoard = this.Position;


            while (!tmpBoard.Name.Equals("Prison"))
            {
                tmpBoard = tmpBoard.next;
                this.Position = this.Position.next;
            }

            if (this.Position.ID > 11)
            {
                /*
                 * When the player go to Jail, if he goes through the start box
                 * he'l win 200 € nut he shouldn't according to the rules 
                 */
                this.Money -= 200;
            }
        }

        public void displayAllPlayers()
        {
            Player pl = this;
            int id_start = this.ID;

            do
            {
                Console.WriteLine(pl.toString());
                pl = pl.next;
            }
            while (pl.ID != id_start);
        }

        public int MoveTo(int idBox)
        {
            Player pl = this;
            int cpt = 0;

            while (pl.Position.ID != idBox)
            {
                pl.Position = pl.Position.next;
                cpt++;
            }
            return cpt;
        }

        public void Card(String type)
        {
            Deck deck = Deck.ReadDeck();
            Content tmpContent = null;

            if (type.Equals("community"))
            {
                tmpContent = deck.Community.Pop();
            }
            else if (type.Equals("chance"))
            {
                tmpContent = deck.Chance.Pop();
            }
            Console.WriteLine(tmpContent.Request);

            String[] actions = tmpContent.Action.Split(',');
            int[] intAction = Array.ConvertAll(actions, s => int.Parse(s));

            for (int i=0; i<intAction.Length; i++)
            {
                if (intAction[i] != 0)
                {
                    switch (i)
                    {
                        case 0:
                            this.Move(this.MoveTo(intAction[i]));
                            break;

                        case 1:
                            // A player can move forward with a certain number of steps
                            this.Move(intAction[i]);
                            break;

                        case 2:
                            // A player can win or lose money with cards
                            this.Money += intAction[i];
                            break;

                        case 3:
                            if (intAction[i] == 1) { this.GoToJail(); }
                            else if (intAction[i] == -1) { this.CardJail = true;; }
                            break;
                    }
                }
            }
        }

        public String emojiPawn()
        {
            String[] emoji = {"\U0001F47D", "\U0001F60B", "\U0001F61C", "\U0001F920", "\U0001F60E", "\U0001F47B"};
            return emoji[this.ID-1];
        }

        public String toString()
        {
            String output = "Player [id: "+this.id+", Name: "+this.name+", Money: "+this.money+", Purchased: [";
            if (this.id_purchased.Count != 0)
            {
                output += this.id_purchased[0];
                for (int i=1; i<this.id_purchased.Count; i++)
                {
                    output += ", " + this.id_purchased[i];
                }
            }
            output += "]";
            if (this.position != null)
            {
                output += ", Position: "+this.position.toString();
            }
            output += ", First Round: "+this.firstRound+", In jail: "+this.inJail+ ", Card out of jail: "+this.cardJail+", Thrown Dice: [";
            if (this.thrownDice.Count != 0)
            {
                Queue<String> q = new Queue<String>(this.thrownDice);
                output += "{"+q.Dequeue()+"}";
                while (q.Count != 0)
                {
                    output +=" - {"+q.Dequeue()+ "}";
                }
            }
            output += "]";
            return output;
        }
    }
}
