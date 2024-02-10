using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestContest
{
    internal class Program
    {
        static List<(int, string)> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<(int, string)> input = new();

            int numOfGroups = ReadSingleNumber();

            for (int i = 0; i < numOfGroups; i++)
            {
                int numOfPages = ReadSingleNumber();
                string printedPages = Console.ReadLine();

                input.Add((numOfPages, printedPages));
            }

            return input;
        }

        static void Solve(List<(int NumOfPages, string PrintedPages)> entries)
        {
            foreach (var entry in entries)
            {
                BitArray isPagePrinted = new(entry.NumOfPages + 1);

                string[] printedPagesArr = entry.PrintedPages.Split(',');

                foreach (var printedPages in printedPagesArr)
                {                    
                    if (printedPages.Contains('-'))                         
                    {
                        string[] lh = printedPages.Split('-');
                        int l = Convert.ToInt32(lh[0]);
                        int h = Convert.ToInt32(lh[1]);

                        for (int i = l; i <= h; i++)
                        {
                            isPagePrinted[i] = true;
                        }

                        continue;
                    }

                    isPagePrinted[Convert.ToInt32(printedPages)] = true;
                }
                
                StringBuilder sB = new();

                int page = 1;

                while (page < isPagePrinted.Length)
                {
                    if (isPagePrinted[page])
                    {
                        ++page;
                        continue;
                    }

                    int start = page;                    
                    while (page < isPagePrinted.Length && !isPagePrinted[page])
                    {
                        ++page;
                    }

                    if (sB.Length > 0)
                    {
                        sB.Append(',');
                    }

                    if (page == start + 1)
                    {
                        sB.Append(start);
                        continue;
                    }

                    sB.Append(start);
                    sB.Append('-');
                    sB.Append(page - 1);
                }

                Console.WriteLine(sB.ToString());
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}