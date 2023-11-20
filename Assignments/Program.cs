// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T21 - Swap Two Numbers
// ---------------------------------------------------------------------------------------

using System;

namespace Spark;
public class T21 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Swap Numbers:" + "\x1B[0m");
      Console.Write ("Enter Number, a = ");
      int a = Convert.ToInt32 (Console.ReadLine ());
      Console.Write ("Enter Number, b = ");
      int b = Convert.ToInt32 (Console.ReadLine ());
      (a, b) = Swap (a, b);
      Console.WriteLine (value: $"\r\nAfter Swapping: \r\na = {a}\r\nb = {b}");
   }

   /// <summary>Swaps the given two numbers</summary>
   public static (int, int) Swap (int a, int b) {
      int temp = a;
      a = b; 
      b = temp;
      return (a, b);
   }
}

