// ---------------------------------------------------------------------------------------
// T5 - Prime Number Checker
// ---------------------------------------------------------------------------------------
using System;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T5 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Prime Number Checker:-" + "\x1B[0m");
      int[] testCases = new int[] { 2, 3, 5, 11, 51, 61, 297, 313, 1381, 1663, 5157, 5153, 7871, 7873, 7919, 110503, 1257787, 3021379, 37156667 };
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"{i + 1}. {testCases[i]} - ", 15);
         PrintResult (i);
      }
      Console.Write ("\nNow, lets enter a number to check: ");
      if (int.TryParse(Console.ReadLine (), out int a)) PrintResult (a);
   }

   /// <summary>Prints the result in the console page</summary>
   public static void PrintResult (int input) {
      (string output, Console.ForegroundColor) = IsPrime (input) ? ("Prime", Green) : ("Not a Prime", Red);
      Console.WriteLine ($"{output}");
      Console.ResetColor ();
   }
   /// <summary>Returns true if the input number is a prime, else false</summary>
   public static bool IsPrime (int num) {
      if (num < 2) return false;
      for (int i = 2; i <= Math.Sqrt (num); i++) {
         if (num % i == 0) return false;
      }
      return true;
   }

   /// <summary>Returns the Nth prime number</summary>
   public static int NthPrimeOf (int n) {
      int count = 0;
      for (int i = 2; count <= n; i++) {
         if (IsPrime (i)) count++;
         if (count == n) return i;
      }
      return 1;
   }

}