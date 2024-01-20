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
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<string> input = new();

            int numOfEntries = ReadSingleNumber();

            for (int i = 0; i < numOfEntries; i++)
            {
                input.Add(Console.ReadLine());                
            }

            return input;
        }

        static void Solve(List<string> entries)
        {
            bool IsLowerCaseOrDigit(char ch)
            {
                return (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9');
            }

            foreach (var entry in entries)
            {
                List<List<char>> strings = new()
                {
                    new()
                };
                int currString = 0;
                int currPosition = 0;

                foreach (var symbol in entry)
                {
                    if (IsLowerCaseOrDigit(symbol))
                    {
                        strings[currString].Insert(currPosition, symbol);
                        currPosition++;
                        
                        continue;
                    }

                    if (symbol == 'L')
                    {
                        currPosition = currPosition == 0 ? 0 : currPosition - 1;

                        continue;
                    }

                    if (symbol == 'R')
                    {
                        currPosition = currPosition == strings[currString].Count 
                            ? strings[currString].Count 
                            : currPosition + 1;

                        continue;
                    }

                    if (symbol == 'U')
                    {
                        if (currString == 0)
                        {
                            continue;
                        }

                        currString = currString - 1;                        

                        if (strings[currString].Count < currPosition)
                        {
                            currPosition = strings[currString].Count;
                        }

                        continue;
                    }

                    if (symbol == 'D')
                    {
                        if (currString == strings.Count - 1)
                        {
                            continue;
                        }

                        currString = currString + 1;

                        if (strings[currString].Count < currPosition)
                        {
                            currPosition = strings[currString].Count;
                        }

                        continue;
                    }

                    if (symbol == 'B')
                    {
                        currPosition = 0;
                        continue;
                    }

                    if (symbol == 'E')
                    {
                        currPosition = strings[currString].Count;
                        continue;
                    }

                    if (symbol == 'N')
                    {
                        string t = new string(strings[currString].ToArray());
                        string firstPart = t.Substring(0, currPosition);
                        string secondPart = t.Substring(currPosition);

                        strings[currString] = firstPart.ToList();
                        ++currString;
                        strings.Insert(currString, secondPart.ToList());
                        currPosition = 0;
                    }
                }

                foreach (var str in strings)
                {
                    Console.WriteLine(new string(str.ToArray()));
                }
                Console.WriteLine('-');
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}