// ---------------------------------------------------------------------------------------
// T14 - Reduced String
// ---------------------------------------------------------------------------------------

using System;
using static System.ConsoleColor;
namespace Spark;

public class T14 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "String Reducer:-" + "\x1B[0m");
      Console.WriteLine ("Here, the input is a string, the output is a reduced string of lowercase characters by deleting a pair of adjacent letters that match. ");
      string[] testCases = { "aaabccddd", "aaaabbbb", "AAbcXXY", "xyz1233345667", "  " };
      Console.WriteLine ("\n\x1B[4m" + "Test Cases:-" + "\x1B[0m");
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"\n{i + 1}. {testCases[i]} ==> ");
         Console.WriteLine ($"{ReducedString (testCases[i])}", Console.ForegroundColor = Yellow);
         Console.ResetColor ();
      }
      Console.Write ("\nNow, let's input enter a string to reduce: ");
      string input = Console.ReadLine ();
      Console.WriteLine ($"{ReducedString (input)}", Console.ForegroundColor = Yellow);
      Console.ResetColor ();
   }
   public static string ReducedString (string input) {
      string output = "";
      for (int i = 0; i < input.Length; i++) {
         if (output.Length == 0) { output += input[i]; continue; }
         if (output[^1] != input[i]) output += input[i];
         else output = output[..^1];
      }
      return output;
   }
}

