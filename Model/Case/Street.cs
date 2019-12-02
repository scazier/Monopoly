using System;
using System.Collections.Generic;

public class Street:Buyable
{
    private String color;
    private int nb_house;
    private bool hotel;
    private List<int> priceRentHouse;
    private int priceRentHotel;
    private int priceHouse;
    private int priceHotel;

    public Street next { get; set; }

    public String Color
    {
        get { return this.color; }
    }

    public int Nb_house
    {
        get { return this.nb_house; }
        set { this.nb_house = value; }
    }

    public int PriceRentHotel
    {
        get { return this.priceRentHotel; }
        set { this.priceRentHotel = value; }
    }

    public List<int> PriceRentHouse
    {
        get { return this.priceRentHouse; }
        set { this.priceRentHouse = value; }
    }

    public bool Hotel
    {
        get { return this.hotel; }
        set { this.hotel = value; }
    }

    public int PriceHouse
    {
        get { return this.priceHouse; }
    }

    public int PriceHotel
    {
        get { return this.priceHotel; }
    }

    public Street(int id, String name, int id_buyer, int basic_price, String color, List<int> priceRentHouse, int priceRentHotel, int nb_house, bool hotel, int priceHouse, int priceHotel) : base(id, name, id_buyer, basic_price)
    {
        //this.basic_price = basic_price;
        this.color =  color;
        this.nb_house = nb_house;
        this.hotel = false;
        this.priceRentHouse = priceRentHouse;
        this.priceRentHotel = priceRentHotel;
        this.priceHouse = priceHouse;
        this.priceHotel = priceHotel;
    }

    public override String toString()
    {
        String output = "Case [id: "+this.id+", name: "+this.name;
        if (this.basic_price == -1)
        {
            output += "]";
        }
        else
        {
            output += ", Basic price: "+this.basic_price;
            if (this.nb_house == -1)
            {
                output += "]";
            }
            else
            {
                output += ", Color: "+this.color+", Nb house: "+this.nb_house+", Hotel on it: "+this.hotel+", Id buyer: "+this.id_buyer+", Price Rent houses: "+this.priceRentHouse[0];
                for (int i=1; i<this.priceRentHouse.Count; i++)
                {
                    output += "-"+this.priceRentHouse[i];
                }
                output += "], Price Rent Hotel: "+this.priceRentHotel+", Price House: "+this.priceHouse+", Price Hotel: "+this.priceHotel+"]";
            }
        }
        return output;
    }
}
