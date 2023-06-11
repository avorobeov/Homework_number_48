using System;
using System.Collections.Generic;

namespace Homework_number_48
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddFish = "1";
            const string CommandDeleteFish = "2";
            const string CommandExit = "3";

            List<Fish> fishes = new List<Fish> { new Fish("Олив",1,10),
                                                new Fish("Аква", 1, 20),
                                                new Fish("Сега",4,30)};

            int maxCountFishes = 10;

            Aquarium aquarium = new Aquarium(fishes, maxCountFishes);

            string userInput;
            bool isExit = false;

            while (isExit == false)
            {
                Console.WriteLine($"\nДля добавления рыбок нажмите {CommandAddFish}\n" +
                                  $"\nДля удаления рыбок нажмите {CommandDeleteFish}\n" +
                                  $"\nДля выхода нажмите {CommandExit}\n");

                aquarium.TimeSkip();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandAddFish:
                        aquarium.TryCreateFish();
                        break;

                    case CommandDeleteFish:
                        aquarium.TryDeleteFish();
                        break;

                    case CommandExit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Fish
    {
        private int _maxAge;

        public Fish(string name, int age, int maxAge)
        {
            Name = name;
            Age = age;
            _maxAge = maxAge;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        
        public void AddAge()
        {
            if (Age <= _maxAge)
            {
                Age += 1;
            }
        }

        public bool GetLives()
        {
            return Age <= _maxAge;
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;
       
        private int _maxCountFishes;

        public Aquarium(List<Fish> fishes, int maxCountFish)
        {
            _fishes = fishes;
            _maxCountFishes = maxCountFish;
        }

        public void TimeSkip()
        {
            for (int i = 0; i < _fishes.Count; i++)
            {
                _fishes[i].AddAge();

                ShowMessage($"Имя: {_fishes[i].Name} Возвраст:{_fishes[i].Age} Жива ли рыбка:{_fishes[i].GetLives()}", ConsoleColor.Blue);
            }
        }

        public void TryCreateFish()
        {
            int minAge = 1;

            ShowMessage("Ведите имя рыбки", ConsoleColor.Yellow);

            string name = Console.ReadLine();

            int age = GetNumber("Ведите возраст рыбки:");

            int maxAge = GetNumber("Ведите максимальный возраст рыбки:");

            if (name != "" && age >= minAge && _fishes.Count <= _maxCountFishes)
            {
                _fishes.Add(new Fish(name, age, maxAge));

                ShowMessage("Рыбка успешно добавлена в аквариум", ConsoleColor.Green);
            }
            else
            {
                ShowMessage("Ошибка ! \nДанные не прошли проверку", ConsoleColor.Red);
            }

        }

        public void TryDeleteFish()
        {
            ShowMessage("Имя рыбки которую хотите достать из аквариума", ConsoleColor.Red);

            string userInput = Console.ReadLine();

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (userInput == _fishes[i].Name)
                {
                    _fishes.RemoveAt(i);

                    ShowMessage("Рыбка успешно убрана из аквариума", ConsoleColor.Red);

                    break;
                }
            }
        }

        private int GetNumber(string text)
        {
            string inputUser;
            int meaning = 0;
            bool isCorrect = false;

            while (isCorrect == false)
            {
                ShowMessage(text, ConsoleColor.Green);

                inputUser = Console.ReadLine();

                if (Int32.TryParse(inputUser, out meaning))
                {
                    return meaning;
                }
                else
                {
                    ShowMessage("Вы вели вместо числа строку", ConsoleColor.Red);
                }
            }

            return meaning;
        }

        private void ShowMessage(string message, ConsoleColor color)
        {
            ConsoleColor preliminaryColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message + "\n");

            Console.ForegroundColor = preliminaryColor;
        }
    }
}
