using System;
using System.Collections.Generic;

public class CaseFactory
{
    public static Street getCase(String type, int id, String name, int id_buyer=0, int basic_price=2, String color="", List<int> priceRentHouse=null, int priceRentHotel=0, int price_house=-1, int price_hotel=-1)
    {
        if (type.Equals("Case"))
        {
            return new Street(id, name, -1, -1, color, null, -1, -1, false,-1,-1);
        }
        else if (type.Equals("Buyable"))
        {
            return new Street(id, name, id_buyer, basic_price, color, priceRentHouse, -1, -1, false,-1,-1);
        }
        else if (type.Equals("Street"))
        {
            return new Street(id, name, id_buyer, basic_price, color, priceRentHouse, priceRentHotel, 0, false, price_house, price_hotel);
            // We set the args nb_house at 0 and hotel at false because during the creation, there is no houses or hotel on it.
        }

        return null;
    }
}
