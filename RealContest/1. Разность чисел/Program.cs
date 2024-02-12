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
            return new List<string>() { Console.ReadLine() };
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

            var ab = ConvertStringToListOfInt32(input[0]);

            return $"{(BigInteger)ab[0] - (BigInteger)ab[1]}";
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            Console.WriteLine(Solve(input));
        }
    }
}