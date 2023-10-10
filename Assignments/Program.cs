using System;
using System.Numerics;
using static System.ConsoleColor;

namespace Spark;

public class T3 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Fibonacci Series Printer:-" + "\x1B[0m");
      List<int> testCases = new () { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 15, 20, 25};
      foreach (int i in testCases) {
         Console.Write ($"\nf{i} ==> ");
         Console.ForegroundColor = Yellow;
         FiboSeries (i);
         Console.ResetColor ();
      }
      Console.Write ("\n\nTry Yourself! Enter the length of series, l = ");
      if (int.TryParse (Console.ReadLine (), out int l)) {
         Console.Write ($"\nf{l} ==> ");
         Console.ForegroundColor = Yellow;
         FiboSeries (l);
         Console.ResetColor ();
      }
   }

   /// <summary>Prints the series of n Fibonacci numbers</summary>
   public static void FiboSeries (int count) {
      if (count <= 0) Console.WriteLine ("Please enter a +ve value");
      BigInteger f0 = 0, f1 = 1;
      while (count > 0) {
         Console.Write ($"{f0}  ");
         (f0, f1) = (f1, f0 + f1);
         count--;
      }
   }

   #region Nth Fibonacci Number:
   /// <summary>Returns nth Fibonacci number using caching</summary>
   public static BigInteger NthFib3 (int n) {
      Console.ForegroundColor = Green;
      BigInteger f0 = 0, f1 = 1;
      while (0 <= n--) {
         if (n == 0) return f0;
         (f0, f1) = (f1, f0 + f1);
      }
      return f0;
   }

   /// <summary>Returns nth Fibonacci number using golden ratio</summary>
   public static BigInteger NthFib1 (int n) {
      //This method works upto (n <= 71)
      double sqrt5 = Math.Sqrt (5);
      double phi = (sqrt5 + 1) / 2; n--;
      return (BigInteger)(Math.Ceiling (Math.Pow (phi, n) - Math.Pow (1 - phi, n)) / sqrt5);
   }

   private static Dictionary<int, BigInteger> Fibos = new ();
   /// <summary>Returns nth Fibonacci number using caching</summary>
   public static BigInteger NthFib2 (int n) {
      Console.ForegroundColor = Green;
      if (n == 1) return 0;
      if (n == 2) return 1;
      if (!Fibos.TryGetValue (n, out BigInteger value)) {
         value = NthFib2 (n - 1) + NthFib2 (n - 2);
         Fibos.Add (n, value);
      }
      return value;
   }
   #endregion
}
