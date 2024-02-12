using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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

            List<string> input = new();

            int numOfGroups = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numOfGroups; i++)
            {
                int numOfLines = Convert.ToInt32(Console.ReadLine());
                StringBuilder sb = new();
                for (int j = 0; j < numOfLines; j++)
                {
                    sb.Append(Console.ReadLine());
                }
                input.Add(sb.ToString());
            }

            return input;
        }

        public class Dir
        {
            [JsonPropertyName("dir")]
            public string dir;
            [JsonPropertyName("files")]
            public List<string> files;
            [JsonPropertyName("folders")]
            public List<Dir> folders;
        }

        static string Solve(List<string> input)
        {
            BigInteger GetNumberOfHackedFiles(Dir dir, bool isInitiallyHacked)
            {
                BigInteger sum = 0;

                if (dir.files is null)
                {
                    if (dir.folders is null)
                    {
                        return 0;
                    }

                    foreach (var folder in dir.folders)
                    {
                        sum += GetNumberOfHackedFiles(folder, isInitiallyHacked);
                    }

                    return sum;
                }

                isInitiallyHacked = isInitiallyHacked || dir.files.Count(x => x.EndsWith(".hack")) > 0;

                if (isInitiallyHacked)
                {
                    if (dir.folders is null)
                    {
                        return dir.files.Count;
                    }

                    foreach (var folder in dir.folders)
                    {
                        sum += GetNumberOfHackedFiles(folder, true);
                    }

                    return dir.files.Count + sum;
                }

                if (dir.folders is null)
                {
                    return 0;
                }

                foreach (var folder in dir.folders)
                {
                    sum += GetNumberOfHackedFiles(folder, false);
                }

                return sum;
            }

            StringBuilder output = new StringBuilder();

            foreach (var json in input)
            {
                Dir dir = JsonSerializer.Deserialize<Dir>(json, new JsonSerializerOptions() { MaxDepth = 1000000, IncludeFields = true });

                output.AppendLine($"{GetNumberOfHackedFiles(dir, false)}");
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