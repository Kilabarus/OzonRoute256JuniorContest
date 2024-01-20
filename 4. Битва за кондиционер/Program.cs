using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            int numOfGroups = Convert.ToInt32(input[0]);
            for (int i = 0; i < numOfGroups; i++)
            {
                input.Add(Console.ReadLine());
                int numOfEntries = Convert.ToInt32(input[^1]);

                for (int j = 0; j < numOfEntries; j++)
                {
                    input.Add(Console.ReadLine());
                }
            }

            return input;
        }

        static List<List<string>> ConvertInput(List<string> input)
        {
            List<List<string>> entries = new();
            List<string> group = new();

            for (int i = 1; i < input.Count; i++)
            {
                if (!input[i].Contains("="))
                {
                    entries.Add(group);
                    group = new();

                    continue;
                }

                group.Add(input[i]);                
            }

            entries.Add(group);

            return entries;
        }

        static void Solve(List<List<string>> entries)
        {
            foreach (var group in entries)
            {
                int low = 15;
                int high = 30;

                int currentTemperature = low;

                foreach (var entry in group)
                {
                    if (entry.StartsWith("<"))
                    {
                        int newEmployeeHigh = Convert.ToInt32(entry.Split()[1]);
                        
                        if (newEmployeeHigh < high)
                        {
                            high = newEmployeeHigh;
                        }                        
                    }

                    if (entry.StartsWith('>'))
                    {
                        int newEmployeeLow = Convert.ToInt32(entry.Split()[1]);

                        if (newEmployeeLow > low)
                        {
                            low = newEmployeeLow;
                        }
                    }

                    currentTemperature = low <= high ? low : -1;
                    Console.WriteLine(currentTemperature);
                }

                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            List<List<string>> entries = ConvertInput(input);

            Solve(entries);
        }
    }
}