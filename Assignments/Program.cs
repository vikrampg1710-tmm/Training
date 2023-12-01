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
      var wordList = new List<string> (File.ReadAllLines (@"c:/etc/words.txt"));
      Dictionary<string, List<string>> anagrams = new ();
      int count = 0;
      int[] hashTable = new int[26];
      var watch = Stopwatch.StartNew (); //.............................TIMER ON
      foreach (string s in wordList) {
         string Hash = HashCode (ref hashTable, s);
         if (anagrams.TryGetValue (Hash, out List<string> value)) value.Add (s);
         else anagrams.Add (Hash, new List<string> { s });
      }
      watch.Stop ();//..................................................TIMER OFF
      Console.WriteLine ("\x1B[4m" + "Anagrams:-" + "\x1B[0m");
      foreach (var kvp in anagrams.Where (a => a.Value.Count > 1).OrderByDescending (a => a.Value.Count))
         Console.WriteLine ($"{++count,4}. {kvp.Value.Count,2}- {string.Join (", ", kvp.Value)}");
      Console.WriteLine ($"\nThe Execution time of the program: {watch.ElapsedMilliseconds} ms");

      /// <summary>Returns an unique string made of hash-table digits of given input string</summary>
      static string HashCode (ref int[] array, string word) {
         array = new int [26];
         foreach (char c in word) array[c - 65]++;
         return string.Join ("", array);
      }
   }

}


