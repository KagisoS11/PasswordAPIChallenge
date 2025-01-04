using System;
using System.IO;
using System.Collections.Generic;

class DictionaryGenerator
{
    public static void GenerateDictionary()
    {
        string word = "password";
        List<string> permutations = new List<string>();
        GeneratePermutations("", word, permutations);
        File.WriteAllLines("dict.txt", permutations);
        Console.WriteLine("Dictionary generated with {0} entries.", permutations.Count);
    }

    static void GeneratePermutations(string prefix, string remainder, List<string> results)
    {
        if (remainder.Length == 0)
        {
            results.Add(prefix);
            return;
        }

        char currentChar = remainder[0];
        string rest = remainder.Substring(1);
        var replacements = GetReplacements(currentChar);

        foreach (var replacement in replacements)
        {
            GeneratePermutations(prefix + replacement, rest, results);
        }
    }

    static List<string> GetReplacements(char c)
    {
        return c switch
        {
            'a' => new List<string> { "a", "A", "@" },
            's' => new List<string> { "s", "S", "5" },
            'o' => new List<string> { "o", "O", "0" },
            _ => new List<string> { c.ToString(), char.ToUpper(c).ToString() }
        };
    }
}
