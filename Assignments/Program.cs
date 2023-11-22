// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A14.1 - Anagram
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace Academy;
public class A14 {
   public static void Main () {
      var DictWords = new List<string> (File.ReadAllLines (@"c:/etc/words.txt"));
      Dictionary<string, List<string>> Anagrams = new ();
      int count = 0;
      List<int> HashTable;
      var watch = Stopwatch.StartNew (); //.............................TIMER ON
      foreach (string A in DictWords) {
         string Hash = HashTableOf (A);
         if (Anagrams.TryGetValue (Hash, out List<string> value)) value.Add (A);
         else Anagrams.Add (Hash, new List<string> { A });
      }
      watch.Stop ();//..................................................TIMER OFF
      Console.WriteLine ("\x1B[4m" + "Anagrams:-" + "\x1B[0m");
      foreach (KeyValuePair<string, List<string>> abc in Anagrams.Where (abc => abc.Value.Count >= 2).OrderByDescending (a => a.Value.Count))
         Console.WriteLine ($"{++count,4}. {abc.Value.Count,2}- {string.Join (", ", abc.Value)}");
      Console.WriteLine ($"\nThe Execution time of the program: {watch.ElapsedMilliseconds} ms");

      /// <summary>Returns an unique string made of hash-table digits of given input string</summary>
      string HashTableOf (string word) {
         HashTable = new () { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
         foreach (char c in word) HashTable[c - 65]++;
         return string.Join ("", HashTable);
      }
   }

}


