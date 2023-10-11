// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T14 - Reduced String
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace Spark;

public class T14 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "String Reducer:-" + "\x1B[0m");
      Console.WriteLine ("Here, the input is a string, the output is a reduced string of lowercase characters by deleting a pair of adjacent letters that match. ");
      string[] testCases = { "  ", "aaabccddd", "aaaabbbb", "AAbcXXY", "xyz1233345667" };
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Test Cases:-" + "\x1B[0m");
      Console.WriteLine ();
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"{i + 1}. {testCases[i], 15} ==> ");
         PrintInBlue (ReducedString (testCases[i]));
      }
      Console.Write ("\nNow, let's input enter a string to reduce: ");
      PrintInBlue (ReducedString (Console.ReadLine ()));
   }

   /// <summary>Returns the reduced string by deleting the pairs of characters if any</summary>
   public static string ReducedString (string input) {
      List<char> output = new ();
      for (int i = 0; i < input.Length; i++) {
         char c = input[i];
         if (output.Count == 0) { output.Add (c); continue; }
         if (output[^1] != c) output.Add (c);
         else output.RemoveAt (output.Count - 1);
      }
      return new string (output.ToArray ());
   }

   /// <summary>Prints the input string with Foreground colour as blue</summary>
   public static void PrintInBlue (string input) {
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.WriteLine (input);
      Console.ResetColor ();
   }
}

