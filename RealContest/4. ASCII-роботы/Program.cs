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

        enum Direction
        {
            Left, Right, Up, Down
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

            for (int inputI = 1; inputI < input.Count;)
            {
                var NM = ConvertStringToListOfInt32(input[inputI]);
                (int N, int M) = (NM[0], NM[1]);

                char[,] m = new char[N, M];

                int AColumn = 0;
                int ALine = 0;
                int BColumn = 0;
                int BLine = 0;

                for (int inputJ = inputI + 1; inputJ < inputI + 1 + N; inputJ++)
                {
                    for (int k = 0; k < M; k++)
                    {
                        char c = input[inputJ][k];

                        int line = inputJ - (inputI + 1);
                        int column = k;

                        if (c == 'A')
                        {
                            ALine = line;
                            AColumn = column;
                        }

                        if (c == 'B')
                        {
                            BLine = line;
                            BColumn = column;
                        }

                        m[line, column] = c;
                    }
                }

                void Drive(Direction direction, int startLine, int startColumn, char pathMark)
                {
                    switch (direction)
                    {
                        case Direction.Left:
                            while (startColumn > 0)
                            {
                                m[startLine, --startColumn] = pathMark;
                            }
                            break;
                        case Direction.Right:
                            while (startColumn < M - 1)
                            {
                                m[startLine, ++startColumn] = pathMark;
                            }
                            break;
                        case Direction.Up:
                            while (startLine > 0)
                            {
                                m[--startLine, startColumn] = pathMark;
                            }
                            break;
                        case Direction.Down:
                            while (startLine < N - 1)
                            {
                                m[++startLine, startColumn] = pathMark;
                            }
                            break;
                    }
                }

                if (AColumn < BColumn || ALine < BLine)
                {
                    // Ведем А в 0 0
                    if (ALine % 2 != 0)
                    {
                        // Слева А стойка
                        // Едем вверх до конца и налево до 0 0                        
                        Drive(Direction.Up, ALine, AColumn, 'a');
                        Drive(Direction.Left, 0, AColumn, 'a');
                    }
                    else
                    {
                        // Слева А ничего нет
                        // Едем влево до конца и наверх до 0 0                        
                        Drive(Direction.Left, ALine, AColumn, 'a');
                        Drive(Direction.Up, ALine, 0, 'a');
                    }

                    // Ведем В в N M
                    if (BLine % 2 != 0)
                    {
                        // Справа В стойка
                        // Едем вниз до конца и направо до N M               
                        Drive(Direction.Down, BLine, BColumn, 'b');
                        Drive(Direction.Right, N - 1, BColumn, 'b');
                    }
                    else
                    {
                        // Справа В ничего нет
                        // Едем вправо до конца и вниз до N M
                        Drive(Direction.Right, BLine, BColumn, 'b');
                        Drive(Direction.Down, BLine, M - 1, 'b');
                    }
                }
                else
                {
                    // Ведем В в 0 0
                    if (BLine % 2 != 0)
                    {
                        // Слева А стойка
                        // Едем вверх до конца и налево до 0 0                        
                        Drive(Direction.Up, BLine, BColumn, 'b');
                        Drive(Direction.Left, 0, BColumn, 'b');
                    }
                    else
                    {
                        // Слева А ничего нет
                        // Едем влево до конца и наверх до 0 0                        
                        Drive(Direction.Left, BLine, BColumn, 'b');
                        Drive(Direction.Up, BLine, 0, 'b');
                    }

                    if (ALine % 2 != 0)
                    {
                        // Справа В стойка
                        // Едем вниз до конца и направо до N M               
                        Drive(Direction.Down, ALine, AColumn, 'a');
                        Drive(Direction.Right, N - 1, AColumn, 'a');
                    }
                    else
                    {
                        // Справа В ничего нет
                        // Едем вправо до конца и вниз до N M
                        Drive(Direction.Right, ALine, AColumn, 'a');
                        Drive(Direction.Down, ALine, M - 1, 'a');
                    }
                }

                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < M; j++)
                    {
                        output.Append(m[i, j]);
                    }
                    output.AppendLine();
                }

                inputI += N + 1;
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