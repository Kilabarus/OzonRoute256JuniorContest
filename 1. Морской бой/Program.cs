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
            int numOfEntries = Convert.ToInt32(input[0]);
            List<List<int>> entries = new();

            for (int i = 1; i < input.Count; i++)
            {
                entries.Add(input[i].Split(' ').Select(strWithNumber => Convert.ToInt32(strWithNumber)).ToList());
            }

            return entries;
        }

        static void Solve(List<List<int>> entries)
        {
            Dictionary<int, int> shipsLeft = new Dictionary<int, int>()
            {
                { 1, 4 },
                { 2, 3 },
                { 3, 2 },
                { 4, 1 },
            };

            foreach (var entry in entries)
            {                
                shipsLeft[1] = 4;
                shipsLeft[2] = 3;
                shipsLeft[3] = 2;
                shipsLeft[4] = 1;

                foreach (var ship in entry)
                {
                    --shipsLeft[ship];                    
                }

                bool isCorrect = true;
                foreach (var shipsLeftValue in shipsLeft.Values)
                {
                    if (shipsLeftValue != 0)
                    {
                        isCorrect = false;
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