using System;
using System.Collections.Generic;

namespace TrainPlanner
{
    class Program
    {
        static void Main(string[] args)
        {
            TrainStation station = new TrainStation();
            station.Run();
        }
    }

    class TrainStation
    {
        private const string QuitKey = "q";
        private List<Train> _sentTrains = new List<Train>();

        public void Run()
        {
            bool isContinueProgram = true;

            while (isContinueProgram)
            {
                TrainDirection direction = GetDirectionFromUser();

                if (direction == null)
                {
                    continue;
                }

                int passengers = SellTickets();
                List<Carriage> carriages = CreateCarriages(passengers);

                if (carriages == null)
                {
                    continue;
                }

                Train currentTrain = new Train(direction, carriages);
                SendTrain(currentTrain);

                Console.WriteLine($"Нажмите любую клавишу для продолжения или '{QuitKey}' для выхода.");
                string userInput = Console.ReadLine();

                if (userInput.ToLower() == QuitKey)
                {
                    isContinueProgram = false;
                }
            }

            DisplaySentTrains();
        }

        private TrainDirection GetDirectionFromUser()
        {
            TrainDirection direction = null;

            while (direction == null)
            {
                Console.WriteLine("Введите пункт отправления: ");
                string departure = Console.ReadLine();
                Console.WriteLine("Введите пункт назначения: ");
                string destination = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(departure) || string.IsNullOrWhiteSpace(destination) || departure == destination)
                {
                    Console.WriteLine("Неверные пункты отправления или назначения. Пожалуйста, попробуйте еще раз.");
                }
                else
                {
                    direction = new TrainDirection(departure, destination);
                }
            }

            return direction;
        }

        private int SellTickets()
        {
            const int MinPassengers = 1;
            const int MaxPassengers = 101;

            Random random = new Random();
            int passengers = random.Next(MinPassengers, MaxPassengers);

            Console.WriteLine($"Продано билетов: {passengers}");
            return passengers;
        }

        private List<Carriage> CreateCarriages(int passengers)
        {
            Console.WriteLine("Введите вместимость вагона (или нажмите Enter значения по умолчанию): ");
            string input = Console.ReadLine();

            int carriageCapacity;

            if (string.IsNullOrWhiteSpace(input))
            {
                carriageCapacity = 33;
            }
            else if (!int.TryParse(input, out carriageCapacity) || carriageCapacity <= 0)
            {
                Console.WriteLine("Ошибка. Недопустимое значение. Будет использовано количество по умолчанию.");
                carriageCapacity = 33;
            }

            int totalCarriages = (int)Math.Ceiling((double)passengers / carriageCapacity);

            List<Carriage> carriages = new List<Carriage>();

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
            _sentTrains.Add(currentTrain);
        }

        private void DisplaySentTrains()
        {
            Console.WriteLine("Отправленные поезда: ");

            foreach (var train in _sentTrains)
            {
                train.DisplayInfo();
            }
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
            Console.WriteLine($"Информация о поезде: Вместимость: {_carriages.Count * _carriages[0].Capacity}, Вагоны: {_carriages.Count}");
            _direction.DisplayInfo();
        }
    }

    class Carriage
    {
        public Carriage(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; private set; }
    }
}
