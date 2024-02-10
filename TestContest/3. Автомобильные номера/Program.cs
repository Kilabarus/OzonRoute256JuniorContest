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

            int numOfEntries = Convert.ToInt32(input[0]);
            for (int i = 0; i < numOfEntries; i++)
            {
                input.Add(Console.ReadLine());
            }

            return input;
        }

        static List<string> ConvertInput(List<string> input)
        {
            List<string> entries = new();
            
            for (int i = 1; i < input.Count; i++)
            {
                entries.Add(input[i]);
            }

            return entries;
        }

        static void Solve(List<string> entries)
        {
            bool IsDigit(char ch)
            {
                return ch >= '0' && ch <= '9';
            }            

            StringBuilder sB = new();
            StringBuilder currentLicensePlate = new();

            foreach (string entry in entries)
            {
                bool isCorrect = true;

                sB.Clear();
                currentLicensePlate.Clear();

                for (int i = 0; i < entry.Length && isCorrect; i++)
                {
                    switch (currentLicensePlate.Length)
                    {
                        case 0:
                            {
                                if (IsDigit(entry[i]))
                                {
                                    isCorrect = false;
                                    break;
                                }

                                currentLicensePlate.Append(entry[i]);
                                break;
                            }
                        case 1:
                            {
                                if (!IsDigit(entry[i]))
                                {
                                    isCorrect = false;
                                    break;
                                }

                                currentLicensePlate.Append(entry[i]);
                                break;
                            }
                        case 2:
                            {
                                currentLicensePlate.Append(entry[i]);
                                break;
                            }
                        case 3:
                            {
                                if (IsDigit(entry[i]))
                                {
                                    isCorrect = false;
                                    break;
                                }

                                currentLicensePlate.Append(entry[i]);

                                if (!IsDigit(entry[i - 1]))
                                {                                    
                                    sB.Append(currentLicensePlate.ToString());
                                    sB.Append(' ');
                                    currentLicensePlate.Clear();
                                    
                                    break;
                                }

                                break;
                            }
                        case 4:
                            {
                                if (IsDigit(entry[i]))
                                {
                                    isCorrect = false;
                                    break;
                                }

                                currentLicensePlate.Append(entry[i]);
                                sB.Append(currentLicensePlate.ToString());
                                sB.Append(' ');
                                currentLicensePlate.Clear();

                                break;
                            }

                    }
                }

                if (currentLicensePlate.Length > 0)
                {
                    isCorrect = false;
                }

                string answer = isCorrect ? sB.ToString() : "-";
                Console.WriteLine(answer);
            }
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            List<string> entries = ConvertInput(input);

            Solve(entries);
        }
    }
}