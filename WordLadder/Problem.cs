using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WordLadder;

public class Problem
{
    #region Init

    private readonly string _beginWord;
    private readonly string _endWord;
    private readonly IList<string> _wordList;
    private int MinLength = int.MaxValue;

    public Problem(string beginWord, string endWord, IList<string> wordList)
    {
        _beginWord = beginWord;
        _endWord = endWord;
        _wordList = wordList;
    }
    private List<string> BeginList
    {
        get
        {
            var list = new List<string>
            {
                _beginWord
            };
            return list;
        }
    }

    public List<List<string>> Solve()
    {
        #region Begin
        Console.WriteLine($"Shortest transformation sequences:");
        #endregion

        #region Solve
        var result = FindSolution();
        #endregion

        #region End
        PrintResult(result);
        return result;
        #endregion
    }

    #endregion

    #region Solution
    private List<List<string>> FindSolution()
    {
        var result = new List<List<string>>();

        var tmpResult = FindAllSequences(BeginList);
        foreach (var item in tmpResult)
        {
            if (item.Count == MinLength)
                result.Add(item);
        }

        return result;
    }

    private List<List<string>> FindAllSequences(List<string> list)
    {
        var result = new List<List<string>>();

        if (list.Last().Equals(_endWord))
        {
            MinLength = Math.Min(list.Count, MinLength);

            result.Add(list);
            return result;
        }

        foreach (var word in _wordList)
        {
            if (!list.Contains(word) && IsOneLetterDifference(word, list.Last()))
            {
                var tmpList = new List<string>(list)
                {
                    word
                };

                var tmpResult = FindAllSequences(tmpList);
                foreach (var item in tmpResult)
                {
                    if (IsUnique(result, item))
                    {
                        result.Add(item);
                    }
                }

            }
        }

        return result;
    }

    private bool IsUnique(List<List<string>> result, List<string> item)
    {
        foreach (var list in result)
        {
            if (item.SequenceEqual(list))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsOneLetterDifference(string word1, string word2)
    {
        return word1.Except(word2).ToList().Count == 1;
    }

    #endregion

    private static void PrintResult(List<List<string>> result)
    {
        Console.WriteLine($"Total {result.Count} sequence possible.");

        result.ForEach(item => Console.WriteLine("[" + string.Join(", ", item) + "]"));
    }

}