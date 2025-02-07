﻿using System;
using System.Collections.Generic;

namespace TestTask
{
    public class Program
    {
        private const string _pressKeyMessage = "To continue please press any key on keyboard";
        private const string _singleStatisticMessage = "Single statistic:";
        private const string _doubleStatisticMessage = "Double statistic:";

        /// <summary>
        /// Программа принимает на входе 2 пути до файлов.
        /// Анализирует в первом файле кол-во вхождений каждой буквы (регистрозависимо). Например А, б, Б, Г и т.д.
        /// Анализирует во втором файле кол-во вхождений парных букв (не регистрозависимо). Например АА, Оо, еЕ, тт и т.д.
        /// По окончанию работы - выводит данную статистику на экран.
        /// </summary>
        /// <param name="args">Первый параметр - путь до первого файла.
        /// Второй параметр - путь до второго файла.</param>
        static void Main(string[] args)
        {
            var statisticHandler = new StatisticHandler();

            IList<LetterStats> singleLetterStats = GetSingleLetterStats(args[0], statisticHandler);
            IList<LetterStats> doubleLetterStats = GetDoubleLetterStats(args[1], statisticHandler);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(_singleStatisticMessage);
            PrintStatistic(singleLetterStats);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_doubleStatisticMessage);
            PrintStatistic(doubleLetterStats);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(_pressKeyMessage);
            Console.ReadKey();
        }

        /// <summary>
        /// Получение статистики одинарных букв
        /// </summary>
        /// <param name="args"></param>
        /// <param name="statisticHandler"></param>
        /// <returns></returns>
        private static IList<LetterStats> GetSingleLetterStats(string path, StatisticHandler statisticHandler)
        {
            return statisticHandler.RemoveCharTypes(statisticHandler.GetLetterStatistic(path), CharType.Vowel);
        }

        /// <summary>
        /// Получение статистики удвоенных букв
        /// </summary>
        /// <param name="args"></param>
        /// <param name="statisticHandler"></param>
        /// <returns></returns>
        private static IList<LetterStats> GetDoubleLetterStats(string path, StatisticHandler statisticHandler)
        {
            return statisticHandler.RemoveCharTypes(statisticHandler.GetLetterStatistic(path, isDoubleLetterStats: true, ignoreCase: true), CharType.Consonants);
        }

        /// <summary>
        /// Ф-ция выводит на экран полученную статистику в формате "{Буква} : {Кол-во}"
        /// Каждая буква - с новой строки.
        /// Выводить на экран необходимо предварительно отсортировав набор по алфавиту.
        /// В конце отдельная строчка с ИТОГО, содержащая в себе общее кол-во найденных букв/пар
        /// </summary>
        /// <param name="letters">Коллекция со статистикой</param>
        private static void PrintStatistic(IEnumerable<LetterStats> letters)
        {
            foreach (var letter in letters)
            {
                Console.WriteLine($"{new string('-', 5)}\nLetter:{letter.Letter}\nCount:{letter.Count}");
            }
            Console.WriteLine();
        }
    }
}
