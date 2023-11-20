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
      Swap (ref a, ref b);
      Console.WriteLine (value: $"\r\nAfter Swapping: \r\na = {a}\r\nb = {b}");
   }

   /// <summary>Swaps the given two numbers</summary>
   public static void Swap (ref int a, ref int b) {
      int temp = a;
      a = b; 
      b = temp;
   }
}

