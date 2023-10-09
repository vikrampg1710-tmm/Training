// ---------------------------------------------------------------------------------------
// T15 - Isogram
// ---------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using static System.ConsoleColor;

namespace Spark;

public class T15 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Check for Isogram Word:-" + "\x1B[0m");
      Console.WriteLine ("An isogram is a word that has no duplicate letters.");
      List<string> testCases = new () { "", "apple", "banana", "cat", "doctrine", "elephant", "fruits", "pebble", "rubiks", "yellow", "zebra", "12345", "123345", "@#$4%! -)", "*765^%$*" };
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Test Cases:" + "\x1B[0m");
      for (int i = 0; i < testCases.Count; i++) {
         var t = testCases[i];
         Console.Write ($"{i + 1, 2}. {t, 11} - ");
         perform (t);
      }
      Console.Write ("\nNow, let's enter a word: ");
      perform (Console.ReadLine ());
   }

   /// <summary>Prints the result whether the input is isogram or not</summary>
   public static void perform (string input) {
      (string output, Console.ForegroundColor) = IsIsogram (input) ? ("Isogram", Green) : ("Not an Isogram", Red);
      Console.WriteLine ($"{output}");
      Console.ResetColor ();
   }

   /// <summary>Returns true if input string is an isogram, otherwise false</summary>
   public static bool IsIsogram (string input) {
      if (input.Length == 0) return false;
      input = input.ToUpper ();
      Dictionary<char, int> freq = new ();
      foreach (char c in input) {
         if (freq.TryGetValue (c, out int _)) return false;
         else freq.Add (c, 1);
      }
      return true;
   }
}
