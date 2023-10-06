// ---------------------------------------------------------------------------------------
// T8 - Multiplication Table
// ---------------------------------------------------------------------------------------
using System;

namespace Spark;

public class T8 {
   public static void Main () {
      MultiplicationTables ();
   }
   public static void MultiplicationTables () {
      Console.WriteLine ("\x1B[4m" + "1 - 10 Multiplication Tables:-" + "\x1B[0m");
      for (int i = 1; i < 11; i++) {
         Console.WriteLine ($"\n{i}-Table", 12);
         for (int j = 1; j < 12; j++) {
            if (j == 1 || j == 11) {
               string s = j == 1 ? "┌┐" : "└┘";
               Console.WriteLine (s[0] + new string ('─', 15) + s[1]);
            }
            if (j != 11) Console.WriteLine ($"│ {i,2} x {j,2} = {i * j,3} │");
         }
      }
   }
}
