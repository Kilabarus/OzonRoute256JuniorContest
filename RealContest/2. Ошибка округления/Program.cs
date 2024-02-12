using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                int numOfLines = Convert.ToInt32(input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries)[0]);

                for (int j = 0; j < numOfLines; j++)
                {
                    input.Add(Console.ReadLine());
                }
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

            int numOfGroups = Convert.ToInt32(input[0]);

            for (int i = 1; i < input.Count;)
            {
                var NP = ConvertStringToListOfInt32(input[i]);
                (int N, int P) = (NP[0], NP[1]);
                BigInteger sum = 0;

                for (int j = i + 1; j < i + 1 + N; j++)
                {
                    int price = ConvertStringToListOfInt32(input[j])[0];

                    BigInteger withP = (BigInteger)price * P;
                    BigInteger kopeiki = withP % 100;

                    sum += kopeiki;
                }

                string kopeikiStr = sum % 100 < 10 ? $"0{sum % 100}" : $"{sum % 100}";

                output.AppendLine($"{sum / 100}.{kopeikiStr}");
                i += N + 1;
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