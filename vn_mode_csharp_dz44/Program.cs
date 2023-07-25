using System;

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
                SellTickets(direction);
                var currentTrain = CreateTrain(direction);
                SendTrain(currentTrain, direction);

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить, или '{0}' для выхода.", QuitKey);
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

        private void SellTickets(TrainDirection direction)
        {
            Random random = new Random();
            int passengers = random.Next(1, 101);
            Console.WriteLine("Продано билетов: {0}", passengers);
        }

        private Train CreateTrain(TrainDirection direction)
        {
            Console.WriteLine("Введите вместимость поезда:");
            int capacity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите вместимость вагона:");
            int carriageCapacity = Convert.ToInt32(Console.ReadLine());
            var currentTrain = new Train(capacity, carriageCapacity);
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
            Console.WriteLine("Текущее направление: {0} - {1}", Departure, Destination);
        }
    }

    class Train
    {
        private int Capacity;
        private int CarriageCapacity;

        public Train(int capacity, int carriageCapacity)
        {
            Capacity = capacity;
            CarriageCapacity = carriageCapacity;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Информация о поезде: Вместимость: {0}, Вместимость вагона: {1}", Capacity, CarriageCapacity);
        }
    }
}
