using System;

namespace TrainPlanner
{
    class TrainDirection
    {
        public string Departure { get; set; }
        public string Destination { get; set; }

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

        public Train(int capacity)
        {
            Capacity = capacity;
        }

        public void AddCarriage(int carriageCapacity)
        {
            Console.WriteLine(Constants.CarriageAddedInfo, carriageCapacity);
        }

        public void DisplayInfo()
        {
            Console.WriteLine(Constants.TrainInfo, Capacity);
        }
    }

    class Constants
    {
        public const string CurrentDirectionInfo = "Текущее направление: {0} - {1}";
        public const string CarriageAddedInfo = "Добавлен вагон вместимостью: {0}";
        public const string TrainInfo = "Информация о поезде: Вместимость: {0}";
        public const string MainMenu = "Выберите действие:\n1. Создать направление\n2. Продать билеты\n3. Сформировать поезд\n4. Отправить поезд\n5. Выйти из программы";
        public const string InvalidChoice = "Неверный выбор. Попробуйте снова.";
        public const string EnterDeparture = "Введите пункт отправления:";
        public const string EnterDestination = "Введите пункт назначения:";
        public const string TicketsSold = "Продано билетов: {0}";
        public const string EnterTrainCapacity = "Введите вместимость поезда:";
        public const string EnterCarriageCapacity = "Введите вместимость вагона:";
        public const string TrainSent = "Поезд отправлен!";
        public const string ProgramFinished = "Программа завершена.";
        public const string InvalidCapacity = "Неверное значение вместимости. Попробуйте снова.";
    }

    class Program
    {
        static void Main(string[] args)
        {
            TrainDirection direction = null;
            Train currentTrain = null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine(Constants.MainMenu);
                Console.Write("Ваш выбор: ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine(Constants.InvalidChoice);
                    Console.WriteLine();
                    continue;
                }
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        CreateDirection(ref direction);
                        break;
                    case 2:
                        SellTickets(direction);
                        break;
                    case 3:
                        CreateTrain(ref currentTrain, direction);
                        break;
                    case 4:
                        SendTrain(currentTrain, ref direction, ref currentTrain);
                        break;
                    case 5:
                        ExitProgram();
                        return;
                    default:
                        Console.WriteLine(Constants.InvalidChoice);
                        Console.WriteLine();
                        break;
                }
            }
        }

        static void CreateDirection(ref TrainDirection direction)
        {
            Console.WriteLine(Constants.EnterDeparture);
            string departure = Console.ReadLine();
            Console.WriteLine(Constants.EnterDestination);
            string destination = Console.ReadLine();
            direction = new TrainDirection(departure, destination);
            direction.DisplayInfo();
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        static void SellTickets(TrainDirection direction)
        {
            Console.Clear();
            if (direction == null)
            {
                Console.WriteLine(Constants.InvalidChoice);
            }
            else
            {
                Random random = new Random();
                int passengers = random.Next(1, 101);
                Console.WriteLine(Constants.TicketsSold, passengers);
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        static void CreateTrain(ref Train currentTrain, TrainDirection direction)
        {
            Console.Clear();
            if (direction == null)
            {
                Console.WriteLine(Constants.InvalidChoice);
            }
            else
            {
                Console.WriteLine(Constants.EnterTrainCapacity);
                int capacity;
                if (!int.TryParse(Console.ReadLine(), out capacity) || capacity <= 0)
                {
                    Console.WriteLine(Constants.InvalidCapacity);
                    Console.WriteLine();
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                    return;
                }
                if (currentTrain == null)
                {
                    currentTrain = new Train(capacity);
                }
                Console.WriteLine(Constants.EnterCarriageCapacity);
                int carriageCapacity;
                if (!int.TryParse(Console.ReadLine(), out carriageCapacity) || carriageCapacity <= 0)
                {
                    Console.WriteLine(Constants.InvalidCapacity);
                    Console.WriteLine();
                    Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                    Console.ReadKey();
                    return;
                }
                currentTrain.AddCarriage(carriageCapacity);
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        static void SendTrain(Train currentTrain, ref TrainDirection direction, ref Train currentTrainRef)
        {
            Console.Clear();
            if (currentTrain == null)
            {
                Console.WriteLine(Constants.InvalidChoice);
            }
            else
            {
                Console.WriteLine(Constants.TrainSent);
                currentTrain.DisplayInfo();
                direction = null;
                currentTrainRef = null;
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }

        static void ExitProgram()
        {
            Console.Clear();
            Console.WriteLine(Constants.ProgramFinished);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }
    }
}
