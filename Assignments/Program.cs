// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T4 - GCD & LCM Computer
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;

public class T4 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "GCD & LCM Generator:-" + "\x1B[0m");
      List<List<int>> testCases = new () { new () { 5, 10, 15, 21 }, new () { 6, 18, 36, 54, 96 }, 
         new () { 6, 8, 12, 10, 30, 72, 134 }, new () { 2, 3, 5, 7, 11, 13 }, new () { 17, 34, 51, 68, 36 } };
      for (int i = 0; i < testCases.Count; i++) {
         Console.WriteLine ();
         Console.Write ($"Test case-{i + 1}: ");
         PrintResult (testCases[i]);
      }
      Console.WriteLine ();
      Console.Write ("Now, lets try. Enter the count of no. of inputs (2 - 10): ");
      if (int.TryParse (Console.ReadLine (), out int n) && n > 1 && n < 11) {
         List<int> inputs = new ();
         for (int i = 0; i < n; i++) {
            Console.Write ($"Enter input-{i + 1} = ");
            if (int.TryParse (Console.ReadLine (), out int j)) inputs.Add (j);
            else i--;
         }
         Console.WriteLine ();
         PrintResult (inputs);
      } else {
         Console.ForegroundColor = Red;
         Console.WriteLine ();
         Console.WriteLine ("Program terminated due to incorrect input.");
         Console.ResetColor ();
      }
   }

   /// <summary>Prints the result in the console page</summary>
   public static void PrintResult (List<int> input) {
      var len = input.Count;
      for (int i = 0; i < len; i++) {
         Console.ForegroundColor = Cyan;
         Console.Write (input[i]);
         if (i != len - 1) Console.Write (", ");
      }
      Console.ResetColor ();
      Console.WriteLine ();
      Console.Write ("GCD = "); WriteInYellow ($"{GCDof (input)}");
      Console.Write ("LCM = "); WriteInYellow ($"{LCMof (input)}");
   }

   /// <summary>Returns the Greatest Common Factor of all the numbers in the input list</summary>
   public static int GCDof (List<int> inputs) {
      int result = EuclidGCDof (inputs[0], inputs[1]);
      for (int i = 2; i < inputs.Count; i++)
         result = EuclidGCDof (result, inputs[i]);
      return result;
   }
   
   /// <summary>Returns the Least Common Multiple of all numbers in the input list</summary>
   public static int LCMof (List<int> inputs) {
      int result = EuclidLCMof (inputs[0], inputs[1]);
      for (int i = 2; i < inputs.Count; i++)
         result = EuclidLCMof (result, inputs[i]);
      return result;
   }

   /// <summary>Returns the GCD of input two numbers using Euclidean Algorithm</summary>
   public static int EuclidGCDof (int a, int b) {
      if (a < b) (a, b) = (b, a);
      return a == 0 ? b : (b == 0 ? a : EuclidGCDof (b, a % b));
   }

   /// <summary>Returns the LCM of input two numbers using Euclidean Algorithm</summary>
   public static int EuclidLCMof (int a, int b) 
      => Math.Abs (a * b) / EuclidGCDof (a, b);

   /// <summary>Prints the input string with yellow as foreground colour</summary>
   public static void WriteInYellow (string input) {
      Console.ForegroundColor = Yellow;
      Console.WriteLine (input);
      Console.ResetColor ();
   }
}
