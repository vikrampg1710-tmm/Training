// ---------------------------------------------------------------------------------------
// T14 - Reduced String
// ---------------------------------------------------------------------------------------
using System.Net.Http.Headers;
using static System.ConsoleColor;
namespace Spark;

public class T14 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "String Reducer:-" + "\x1B[0m");
      Console.WriteLine ("Here, the input is a string, the output is a reduced string of lowercase characters by deleting a pair of adjacent letters that match. ");
      string[] testCases = { "aaabccddd", "aaaabbbb", "AAbcXXY", "xyz1233345667", "  " };
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Test Cases:-" + "\x1B[0m");
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"\n{i + 1}. {testCases[i]} ==> ");
         Console.ForegroundColor = Yellow;
         Console.WriteLine ($"{ReducedString (testCases[i])}");
         Console.ResetColor ();
      }
      Console.Write ("\nNow, let's input enter a string to reduce: ");
      string input = Console.ReadLine ();
      Console.ForegroundColor = Yellow;
      Console.WriteLine ($"{ReducedString (input)}");
      Console.ResetColor ();
   }

   public static string ReducedString (string input) {
      List<char> output = new ();
      for (int i = 0; i < input.Length; i++) {
         char c = input[i];
         if (output.Count == 0) { output.Add (c); continue; }
         if (output[^1] != c) output.Add (c);
         else output.RemoveAt (output.Count - 1);
      }
      return string.Join ("", output);
   }
}

