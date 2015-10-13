using System;
using Transport.Enums;
using Transport.Model;
using Transport.Model.Carriages;
using Transport.Model.Trains;

namespace Transport
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 50);
            BaggageCarriage baggageCarriage = new BaggageCarriage(2001, new DateTime(2012, 6, 1), 4, 5, 50);
            RestaurantCarriage restaurantCarriage = new RestaurantCarriage(2322, new DateTime(2010, 6, 8), 6, 75,
                "Ресторан Бистро");
            PassengerTrain train1 = new PassengerTrain(1245, new DateTime(2010, 6, 22),
                new Locomotive(1296, new DateTime(2012, 4, 8), 12500, TypeEnergy.Electric, 90))
            {
                new PassengerCarriage(2, new DateTime(2008, 6, 7), 12, 10, TypePassengerCarriage.PremiumClass),
                new PassengerCarriage(1, new DateTime(2006, 10, 28), 4, 12, TypePassengerCarriage.Sedentary),
                new PassengerCarriage(3, new DateTime(2002, 11, 30), 8, 15, TypePassengerCarriage.Sleeping)
            };
            train1.BaggageCarriage = baggageCarriage;
            train1.RestaurantCarriage = restaurantCarriage;
            train1.TakePlace(1, 6);
            train1.TakePlace(2, 6);
            train1.TakePlace(3, 1);
            train1.TakePlace(3, 2);
            train1.TakePlace(1, 10);
            train1.BaggageCarriage.Add(new Baggage { Name = "Собака", Number = 1254, Weight = 12.3 });
            train1.BaggageCarriage.Add(new Baggage { Name = "Сумка", Number = 1245, Weight = 5.6 });
            train1.BaggageCarriage.Add(new Baggage { Name = "Холодильник", Number = 1578, Weight = 38.6 });
            train1.SortByUserCondition();
            Console.WriteLine(train1.Print());
            Console.WriteLine("Общ. кол-во мест: " + train1.TotalPlacesCount);
            Console.WriteLine("Общ. кол-во свободных мест: " + train1.TotalFreePlacesCount);
            Console.WriteLine("Общ. кол-во занятых мест: " + train1.TotalBusyPlacesCount);
            Console.WriteLine("Общ. вес багажа: " + train1.BaggageCarriage.TotalWeight + "\n");
            FreightTrain train2 = new FreightTrain(1287, new DateTime(2012, 6, 22),
                new Locomotive(1295, new DateTime(2012, 9, 21), 12800, TypeEnergy.GasTurbine, 70))
            {
                new FreightCarriage(2, new DateTime(2014, 10, 8), 12, "Калийная соль", TypeFreightCarriage.СoveredCarriage, 120, 100),
                new FreightCarriage(3, new DateTime(2012, 6, 7), 12, "Торф", TypeFreightCarriage.HalfCarriage, 75, 50),
                new FreightCarriage(1, new DateTime(2001, 6, 18), 4, "Мел", TypeFreightCarriage.СoveredCarriage, 100, 98)
            };
            train2.SortByUserCondition();
            Console.WriteLine(train2.Print());
            Console.WriteLine("Общ. вместимость: " + train2.TotalVolume);
            Console.WriteLine("Общ. занятое место: " + train2.TotalOccupiedVolume);
            Console.WriteLine("Общ. кол-во занятых мест: " + train2.TotalFreeVolue);
            Console.WriteLine("Процент свободного места: " + train2.PercentageTotalFreeVolue + "%");
        }
    }
}
