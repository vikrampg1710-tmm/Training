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
      // Loading all the dictionary words from a assembly-manifest resource file.
      string[] wordList = File.ReadAllLines (@"c:\etc\words.txt");
      Dictionary<string, List<string>> anagrams = new ();
      int count = 0;
      var watch = Stopwatch.StartNew (); //.............................TIMER ON
      foreach (string s in wordList) {
         string hash = HashCode (s);
         if (anagrams.TryGetValue (hash, out List<string> value)) value.Add (s);
         else anagrams.Add (hash, new List<string> { s });
      }
      watch.Stop ();//..................................................TIMER OFF
      Console.WriteLine ("\x1B[4m" + "Anagrams:-" + "\x1B[0m");
      foreach (var kvp in anagrams.Where (a => a.Value.Count > 1).OrderByDescending (a => a.Value.Count))
         Console.WriteLine ($"{++count,4}. {kvp.Value.Count,2}- {string.Join (", ", kvp.Value)}");
      Console.WriteLine ($"\nThe Execution time of the program: {watch.ElapsedMilliseconds} ms");
   }

   /// <summary>Returns an unique string made of hash-table digits of given input string</summary>
   // Basically, this method returns a string whose length is 26 with chars either '1' or '0'.
   // The char at Nth index in the returned string represents the presence/absence of (N+1)th alphabet in the given word given word.
   // So the char '1' at 0th index in the returned string implies that 1st alphabet 'A' is present in the given string & vice versa.
   static string HashCode (string word) {
      int[] array = new int[26];
      foreach (char c in word) array[c - 65]++;
      return string.Join ("", array);
   }
}


