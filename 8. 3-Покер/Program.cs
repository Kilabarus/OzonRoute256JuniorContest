using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestContest
{
    internal class Program
    {
        static List<List<(string, string)>> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<List<(string, string)>> input = new();

            int numOfGroups = ReadSingleNumber();

            for (int i = 0; i < numOfGroups; i++)
            {
                int numOfPlayers = ReadSingleNumber();
                List<(string, string)> list = new();

                for (int j = 0; j < numOfPlayers; j++)
                {
                    string[] cards = Console.ReadLine().Split();
                    list.Add((cards[0], cards[1]));
                }

                input.Add(list);
            }

            return input;
        }

        static void Solve(List<List<(string CardOne, string CardTwo)>> entries)
        {
            int GetCardValue(string card)
            {
                if (card[0] >= '2' && card[0] <= '9')
                {
                    return card[0] - '1';
                }

                int baseValue = '9' - '1';

                switch (card[0])
                {
                    case 'T':
                        return baseValue + 1;
                    case 'J':
                        return baseValue + 2;
                    case 'Q':
                        return baseValue + 3;
                    case 'K':
                        return baseValue + 4;
                    default:
                        return baseValue + 5;
                }
            }

            int GetCombinationValue(string cardOne, string cardTwo, string cardThree)
            {
                if (cardOne[0] == cardTwo[0] && cardTwo[0] == cardThree[0])
                {
                    return GetCardValue(cardOne) * 10000;
                }

                if (cardOne[0] == cardTwo[0] || cardOne[0] == cardThree[0])
                {
                    return GetCardValue(cardOne) * 100;
                }

                if (cardTwo[0] == cardThree[0])
                {
                    return GetCardValue(cardTwo) * 100;
                }

                return Math.Max(Math.Max(GetCardValue(cardOne), GetCardValue(cardTwo)), GetCardValue(cardThree));

            }

            foreach (var entry in entries)
            {
                Dictionary<string, bool> cardsOut = new Dictionary<string, bool>()
                {
                    { "2S", false },
                    { "2C", false },
                    { "2D", false },
                    { "2H", false },

                    { "3S", false },
                    { "3C", false },
                    { "3D", false },
                    { "3H", false },

                    { "4S", false },
                    { "4C", false },
                    { "4D", false },
                    { "4H", false },

                    { "5S", false },
                    { "5C", false },
                    { "5D", false },
                    { "5H", false },

                    { "6S", false },
                    { "6C", false },
                    { "6D", false },
                    { "6H", false },

                    { "7S", false },
                    { "7C", false },
                    { "7D", false },
                    { "7H", false },

                    { "8S", false },
                    { "8C", false },
                    { "8D", false },
                    { "8H", false },

                    { "9S", false },
                    { "9C", false },
                    { "9D", false },
                    { "9H", false },

                    { "TS", false },
                    { "TC", false },
                    { "TD", false },
                    { "TH", false },

                    { "JS", false },
                    { "JC", false },
                    { "JD", false },
                    { "JH", false },

                    { "QS", false },
                    { "QC", false },
                    { "QD", false },
                    { "QH", false },

                    { "KS", false },
                    { "KC", false },
                    { "KD", false },
                    { "KH", false },

                    { "AS", false },
                    { "AC", false },
                    { "AD", false },
                    { "AH", false },
                };

                foreach (var playersCards in entry)
                {
                    cardsOut[playersCards.CardOne] = true;
                    cardsOut[playersCards.CardTwo] = true;
                }

                List<string> neededCards = new List<string>();                

                foreach (string card in cardsOut.Keys)
                {
                    if (cardsOut[card])
                    {
                        continue;
                    }

                    List<int> combinationValues = new List<int>();

                    foreach (var playersCards in entry)
                    {
                        combinationValues.Add(GetCombinationValue(playersCards.CardOne, playersCards.CardTwo, card));
                    }

                    if (combinationValues[0] == combinationValues.Max())
                    {
                        neededCards.Add(card);
                    }
                }

                Console.WriteLine(neededCards.Count);
                foreach (var card in neededCards)
                {
                    Console.WriteLine(card);
                }
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}