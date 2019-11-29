using System;

public class Buyable:Case
{
    protected int id_buyer;
    protected int basic_price;

    public int Id_buyer
    {
        get { return this.id_buyer; }
        set { this.id_buyer = value; }
    }

    public int Basic_price
    {
        get { return this.basic_price; }
        set { this.basic_price = value; }
    }

    public Buyable(int id, String name, int id_buyable, int basic_price) : base(id, name)
    {
        this.id_buyer = id_buyable;
        this.basic_price = basic_price;
    }

    public override String toString()
    {
        String output = base.toString() + " - id_buyer: "+this.id_buyer;
        return output;
    }
}
