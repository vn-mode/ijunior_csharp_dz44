using System;
using System.Collections.Generic;

namespace TrainPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var station = new TrainStation();
            station.Start();
        }
    }

    class TrainStation
    {
        private const string QuitKey = "q";

        public void Start()
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                var direction = CreateDirection();
                int passengers = SellTickets(direction);
                var currentTrain = CreateTrain(passengers);
                SendTrain(currentTrain, direction);

                Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить, или '{QuitKey}' для выхода.");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == QuitKey)
                {
                    continueProgram = false;
                }
            }
        }

        private TrainDirection CreateDirection()
        {
            Console.WriteLine("Введите пункт отправления:");
            string departure = Console.ReadLine();
            Console.WriteLine("Введите пункт назначения:");
            string destination = Console.ReadLine();
            var direction = new TrainDirection(departure, destination);
            direction.DisplayInfo();
            return direction;
        }

        private int SellTickets(TrainDirection direction)
        {
            Random random = new Random();
            int passengers = random.Next(1, 101);
            Console.WriteLine($"Продано билетов: {passengers}");
            return passengers;
        }

        private Train CreateTrain(int passengers)
        {
            Console.WriteLine("Введите вместимость вагона:");
            int carriageCapacity = Convert.ToInt32(Console.ReadLine());
            var currentTrain = new Train(passengers, carriageCapacity);
            return currentTrain;
        }

        private void SendTrain(Train currentTrain, TrainDirection direction)
        {
            Console.WriteLine("Поезд отправлен!");
            currentTrain.DisplayInfo();
            Console.WriteLine("Программа завершена.");
        }
    }

    class TrainDirection
    {
        private string Departure;
        private string Destination;

        public TrainDirection(string departure, string destination)
        {
            Departure = departure;
            Destination = destination;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Текущее направление: {Departure} - {Destination}");
        }
    }

    class Train
    {
        private List<Carriage> Carriages = new List<Carriage>();

        public Train(int passengers, int carriageCapacity)
        {
            int totalCarriages = (int)Math.Ceiling((double)passengers / carriageCapacity);

            for (int i = 0; i < totalCarriages; i++)
            {
                Carriages.Add(new Carriage(carriageCapacity));
            }
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Информация о поезде: Вместимость: {Carriages.Count * Carriages[0].Capacity}, Вагонов: {Carriages.Count}");
        }
    }

    class Carriage
    {
        public int Capacity { get; private set; }

        public Carriage(int capacity)
        {
            Capacity = capacity;
        }
    }
}
