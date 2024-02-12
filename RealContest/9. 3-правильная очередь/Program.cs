using System.Numerics;
using System.Text;

// Не работает, единственная задача, которую не смог решить

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

            using var input = new StreamReader(Console.OpenStandardInput());

            List<string> inputL = new();

            int numOfGroups = Convert.ToInt32(input.ReadLine());

            for (int i = 0; i < numOfGroups; i++)
            {
                input.ReadLine();
                inputL.Add(input.ReadLine());
            }

            return inputL;
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

            foreach (var s in input)
            {
                Console.WriteLine(SolveC(s));
            }

            return output.ToString();
        }

        static bool Check(string input)
        {
            BigInteger xCount = 0;
            BigInteger yCount = 0;
            BigInteger zCount = 0;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'X')
                {
                    ++xCount;
                }

                if (input[i] == 'Y')
                {
                    ++yCount;
                }

                if (input[i] == 'Z')
                {
                    ++zCount;
                }
            }

            int xAvailableCount = 0;
            int xyCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'X')
                {
                    ++xAvailableCount;
                }

                if (input[i] == 'Y')
                {
                    if (xAvailableCount > 0)
                    {
                        --xAvailableCount;
                        ++xyCount;
                    }
                }
            }

            int xzCount = 0;
            xAvailableCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'X')
                {
                    ++xAvailableCount;
                }

                if (input[i] == 'Z')
                {
                    if (xAvailableCount > 0)
                    {
                        --xAvailableCount;
                        ++xzCount;
                    }
                }
            }

            int yAvailableCount = 0;
            int yzCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'Y')
                {
                    ++yAvailableCount;
                }

                if (input[i] == 'Z')
                {
                    if (yAvailableCount > 0)
                    {
                        --yAvailableCount;
                        ++yzCount;
                    }
                }
            }

            if (yCount + zCount - 2 * yzCount <= xCount && xCount <= yCount + zCount &&
                xCount + zCount - 2 * xzCount <= yCount && yCount <= xCount + zCount &&
                xCount + yCount - 2 * xyCount <= zCount && zCount <= xCount + yCount)
            {
                return true;
            }

            return false;
        }
        static string SolveC(string input)
        {
            if (input.Length % 2 == 1)
            {
                return "No";
            }

            if (Check(input))
            {
                int xyAvailable = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == 'X' || input[i] == 'Y')
                    {
                        ++xyAvailable;
                    }

                    if (input[i] == 'Z')
                    {
                        if (xyAvailable > 0)
                        {
                            --xyAvailable;
                            continue;
                        }

                        return "No";
                    }
                }

                int yzAvailable = 0;
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (input[i] == 'Y' || input[i] == 'Z')
                    {
                        ++yzAvailable;
                    }

                    if (input[i] == 'X')
                    {
                        if (yzAvailable > 0)
                        {
                            --yzAvailable;
                            continue;
                        }

                        return "No";
                    }
                }

                Dictionary<int, int> dLeft = new();
                Dictionary<int, int> dRight = new();
                for (int i = 0; i < input.Length; i++)
                {
                    dLeft.Add(i, -1);
                    dRight.Add(i, -1);
                }

                int xCountLeft = 0;
                int yCountLeft = 0;
                int zCountLeft = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] == 'X')
                    {
                        ++xCountLeft;
                    }

                    if (input[i] == 'Y')
                    {
                        dLeft[i] = xCountLeft;

                        ++yCountLeft;
                    }

                    if (input[i] == 'Z')
                    {
                        if (yCountLeft > 0)
                        {
                            --yCountLeft;

                            if (i + 1 < input.Length && !Check(input.Substring(i + 1)))
                            {
                                return "No";
                            }

                            continue;


                        }

                        if (i + 1 < input.Length && !Check(input.Substring(i + 1)))
                        {
                            return "No";
                        }

                        --xCountLeft;
                    }
                }

                int xCountRight = 0;
                int yCountRight = 0;
                int zCountRight = 0;
                for (int i = input.Length - 1; i >= 0; i--)
                {
                    if (input[i] == 'X')
                    {
                        if (yCountRight > 0)
                        {
                            --yCountRight;
                            if (i - 1 < input.Length && !Check(input.Substring(0, i - 1)))
                            {
                                return "No";
                            }

                            continue;
                        }
                        if (i - 1 < input.Length && !Check(input.Substring(0, i - 1)))
                        {
                            return "No";
                        }
                        --zCountRight;
                    }

                    if (input[i] == 'Y')
                    {
                        dRight[i] = zCountRight;

                        ++yCountRight;
                    }

                    if (input[i] == 'Z')
                    {
                        ++zCountRight;
                    }
                }

                for (int i = 0; i < input.Length; i++)
                {
                    if (dLeft[i] == 0 && dRight[i] == 0)
                    {
                        return "No";
                    }
                }

                return "Yes";
            }

            return "No";
        }

        static bool SolveSlow(string input)
        {
            char first = input[0];
            bool flag = false;
            if (first == 'X')
            {
                int searchFrom = 1;
                while (searchFrom < input.Length && input.IndexOf('Y', searchFrom) != -1)
                {
                    string altInput = input.Remove(input.IndexOf('Y', searchFrom), 1);
                    if (altInput.Length == 1 || SolveSlow(altInput.Substring(1)))
                    {
                        return true;
                    }
                    searchFrom = input.IndexOf('Y', searchFrom) + 1;
                }

                searchFrom = 1;
                while (searchFrom < input.Length && input.IndexOf('Z', searchFrom) != -1)
                {
                    string altInput = input.Remove(input.IndexOf('Z', searchFrom), 1);
                    if (altInput.Length == 1 || SolveSlow(altInput.Substring(1)))
                    {
                        return true;
                    }
                    searchFrom = input.IndexOf('Z', searchFrom) + 1;
                }

                return false;
            }

            if (first == 'Y')
            {
                int searchFrom = 1;
                while (searchFrom < input.Length && input.IndexOf('Z', searchFrom) != -1)
                {
                    string altInput = input.Remove(input.IndexOf('Z', searchFrom), 1);
                    if (altInput.Length == 1 || SolveSlow(altInput.Substring(1)))
                    {
                        return true;
                    }
                    searchFrom = input.IndexOf('Z', searchFrom) + 1;
                }

                return false;
            }

            if (first == 'Z')
            {
                return false;
            }

            return false;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "XYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        static void Main(string[] args)
        {
            //for (int i = 2; i < 1000; i += 2)
            //{                
            //    for (int n = 0; n < 1000000; n++)
            //    {
            //        string r = RandomString(i);

            //        string slowAnswer = SolveSlow(r) ? "Yes" : "No";

            //        string fastAnswer = SolveC(r);

            //        if (slowAnswer != fastAnswer)
            //        {
            //            Console.WriteLine(r);
            //            Console.WriteLine($"Slow: {slowAnswer}");
            //            Console.WriteLine($"Fast: {fastAnswer}");
            //            Console.WriteLine();

            //            return;                                                
            //        }
            //    }
            //}


            List<string> input = GetInput();

            Console.WriteLine(Solve(input));
        }
    }
}