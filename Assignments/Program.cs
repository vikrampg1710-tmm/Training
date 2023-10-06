// ---------------------------------------------------------------------------------------
// T12 - Pascal's Triangle
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;

public class T12 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Pascal Triangle Printer:-" + "\x1B[0m");
      Console.Write ("\nEnter the length of the tringle (l <= 13): ");
      int len = Convert.ToInt32 (Console.ReadLine ());
      PascalTriangle (len < 14 ? len : 13);
   }

   /// <summary>Prints the Pascal's Traingle of given length</summary>
   public static void PascalTriangle (int length) {
      bool putSlash = length < 9;
      int midPos = Console.WindowWidth / 2;
      List<int> roots = new () { 1 };
      List<int> temp = new ();
      Console.CursorLeft = midPos - 1;
      int left = Console.CursorLeft - 3;
      Console.WriteLine (roots[0]);
      while (length > 0) {
         int size = roots.Count;
         for (int i = 0; i < size; i++) {
            int item = roots[i];
            if (i == 0) temp.Add (item);
            else temp.Add (item + roots[i - 1]);
         }
         temp.Add (roots[size - 1]);
         if (putSlash) {
            Console.CursorLeft = left - 1;
            for (int x = 0; x < temp.Count - 1; x++)
               Console.Write ($"{new string (' ', (int)Math.Floor (Math.Log10 (temp[x])))}  /{new string (' ', 3)}\\", 5);
         }
         Console.WriteLine ();
         
         string s = string.Join ($"  ─   ", temp);
         Console.CursorLeft = midPos - s.Length / 2 - 1;
         left = Console.CursorLeft - 3;
         Console.WriteLine (s);
         length--;
         roots.Clear ();
         roots.AddRange (temp);
         temp.Clear ();
      }
   }
}
