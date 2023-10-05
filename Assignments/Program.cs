// ---------------------------------------------------------------------------------------
// T5 - Prime Number Checker
// ---------------------------------------------------------------------------------------
using System;
using System.Numerics;
using static System.ConsoleColor;

namespace Spark;

public class T5 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Prime Number Checker:-" + "\x1B[0m");
      int count = 1;
      string output;
      int[] test = new int[] { 2, 3, 5, 11, 51, 61, 297, 313, 1381, 1663, 5157, 5153, 7871, 7873, 7919, 110503, 1257787, 3021379, 37156667 };
      foreach (int i in test) {
         Console.Write ($"{count++}. {i} ");
         (output, Console.ForegroundColor) = IsPrime (i) ? ("Prime", Green) : ("Not a Prime", Red);
         Console.WriteLine ($" - {output}");
         Console.ResetColor ();
      }
      Console.Write ("\nNow, lets enter a number to check: ");
      int s = Convert.ToInt32 (Console.ReadLine ());
      (output, Console.ForegroundColor) = IsPrime (s) ? ("Prime", Green) : ("Not a Prime", Red);
      Console.WriteLine ($"{output}");
      Console.ResetColor ();
   }

   /// <summary>Returns true if the input number is a prime, else false</summary>
   public static bool IsPrime (long num) {
      for (int i = 2; i <= Math.Sqrt (num); i++) {
         if (num % i == 0) return false;
      }
      return true;
   }

   /// <summary>Returns the Nth prime number</summary>
   public static long NthPrimeOf (int n) {
      int nCount = 0;
      for (long i = 2; nCount <= n; i++) {
         if (IsPrime (i)) nCount++;
         if (nCount == n) return i;
      }
      return 1;
   }

}