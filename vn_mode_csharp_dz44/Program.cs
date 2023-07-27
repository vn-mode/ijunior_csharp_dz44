using System;
using System.Collections.Generic;

namespace TrainPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var station = new TrainStation();
            station.Run();
        }
    }

    class TrainStation
    {
        private const string _quitKey = "q";
        private const int _minPassengers = 1;
        private const int _maxPassengers = 101;

        public void Run()
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                var direction = GetDirectionFromUser();

                if (direction == null)
                {
                    continue;
                }

                int passengers = SellTickets();
                var carriages = CreateCarriages(passengers);

                if (carriages == null)
                {
                    continue;
                }

                var currentTrain = new Train(direction, carriages);
                SendTrain(currentTrain);

                Console.WriteLine($"Нажмите любую клавишу, чтобы продолжить, или '{_quitKey}' для выхода.");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == _quitKey)
                {
                    continueProgram = false;
                }
            }
        }

        private TrainDirection GetDirectionFromUser()
        {
            Console.WriteLine("Введите пункт отправления:");
            string departure = Console.ReadLine();
            Console.WriteLine("Введите пункт назначения:");
            string destination = Console.ReadLine();

            if (departure == destination)
            {
                Console.WriteLine("Пункт отправления не может быть тем же самым, что и пункт назначения.");
                return null;
            }

            return new TrainDirection(departure, destination);
        }

        private int SellTickets()
        {
            Random random = new Random();
            int passengers = random.Next(_minPassengers, _maxPassengers);
            Console.WriteLine($"Продано билетов: {passengers}");
            return passengers;
        }

        private List<Carriage> CreateCarriages(int passengers)
        {
            Console.WriteLine("Введите вместимость вагона:");

            if (!int.TryParse(Console.ReadLine(), out int carriageCapacity) || carriageCapacity <= 0)
            {
                Console.WriteLine("Недопустимая вместимость вагона.");
                return null;
            }

            int totalCarriages = (int)Math.Ceiling((double)passengers / carriageCapacity);

            var carriages = new List<Carriage>();

            for (int i = 0; i < totalCarriages; i++)
            {
                carriages.Add(new Carriage(carriageCapacity));
            }

            return carriages;
        }

        private void SendTrain(Train currentTrain)
        {
            Console.WriteLine("Поезд отправлен!");
            currentTrain.DisplayInfo();
            Console.WriteLine("Программа завершена.");
        }
    }

    class TrainDirection
    {
        private string _departure;
        private string _destination;

        public TrainDirection(string departure, string destination)
        {
            _departure = departure;
            _destination = destination;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Текущее направление: {_departure} - {_destination}");
        }
    }

    class Train
    {
        private List<Carriage> _carriages;
        private TrainDirection _direction;

        public Train(TrainDirection direction, List<Carriage> carriages)
        {
            _direction = direction;
            _carriages = carriages;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Информация о поезде: Вместимость: {_carriages.Count * _carriages[0].Capacity}, Вагонов: {_carriages.Count}");
            _direction.DisplayInfo();
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
