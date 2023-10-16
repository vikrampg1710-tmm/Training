﻿// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T16 - Longest Abcdedarian Word
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;
public class T16 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Longest Abcdedarian Finder:-" + "\x1B[0m");
      List<string> words = new () { "apple", "first", "antwz", "efghijlk", "pqrst", "orange" };
      PrintResult (words);
      Console.WriteLine ("\nNow, let's try words 3 different words:- ");
      words.Clear ();
      for (int i = 0; i < 3; i++) {
         Console.Write ($"Enter words {i + 1}: ");
         words.Add (Console.ReadLine ());
      }
      PrintResult (words);
   }

   /// <summary>Returns the Longest Abcdedarian Word from the given list of words</summary>
   public static string LongestAbcdedarianWordOf (List<string> input) {
      input = input.OrderBy (a => a.Length).ToList ();
      foreach (string word in input)
         if (word == string.Concat (word.OrderBy (a => a))) return word;
      return "";
   }

   /// <summary>Prints the longest abcedarian word using the above method</summary>
   public static void PrintResult (List<string> input) {
      Console.Write ("Input words: ");
      Console.ForegroundColor = ConsoleColor.Yellow;
      foreach (var wrd in input) Console.Write ($"{wrd}  ");
      Console.ResetColor ();
      Console.Write ($"\r\nThe Longest Abcdarian words is ");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine ($"[{LongestAbcdedarianWordOf (input)}]");
      Console.ResetColor ();
   }
}

