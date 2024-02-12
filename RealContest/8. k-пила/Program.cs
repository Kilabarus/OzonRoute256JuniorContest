using System.Reflection;
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

            };

            int numOfGroups = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < numOfGroups; i++)
            {
                Console.ReadLine();
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

            foreach (var line in input)
            {
                var a = ConvertStringToListOfInt32(line);

                char[] bracketsCharArray = new char[a.Count - 1];

                for (int i = 1; i < a.Count; i++)
                {
                    if (a[i - 1] < a[i])
                    {
                        bracketsCharArray[i - 1] = '<';
                        continue;
                    }

                    if (a[i - 1] > a[i])
                    {
                        bracketsCharArray[i - 1] = '>';
                        continue;
                    }

                    bracketsCharArray[i - 1] = '=';
                }

                string brackets = new string(bracketsCharArray);

                var kc = SolveB(brackets);

                for (int k = 1; k <= a.Count; k++)
                {
                    output.Append($"{kc[k]} ");
                }
                output.AppendLine();
                continue;
            }

            return output.ToString();
        }

        static Dictionary<int, int> SolveB(string brackets)
        {
            int currentlyOpenedBrackets = 0;

            Dictionary<int, int> kcCurr = new();
            Dictionary<int, int> kcMax = new();

            for (int K = 0; K <= brackets.Length + 1; K++)
            {
                kcCurr[K] = 0;
                kcMax[K] = 0;
            }

            int i = 0;
            int k = 0;
            while (i < brackets.Length)
            {
                if (brackets[i] == '<')
                {
                    ++currentlyOpenedBrackets;
                    ++i;

                    continue;
                }

                if (brackets[i] == '>')
                {
                    if (currentlyOpenedBrackets == 0)
                    {
                        ++i;
                        continue;
                    }

                    // Закрываем столько скобок, сколько возможно
                    while (i < brackets.Length && currentlyOpenedBrackets > 0 && brackets[i] == '>')
                    {
                        ++k;
                        --currentlyOpenedBrackets;

                        if (kcMax[k] == 0)
                        {
                            kcMax[k] = 1;
                        }

                        ++i;
                    }

                    if (i >= brackets.Length)
                    {
                        break;
                    }

                    // Считаем, сколько таких же скобок идут сразу после этой подряд
                    if (brackets[i] == '<')
                    {
                        kcCurr[k] = 1;

                        string currBracket = new string('<', k) + new string('>', k);

                        while (i + 2 * k <= brackets.Length)
                        {
                            string next2kSymbols = brackets.Substring(i, 2 * k);

                            if (currBracket == next2kSymbols)
                            {
                                kcCurr[k]++;

                                if (kcCurr[k] > kcMax[k])
                                {
                                    kcMax[k] = kcCurr[k];
                                }

                                i += 2 * k;

                                continue;
                            }

                            break;
                        }

                        currentlyOpenedBrackets = 0;
                    }

                    k = 0;
                    continue;
                }

                if (brackets[i] == '=')
                {
                    currentlyOpenedBrackets = 0;
                    ++i;
                }
            }

            return kcMax;
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            Console.WriteLine(Solve(input));
        }
    }
}