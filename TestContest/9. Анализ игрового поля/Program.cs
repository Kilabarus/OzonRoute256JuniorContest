using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestContest
{
    internal class Program
    {
        static List<char[,]> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<char[,]> input = new();

            int numOfGroups = ReadSingleNumber();
            for (int i = 0; i < numOfGroups; i++)
            {
                string nm = Console.ReadLine();
                int n = Convert.ToInt32(nm.Split()[0]);
                int m = Convert.ToInt32(nm.Split()[1]);

                char[,] matrix = new char[n,m];
                for (int j = 0; j < n; j++)
                {                    
                    string s = Console.ReadLine();
                    for (int k = 0; k < m; k++)
                    {
                        matrix[j,k] = s[k];
                    }
                }

                input.Add(matrix);
            }

            return input;
        }        

        struct Point
        {
            public int Row;
            public int Column;

            public Point(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }

        static void Solve(List<char[,]> matrices)
        {
            void Solve(char[,] matrix, Point borderLeftUpPoint, Point borderRightDownPoint, int nestingLevel, List<int> nestingLevels)
            {
                for (int i = borderLeftUpPoint.Row; i <= borderRightDownPoint.Row; i++)
                {
                    for (int j = borderLeftUpPoint.Column; j <= borderRightDownPoint.Column; j++)
                    {
                        if (matrix[i, j] == '#')
                        {
                            continue;
                        }                        

                        if (matrix[i, j] == '.')
                        {
                            matrix[i, j] = '#';
                            continue;
                        }

                        nestingLevels.Add(nestingLevel);

                        Point newBorderLeftUpPoint = new Point(i, j);
                        
                        int newBorderRightDownPointColumn = j;
                        int newBorderRightDownPointRow = i;
                        
                        while (newBorderRightDownPointRow < matrix.GetLength(0))
                        {
                            if (matrix[newBorderRightDownPointRow, newBorderRightDownPointColumn] == '.')
                            {
                                newBorderRightDownPointRow--;
                                break;
                            }

                            newBorderRightDownPointRow++;
                        }

                        if (newBorderRightDownPointRow == matrix.GetLength(0))
                        {
                            newBorderRightDownPointRow--;
                        }

                        while (newBorderRightDownPointColumn < matrix.GetLength(1))
                        {
                            if (matrix[newBorderRightDownPointRow, newBorderRightDownPointColumn] == '.')
                            {
                                newBorderRightDownPointColumn--;
                                break;
                            }

                            newBorderRightDownPointColumn++;
                        }

                        if (newBorderRightDownPointColumn == matrix.GetLength(1))
                        {
                            newBorderRightDownPointColumn--;
                        }

                        Point newBorderRightDownPoint = new Point(newBorderRightDownPointRow, newBorderRightDownPointColumn);

                        for (int ii = newBorderLeftUpPoint.Row; ii <= newBorderRightDownPoint.Row; ii++)
                        {
                            matrix[ii, newBorderLeftUpPoint.Column] = '#';
                            matrix[ii, newBorderRightDownPoint.Column] = '#';
                        }

                        for (int jj = newBorderLeftUpPoint.Column; jj <= newBorderRightDownPoint.Column; jj++)
                        {
                            matrix[newBorderLeftUpPoint.Row, jj] = '#';
                            matrix[newBorderRightDownPoint.Row, jj] = '#';
                        }

                        Solve(matrix, newBorderLeftUpPoint, newBorderRightDownPoint, nestingLevel + 1, nestingLevels);
                    }
                }

            }

            foreach (var matrix in matrices)
            {
                List<int> nestingLevels = new();

                Solve(matrix, new Point(0, 0), new Point(matrix.GetLength(0) - 1, matrix.GetLength(1) - 1), 0, nestingLevels);

                nestingLevels.Sort();

                StringBuilder sB = new();

                foreach (var nestingLevel in nestingLevels)
                {
                    sB.Append(nestingLevel);
                    sB.Append(' ');
                }

                Console.WriteLine(sB.ToString().Trim());
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}