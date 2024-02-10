using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestContest
{
    internal class Program
    {
        static List<List<string>> GetInput()
        {
            int ReadSingleNumber()
            {
                return Convert.ToInt32(Console.ReadLine());
            }

            List<List<string>> input = new();

            int numOfGroups = ReadSingleNumber();

            for (int i = 0; i < numOfGroups; i++)
            {
                int numOfEntries = ReadSingleNumber();
                List<string> entries = new List<string>();

                for (int j = 0; j < numOfEntries; j++)
                {
                    entries.Add(Console.ReadLine());
                }

                input.Add(entries);
            }

            return input;
        }

        class CommentTree
        {
            public int ID;
            public int ParentID;
            public string Text;

            public SortedList<int, CommentTree> Children;
        }

        static void Solve(List<List<string>> entries)
        {
            foreach (var group in entries)
            {
                Dictionary<int, CommentTree> idToCommentTree = new();

                foreach (var comment in group)
                {                    
                    int secondWhitespaceIndex = comment.IndexOf(' ', comment.IndexOf(' ') + 1);
                    string[] ids = comment.Substring(0, secondWhitespaceIndex).Split(' ');

                    int id = Convert.ToInt32(ids[0]);
                    int parentId = Convert.ToInt32(ids[1]);
                    string text = comment.Substring(secondWhitespaceIndex + 1);

                    CommentTree newComment = new CommentTree()
                    {
                        ID = id,
                        ParentID = parentId,
                        Text = text,
                        Children = new SortedList<int, CommentTree>()
                    };

                    idToCommentTree.Add(id, newComment);                    
                }

                CommentTree root = new CommentTree()
                {
                    Children = new()
                };

                foreach (var commentTree in idToCommentTree.Values)
                {
                    if (commentTree.ParentID != -1)
                    {
                        idToCommentTree[commentTree.ParentID].Children.Add(commentTree.ID, commentTree);
                        continue;
                    }

                    root.Children.Add(commentTree.ID, commentTree);
                }

                void Output(CommentTree currentNode, string prefix)
                {
                    Console.WriteLine(currentNode.Text);

                    for (int i = 0; i < currentNode.Children.Count; i++)
                    {
                        Console.WriteLine(prefix + '|');
                        Console.Write(prefix + "|--");

                        if (i == currentNode.Children.Count - 1)
                        {
                            Output(currentNode.Children.Values[i], prefix + "   ");
                            continue;
                        }

                        Output(currentNode.Children.Values[i], prefix + "|  ");
                    }
                }

                foreach (var commentTree in root.Children.Values)
                {
                    Output(commentTree, "");
                    Console.WriteLine();                    
                }
            }
        }

        static void Main(string[] args)
        {
            Solve(GetInput());
        }
    }
}