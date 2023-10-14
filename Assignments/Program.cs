// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T10 - Reverse perform a given integer
// ---------------------------------------------------------------------------------------

using System;
using static System.ConsoleColor;
namespace Spark;

public class Q10 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Integer Reversing:" + "\x1B[0m");
      int[] testCases = new int[] { 0, 1, 9, 21, 321, 4321, 54321, 654321, 7654321, 87654321, 987654321, -1, -54321, -987654321 };
      int count = 0;
      foreach (int i in testCases) {
         Console.Write ($"{count++}. ");
         PrintResult (i);
         Console.WriteLine ();
      }
      Console.Write ("\nNow, lets try! Please enter an number: ");
      PrintResult (Convert.ToInt32(Console.ReadLine ()));
   }


   /// <summary>Prints the result in the console page</summary>
   public static void PrintResult (int input) {
      int output = ReverseInteger (input);
      Console.Write ($"{input} ==> ");
      Console.ForegroundColor = Cyan;
      Console.Write (output);
      if (input == output) {
         Console.ForegroundColor = Yellow;
         Console.Write (" (Palindrome)");
      }
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
