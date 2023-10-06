// ---------------------------------------------------------------------------------------
// T10 - Reverse perform a given integer
// ---------------------------------------------------------------------------------------

using System;
using System.Numerics;

namespace Spark;

public class Q10 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Integer Reversing:" + "\x1B[0m");
      int[] testCases = new int[] { 0, 1, 9, 21, 321, 4321, 54321, 654321, 7654321, 87654321, 987654321, -1, -54321, -987654321 };
      int count = 0;
      foreach (int i in testCases) {
         Console.Write ($"{count++}. {i} ==> ");
         Console.WriteLine ($"{ReverseInteger (i)}", Console.ForegroundColor = ConsoleColor.Cyan);
         Console.ResetColor ();
      }
      Console.Write ("\nNow, lets try! Please enter an number: ");
      string num = Console.ReadLine ();
      if (num.Length < 10 && int.TryParse (num, out int input)) { 
         Console.Write ($"{input} ==> ");
         Console.WriteLine ($"{ReverseInteger (input)}", Console.ForegroundColor = ConsoleColor.Cyan);
         Console.ResetColor ();
      }
      else Console.WriteLine ("Incorrect Input", Console.ForegroundColor = ConsoleColor.Red);
      Console.ResetColor ();
   }

   /// <summary>Returns the reversed integer of a input number</summary>
   public static int ReverseInteger (int input) {
      int rInput = 0, sign = 1, d;
      if (input < 0) sign *= -1; input *= sign;
      int len = (int)Math.Floor (Math.Log10 (input)) + 1;
      for (int i = 0; i < len; i++) {
         d = input % 10;
         input = (input - d) / 10;
         rInput += d * (int)Math.Pow (10, len - 1 - i);
      }
      return rInput * sign;
   }
}
