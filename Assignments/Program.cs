// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T12 - Pascal's Triangle
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spark;

public class T12 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Pascal Triangle Printer:-" + "\x1B[0m");
      Console.Write ("\r\nEnter the length of the tringle (l <= 13): ");
      int len = Convert.ToInt32 (Console.ReadLine ());
      PascalTriangle ((len < 14) ? len : 13);
   }

   /// <summary>Prints the Pascal's Traingle of given length</summary>
   public static void PascalTriangle (int length) {
      List<int> roots = new () { 1 }, temp = new ();
      int midPos = Console.WindowWidth / 2;
      Console.CursorLeft = midPos - 1;
      Console.WriteLine (roots[0]);
      while (length > 1) {
         int size = roots.Count;
         for (int i = 0; i < size; i++) {
            int item = roots[i];
            if (i == 0) temp.Add (item);
            else temp.Add (item + roots[i - 1]);
         }
         temp.Add (roots[size - 1]);
         string s = string.Join ($"  ─  ", temp);
         Console.WriteLine ();
         Console.CursorLeft = midPos - s.Length / 2 - 1;
         Console.WriteLine (s);
         roots.Clear ();
         roots.AddRange (temp);
         temp.Clear ();
         length--;
      }
   }
}
