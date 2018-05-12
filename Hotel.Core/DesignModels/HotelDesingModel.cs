using HotelsApp.Core.DataModels;

namespace HotelsApp.Core.DesignModels
{
    public class HotelDesingModel : Hotel
    {
        static HotelDesingModel instance;
        public static HotelDesingModel Instance => instance ?? (instance = new HotelDesingModel());

        private HotelDesingModel()
        {
            Title = "Tourist Hotel Complex";
            Adress = "Dniprovskyj, Kiev";
            Reviews = 4747;
            Rating = 7.8;
            Stars = 3;
            Description = @"This property is a 9-minute walk from the beach. Located beside Livoberezhna Metro Station in Kiev, this modern, 3-star hotel offers 2 international restaurants, and a 24-hour reception. The International Exhibition Centre is a 7-minute walk away.
                            The Tourist Hotel Complex has classic-style rooms and suites with satellite TV, refrigerator, and desk. Wi - Fi is available in the property free of charge.
                            The restaurant serves Ukrainian and European specialities.Japanese food can be enjoyed in the sushi bar. Fine drinks are served in the snack bar on the 20th floor. Several restaurants, supermarkets, bars, and eateries are in the vicinity of Tourist Hotel Complex.
                            Kiev Central Station is a 15 minutes' drive away by metro. Borispol Airport is a 40 minutes' drive away. Public parking spaces are available outside Tourist Hotel Complex on request.
                            Dniprovskyj is a great choice for travelers interested in sightseeing, friendly locals and history.
                            Couples in particular like the location – they rated it 8.3 for a two-person trip.
                            We speak your language!";
        }
    }
}