using System;
using System.Collections.Generic;
using System.Linq;

namespace TestContest
{
    internal class Program
    {
        static List<string> GetInput()
        {
            List<string> input = new List<string>
            {
                Console.ReadLine()
            };

            int numOfEntries = Convert.ToInt32(input[0]);
            for (int i = 0; i < numOfEntries; i++)
            {
                input.Add(Console.ReadLine());
            }

            return input;
        }

        static List<List<int>> ConvertInput(List<string> input)
        {
            List<List<int>> entries = new();

            for (int i = 1; i < input.Count; i++)
            {
                entries.Add(input[i].Split(' ').Select(strWithNumber => Convert.ToInt32(strWithNumber)).ToList());
            }

            return entries;
        }

        static void Solve(List<List<int>> entries)
        {
            bool IsLeapYear(int year)
            {
                return year % 400 == 0 || (year % 4 == 0 && year % 100 != 0);
            }

            foreach (var entry in entries)
            {
                int day = entry[0];
                int month = entry[1];
                int year = entry[2];

                bool isCorrect;

                switch (month) 
                {                    
                    case 4:
                    case 6:                    
                    case 9:
                    case 11:
                        {
                            isCorrect = day <= 30;
                            break;
                        }
                    case 2:
                        {
                            isCorrect = day <= 28 || (day == 29 && IsLeapYear(year));
                            break;
                        }
                    default:
                        {
                            isCorrect = true;
                            break; 
                        }                        
                }

                string answer = isCorrect ? "YES" : "NO";
                Console.WriteLine(answer);
            }
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            List<List<int>> entries = ConvertInput(input);

            Solve(entries);
        }
    }
}