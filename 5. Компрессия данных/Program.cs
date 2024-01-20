using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestContest
{
    internal class Program
    {
        static List<List<int>> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<List<int>> input = new();            

            int numOfGroups = ReadSingleNumber();

            for (int i = 0; i < numOfGroups; i++)
            {
                Console.ReadLine();
                input.Add(Console.ReadLine().Split().Select(x => Convert.ToInt32(x)).ToList());
            }

            return input;
        }

        static void Solve(List<List<int>> entries)
        {
            foreach (var group in entries)
            {                                
                StringBuilder sB = new();

                int i = 0;

                while (i < group.Count)
                {
                    int start = group[i];
                    ++i;

                    int ascByOneCounter = 0;
                    while (i < group.Count && group[i] == group[i - 1] + 1) 
                    {
                        ascByOneCounter++;
                        i++;
                    }

                    if (ascByOneCounter > 0)
                    {
                        sB.Append(start);
                        sB.Append(' ');
                        sB.Append(ascByOneCounter);
                        sB.Append(' ');

                        continue;
                    }

                    int descByOneCounter = 0;
                    while (i < group.Count && group[i] == group[i - 1] - 1)
                    {
                        descByOneCounter++;
                        i++;
                    }

                    if (descByOneCounter > 0)
                    {
                        sB.Append(start);
                        sB.Append(' ');
                        sB.Append('-' + descByOneCounter.ToString());
                        sB.Append(' ');

                        continue;
                    }

                    if (ascByOneCounter == 0 && descByOneCounter == 0)
                    {
                        sB.Append(start);
                        sB.Append(' ');
                        sB.Append(0);
                        sB.Append(' ');                        
                    }
                }

                string answer = sB.ToString().Trim();
                Console.WriteLine(answer.Split(' ').Length);
                Console.WriteLine(answer);
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}