// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T19 - The Factorial Number
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spark;
public class T19 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Nth Factorial Computer:-" + "\x1B[0m");
      List<int> testCases = new () { 0, 1, 2, 3, 5, 8, 10, 12, 15, 16 };
      Console.WriteLine ("Test Cases:");
      for (int i = 0; i < testCases.Count; i++) {
         Console.Write ($"{i + 1,2}.  ");
         PrintResult (testCases[i]);
      }
      Console.WriteLine ();
      Console.Write ("Now, let's try out.  Enter a number between 0 - 15, n = ");
      PrintResult (Convert.ToInt32 (Console.ReadLine ()));
   }

   /// <summary>Print the results in the console page</summary>
   public static void PrintResult (int input) {
      Console.Write ($"{input}! = ");
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine (RFactorialOf (input));
      Console.ResetColor ();
   }

   /// <summary>Returns the Factorial value of given input - WITH RECURSION</summary>
   public static int RFactorialOf (int n) {
      if (n == 0) return 1;
      if (n == 1 || n == 2) return n;
      return n * RFactorialOf (n - 1);
   }
}


