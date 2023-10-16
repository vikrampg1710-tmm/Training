// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T16 - Longest Abecedarian Word
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;
public class T16 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Longest Abecedarian Finder:-" + "\x1B[0m");
      List<string> words = new () { "apple", "firsty", "antwz", "efghijlk", "pqrst", "orange" };
      PrintResult (words);
      Console.WriteLine ("\nNow, let's try words 3 different words:- ");
      words.Clear ();
      for (int i = 0; i < 3; i++) {
         Console.Write ($"Enter words {i + 1}: ");
         words.Add (Console.ReadLine ());
      }
      PrintResult (words);
   }

   /// <summary>Returns the Longest Abecedarian Word from the given list of words</summary>
   public static string LongestAbecedarianWordOf (List<string> input) {
      input = input.OrderByDescending (a => a.Length).ToList ();
      foreach (string word in input)
         if (word == string.Concat (word.OrderBy (a => a))) return word;
      return "";
   }

   /// <summary>Prints the results in the console page</summary>
   public static void PrintResult (List<string> input) {
      Console.Write ("Input words: ");
      Console.ForegroundColor = ConsoleColor.Yellow;
      foreach (var w in input) Console.Write ($"{w}  ");
      Console.ResetColor ();
      Console.Write ($"\r\nThe Longest Abcdarian words is ");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine ($"[{LongestAbecedarianWordOf (input)}]");
      Console.ResetColor ();
   }
}

