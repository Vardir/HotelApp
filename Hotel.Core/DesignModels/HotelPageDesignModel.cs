using HotelsApp.Core.ViewModels;

namespace HotelsApp.Core.DesignModels
{
    public class HotelPageDesignModel : HotelPageViewModel
    {
        static HotelPageDesignModel instance;
        public static HotelPageDesignModel Instance => instance ?? (instance = new HotelPageDesignModel());

        private HotelPageDesignModel()
        {
            Hotel.Title = "Tourist Hotel Complex";
            Hotel.Adress = "Dniprovskyj, Kiev";
            Hotel.Reviews = 4747;
            Hotel.Rating = 7.8;
            Hotel.Stars = 3;
            Hotel.Description = @"This property is a 9-minute walk from the beach. Located beside Livoberezhna Metro Station in Kiev, this modern, 3-star hotel offers 2 international restaurants, and a 24-hour reception. The International Exhibition Centre is a 7-minute walk away.
                                The Tourist Hotel Complex has classic-style rooms and suites with satellite TV, refrigerator, and desk. Wi - Fi is available in the property free of charge.
                                The restaurant serves Ukrainian and European specialities.Japanese food can be enjoyed in the sushi bar. Fine drinks are served in the snack bar on the 20th floor. Several restaurants, supermarkets, bars, and eateries are in the vicinity of Tourist Hotel Complex.
                                Kiev Central Station is a 15 minutes' drive away by metro. Borispol Airport is a 40 minutes' drive away. Public parking spaces are available outside Tourist Hotel Complex on request.
                                Dniprovskyj is a great choice for travelers interested in sightseeing, friendly locals and history.
                                Couples in particular like the location – they rated it 8.3 for a two-person trip.
                                We speak your language!";

        }
    }
}