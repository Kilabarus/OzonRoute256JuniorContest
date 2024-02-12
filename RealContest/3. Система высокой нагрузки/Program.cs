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

            foreach (var str in input.Skip(1).Take(N))
            {
                if (str[0] != 'M')
                {
                    output.AppendLine("NO");
                    continue;
                }

                bool foundAnswer = false;
                bool isStarted = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (foundAnswer)
                    {
                        break;
                    }

                    if (i == 0 && str[i] != 'M')
                    {
                        output.AppendLine("NO");
                        foundAnswer = true;
                        break;
                    }

                    if (str[i] == 'M')
                    {
                        if (isStarted)
                        {
                            output.AppendLine("NO");
                            foundAnswer = true;
                            break;
                        }

                        isStarted = true;
                        continue;
                    }

                    if ((str[i] == 'R' || str[i] == 'C' || str[i] == 'D') && !isStarted)
                    {
                        output.AppendLine("NO");
                        foundAnswer = true;
                        break;
                    }

                    if (str[i] == 'R')
                    {
                        if (i + 1 == str.Length || str[i + 1] != 'C')
                        {
                            output.AppendLine("NO");
                            foundAnswer = true;
                            break;
                        }

                        ++i;
                    }

                    if (str[i] == 'C')
                    {
                        if (i + 1 == str.Length || str[i + 1] != 'M')
                        {
                            output.AppendLine("NO");
                            foundAnswer = true;
                            break;
                        }

                        ++i;
                    }

                    if (str[i] == 'D')
                    {
                        isStarted = false;
                    }
                }

                if (foundAnswer)
                {
                    continue;
                }

                if (isStarted)
                {
                    output.AppendLine("NO");
                    continue;
                }

                output.AppendLine("YES");
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