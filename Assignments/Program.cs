// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T15 - Isogram Checker
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;

public class T15 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Check for Isogram Word:-" + "\x1B[0m");
      Console.WriteLine ("An isogram is a word that has no duplicate letters.");
      List<string> testCases = new () { "", "apple", "banana", "cat", "doctrine", "elephant", 
         "fruits", "pebble", "rubiks", "yellow", "zebra", "12345", "123345", "@#$4%! -)", "*765^%$*" };
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Test Cases:" + "\x1B[0m");
      for (int i = 0; i < testCases.Count; i++) {
         var t = testCases[i];
         Console.Write ($"{i + 1, 2}. {t, 11} - ");
         PrintResult (t);
      }
      Console.Write ("\nNow, let's enter a word: ");
      PrintResult (Console.ReadLine ());
   }

   /// <summary>Prints the result whether the input is isogram or not</summary>
   public static void PrintResult (string input) {
      (string output, Console.ForegroundColor) = IsIsogram (input) ? ("Isogram", Green) : ("Not an Isogram", Red);
      Console.WriteLine ($"{output}");
      Console.ResetColor ();
   }

   /// <summary>Returns true if input string is an isogram, otherwise false</summary>
   public static bool IsIsogram (string input) {
      if (input.Length == 0) return false;
      input = input.ToUpper ();
      Dictionary<char, bool?> freq = new ();
      foreach (char c in input) {
         if (freq.TryGetValue (c, out bool? _)) return false;
         else freq.Add (c, null);
      }
      return true;
   }
}
 