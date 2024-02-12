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

            int T = ConvertStringToListOfInt32(input[0])[0];

            Dictionary<char, int> markToCount = new Dictionary<char, int>()
                    {
                        { '1', 0 }, { '2', 0 },{ '3', 0 },{ '4', 0 },{ '5', 0 }
                    };

            for (int inputI = 1; inputI < input.Count;)
            {
                var NM = ConvertStringToListOfInt32(input[inputI]);
                (int N, int M) = (NM[0], NM[1]);

                char[,] matrix = new char[N, M];

                for (int inputJ = inputI + 1; inputJ < inputI + 1 + N; inputJ++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        matrix[inputJ - (inputI + 1), k] = input[inputJ][k];
                    }
                }

                Dictionary<int, Dictionary<char, int>> lines = new();
                Dictionary<int, Dictionary<char, int>> columns = new();

                for (int i = 0; i < N; i++)
                {
                    lines.Add(i, new Dictionary<char, int>(markToCount));
                }

                for (int j = 0; j < M; j++)
                {
                    columns.Add(j, new Dictionary<char, int>(markToCount));
                }

                char worst = '5';
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        char mark = matrix[i, j];

                        if (mark < worst)
                        {
                            worst = mark;
                        }

                        ++lines[i][mark];
                        ++columns[j][mark];
                    }
                }

                int lineWithBiggestNumberOfWorst = FindWorstLineColumn(worst, lines);
                int columnWithBiggestNumberOfWorst = FindWorstLineColumn(worst, columns);

                // Сначала попробуем убрать линию
                for (int j = 0; j < M; j++)
                {
                    char mark = matrix[lineWithBiggestNumberOfWorst, j];

                    --columns[j][mark];
                }

                int columnWithBiggestNumberOfWorstAfterRemovingLine = FindWorstLineColumn(worst, columns);
                char worstAfterRemovingLineFirst = '5';
                for (int i = 0; i < N; i++)
                {
                    if (i == lineWithBiggestNumberOfWorst)
                    {
                        continue;
                    }

                    for (int j = 0; j < M; j++)
                    {
                        if (j == columnWithBiggestNumberOfWorstAfterRemovingLine)
                        {
                            continue;
                        }

                        if (matrix[i, j] < worstAfterRemovingLineFirst)
                        {
                            worstAfterRemovingLineFirst = matrix[i, j];
                        }
                    }
                }

                // Возвращаем все как было
                for (int j = 0; j < M; j++)
                {
                    char mark = matrix[lineWithBiggestNumberOfWorst, j];

                    ++columns[j][mark];
                }

                // Теперь попробуем сначала убрать колонку
                for (int i = 0; i < N; i++)
                {
                    char mark = matrix[i, columnWithBiggestNumberOfWorst];

                    --lines[i][mark];
                }

                int lineWithBiggestNumberOfWorstAfterRemovingColumn = FindWorstLineColumn(worst, lines);
                char worstAfterRemovingColumnFirst = '5';
                for (int i = 0; i < N; i++)
                {
                    if (i == lineWithBiggestNumberOfWorstAfterRemovingColumn)
                    {
                        continue;
                    }

                    for (int j = 0; j < M; j++)
                    {
                        if (j == columnWithBiggestNumberOfWorst)
                        {
                            continue;
                        }

                        if (matrix[i, j] < worstAfterRemovingColumnFirst)
                        {
                            worstAfterRemovingColumnFirst = matrix[i, j];
                        }
                    }
                }

                string answer = "";
                if (worstAfterRemovingLineFirst > worstAfterRemovingColumnFirst)
                {
                    answer = $"{lineWithBiggestNumberOfWorst + 1} {columnWithBiggestNumberOfWorstAfterRemovingLine + 1}";
                }
                else
                {
                    answer = $"{lineWithBiggestNumberOfWorstAfterRemovingColumn + 1} {columnWithBiggestNumberOfWorst + 1}";
                }

                output.AppendLine(answer);
                inputI += N + 1;
            }

            return output.ToString();
        }

        static public int FindWorstLineColumn(char worstMark, Dictionary<int, Dictionary<char, int>> lines)
        {
            int worstLineColumn = 0;
            foreach (var line in lines.Keys)
            {
                if (lines[line][worstMark] > lines[worstLineColumn][worstMark])
                {
                    worstLineColumn = line;
                }

                if (lines[line][worstMark] == lines[worstLineColumn][worstMark])
                {
                    if (worstMark == '5')
                    {
                        continue;
                    }

                    worstLineColumn = BetweenTwo((char)(worstMark + 1), line, worstLineColumn);
                }
            }

            return worstLineColumn;

            int BetweenTwo(char worstMark, int line1, int line2)
            {
                if (lines[line1][worstMark] > lines[line2][worstMark])
                {
                    return line1;
                }

                if (lines[line1][worstMark] == lines[line2][worstMark])
                {
                    if (worstMark == '5')
                    {
                        return line1;
                    }

                    return BetweenTwo((char)(worstMark + 1), line1, line2);
                }

                return line2;
            }
        }

        static void Main(string[] args)
        {
            List<string> input = GetInput();

            Console.WriteLine(Solve(input));
        }
    }
}