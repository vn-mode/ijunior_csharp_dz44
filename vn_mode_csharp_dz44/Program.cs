using System;

namespace TrainPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                TrainDirection direction = CreateDirection();
                SellTickets(direction);
                Train currentTrain = CreateTrain(direction);
                SendTrain(currentTrain, direction);
            }
        }

        static TrainDirection CreateDirection()
        {
            Console.WriteLine(Constants.EnterDeparture);
            string departure = Console.ReadLine();
            Console.WriteLine(Constants.EnterDestination);
            string destination = Console.ReadLine();
            TrainDirection direction = new TrainDirection(departure, destination);
            direction.DisplayInfo();
            return direction;
        }

        static void SellTickets(TrainDirection direction)
        {
            Random random = new Random();
            int passengers = random.Next(1, 101);
            Console.WriteLine(Constants.TicketsSold, passengers);
        }

        static Train CreateTrain(TrainDirection direction)
        {
            Console.WriteLine(Constants.EnterTrainCapacity);
            int capacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(Constants.EnterCarriageCapacity);
            int carriageCapacity = Convert.ToInt32(Console.ReadLine());
            Train currentTrain = new Train(capacity, carriageCapacity);
            return currentTrain;
        }

        static void SendTrain(Train currentTrain, TrainDirection direction)
        {
            Console.WriteLine(Constants.TrainSent);
            currentTrain.DisplayInfo();
            Console.WriteLine(Constants.ProgramFinished);
        }
    }

    class TrainDirection
    {
        public string Departure { get; private set; }
        public string Destination { get; private set; }

        public TrainDirection(string departure, string destination)
        {
            Departure = departure;
            Destination = destination;
        }

        public void DisplayInfo()
        {
            Console.WriteLine(Constants.CurrentDirectionInfo, Departure, Destination);
        }
    }

    class Train
    {
        public int Capacity { get; set; }
        public int CarriageCapacity { get; set; }

        public Train(int capacity, int carriageCapacity)
        {
            Capacity = capacity;
            CarriageCapacity = carriageCapacity;
        }

        public void DisplayInfo()
        {
            Console.WriteLine(Constants.TrainInfo, Capacity, CarriageCapacity);
        }
    }

    class Constants
    {
        public const string CurrentDirectionInfo = "Текущее направление: {0} - {1}";
        public const string TrainInfo = "Информация о поезде: Вместимость: {0}, Вместимость вагона: {1}";
        public const string TicketsSold = "Продано билетов: {0}";
        public const string TrainSent = "Поезд отправлен!";
        public const string ProgramFinished = "Программа завершена.";
        public const string EnterDeparture = "Введите пункт отправления:";
        public const string EnterDestination = "Введите пункт назначения:";
        public const string EnterTrainCapacity = "Введите вместимость поезда:";
        public const string EnterCarriageCapacity = "Введите вместимость вагона:";
    }
}
