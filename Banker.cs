using System;
using System.Collections;
using System.Collections.Generic;

public class Exchange
{
    private int id_buyer;
    private String amount;

    public int Id_buyer
    {
        get { return this.id_buyer; }
        set { this.id_buyer = value; }
    }

    public String Amount
    {
        get { return this.amount; }
        set { this.amount = value; }
    }

    public Exchange(int id_buyer, String amount)
    {
        this.id_buyer = id_buyer;
        this.amount = amount;
    }

    public String toString()
    {
        String output = "<"+this.id_buyer+", "+this.amount+">";
        return output;
    }
}

public class Banker
{
    /*
     * This class is a singleton pattern because
     * there is only one banker in the game
     */
    private static Banker _object;
    private int money;
    private List<Exchange> operations; // <id_buyer, amount>

    public int Money
    {
        get { return this.money; }
        set { this.money = value; }
    }

    public List<Exchange> Operations
    {
        get { return this.operations; }
        set { this.operations = value; }
    }

    public int Id { get; set; }

    private Banker()
    {
            this.money = 0;
            this.operations = new List<Exchange>();
            Id += 1;
    }

    public static Banker CreateBanker()
    {
        if (_object == null)
        {
            _object = new Banker();
        }
        return _object;
    }

    public String toString()
    {
        String output = "Banker [Id: "+this.Id+", Money: "+this.money;
        if (this.operations.Count >= 1)
        {
            output += ", Operations: ["+this.operations[0].toString();
            for (int i=1; i<this.operations.Count; i++)
            {
                output += " - "+this.operations[i].toString();
            }
        }
        output += "]";
        return output;
    }
}
