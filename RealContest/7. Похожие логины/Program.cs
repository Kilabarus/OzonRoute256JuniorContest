using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealContest
{
    internal class Program
    {
        static List<string> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<string> input = new()
            {
                Console.ReadLine()
            };

            int numOfGroups = Convert.ToInt32(input[0]);

            for (int i = 0; i < numOfGroups; i++)
            {
                input.Add(Console.ReadLine());
            }

            input.Add(Console.ReadLine());

            int numOfGroups1 = Convert.ToInt32(input[^1]);

            for (int i = 0; i < numOfGroups1; i++)
            {
                input.Add(Console.ReadLine());
            }

            return input;
        }

        static string Solve(List<string> input)
        {
            List<int> ConvertStringToListOfInt32(string ints)
            {
                return ints.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                           .Select(int.Parse)
                           .ToList();
            }

            StringBuilder output = new StringBuilder();

            int N = ConvertStringToListOfInt32(input[0])[0];

            List<string> employees = input.Skip(1).Take(N).ToList();

            int M = ConvertStringToListOfInt32(input[1 + N])[0];

            List<string> newOnes = input.Skip(1 + N + 1).Take(M).ToList();

            bool foundSame;
            foreach (var newOne in newOnes)
            {
                foundSame = false;

                foreach (var emp in employees)
                {
                    if (foundSame)
                    {
                        break;
                    }

                    if (newOne.Length != emp.Length)
                    {
                        continue;
                    }

                    bool swapped = false;
                    bool same = true;
                    for (int i = 0; i < newOne.Length; i++)
                    {
                        if (newOne[i] != emp[i])
                        {
                            if (swapped)
                            {
                                same = false;
                                break;
                            }

                            if (i + 1 == newOne.Length)
                            {
                                same = false;
                                break;
                            }

                            if (newOne[i] != emp[i + 1] || newOne[i + 1] != emp[i])
                            {
                                same = false;
                                break;
                            }

                            swapped = true;
                            ++i;
                        }
                    }

                    if (same)
                    {
                        foundSame = true;
                        break;
                    }
                }

                if (foundSame)
                {
                    output.AppendLine("1");
                    continue;
                }

                output.AppendLine("0");
            }

            return output.ToString();
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            Console.WriteLine(Solve(input));
        }
    }
}