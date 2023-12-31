﻿using System;
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
                        aquarium.AddFish();
                        break;

                    case CommandDeleteFish:
                        aquarium.DeleteFish();
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

        public bool IsAlive => Age <= _maxAge;
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void GrowOld()
        {
            if (Age <= _maxAge)
            {
                Age += 1;
            }
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
                _fishes[i].GrowOld();

                ShowMessage($"№:{i} Имя: {_fishes[i].Name} Возвраст:{_fishes[i].Age} Жива ли рыбка:{_fishes[i].IsAlive}", ConsoleColor.Blue);
            }
        }

        public void AddFish()
        {
            ShowMessage("Ведите имя рыбки", ConsoleColor.Yellow);

            string name = Console.ReadLine();

            int age = GetNumber("Ведите возраст рыбки:");

            int maxAge = GetNumber("Ведите максимальный возраст рыбки:");

            if (TryCreateFish(name, age, maxAge) == true)
            {
                ShowMessage("Рыбка успешно добавлена в аквариум", ConsoleColor.Green);
            }
            else
            {
                ShowMessage("Ошибка ! \nДанные не прошли проверку", ConsoleColor.Red);
            }
        }

        public void DeleteFish()
        {
            int index = GetNumber("Индекс рыбки которую хотите достать из аквариума");

            if (IsIndexInList(index))
            {
                ShowMessage("Рыбка успешно убрана из аквариума", ConsoleColor.Red);

                _fishes.RemoveAt(index);
            }
            else
            {
                ShowMessage("К сожалению рыбки с таким индексом нет", ConsoleColor.Red);
            }
        }

        private bool TryCreateFish(string name, int age, int maxAge, int minAge = 1)
        {
            if (name != "" && age >= minAge && _fishes.Count <= _maxCountFishes)
            {
                _fishes.Add(new Fish(name, age, maxAge));

                return true;
            }

            return false;
        }

        private bool IsIndexInList(int index)
        {
            return index > 0 && index < _fishes.Count;
        }

        private int GetNumber(string title)
        {
            bool isNumber = false;
            string userInput;
            int number = 0;

            while (isNumber == false)
            {
                ShowMessage(title, ConsoleColor.Blue);
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out number))
                {
                    isNumber = true;
                }
                else
                {
                    ShowMessage("Не верный формат вода", ConsoleColor.Red);
                }
            }

            return number;
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
