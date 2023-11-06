// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T24 - Voting Contest
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T24 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Voting Contest:-" + "\x1B[0m");
      List<string> testCases = new () { "", "AabBBcd", "HelloWorld", "Education", "Alphaleable" };
      Console.WriteLine ("\r\nTest Cases:");
      for (int i = 0; i < testCases.Count; i++) {
         Console.Write ($"{i + 1,2}. ");
         PrintResult (testCases[i]);
         Console.WriteLine ();
      }
      Console.Write ("Now, let's enter a string: ");
      PrintResult (Console.ReadLine ());
   }

   /// <summary>Prints the result in the console page</summary>
   public static void PrintResult (string input) {
      Console.Write ($"Input string : ");
      if (input.Length == 0) { PrintInColor ("Invalid input!", Red); return; }
      PrintInColor (input, Yellow);
      Console.Write ($"    Winning Contestant: ");
      PrintInColor ($"{WinnersOf (input)}", Green);
   }

   /// <summary>Returns a single char which has highest frequencies in the given string</summary>
   public static char WinnersOf (string input) {
      input = input.ToUpper ();
      Dictionary<char, int> freq = new ();
      foreach (char c in input) {
         if (freq.TryGetValue (c, out int _)) freq[c]++;
         else freq.Add (c, 1);
      }
      return freq.OrderByDescending (a => a.Value).FirstOrDefault ().Key;
   }
   
   /// <summary>Prints the given string in given foreground color</summary>
   public static void PrintInColor (string s, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (s);
      Console.ResetColor ();
   }
}
