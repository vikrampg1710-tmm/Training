// ---------------------------------------------------------------------------------------
// T9 - Print Diamond
// ---------------------------------------------------------------------------------------

using System;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T9 {
   public static void Main (string[] args) {
      Console.WriteLine ("\x1B[4m" + "N-Row Diamond Printer:-" + "\x1B[0m");
      Console.Write ("\nEnter the number of diamond rows (1-50): ");
      if (int.TryParse (Console.ReadLine (), out int row) && row > 0 && row < 51)
         PrintDiamond (row);
   }

   /// <summary>Prints Diamond shape using asterisk in the console page</summary>
   public static void PrintDiamond (int row) {
      Console.ResetColor ();
      int midPos = Console.WindowWidth / 2;
      Console.Write ("\nEnter the row length in integer (1-50): ");
      bool above = true;
      for (int i = (above ? 1 : row); above ? (i <= row) : (i >= 1); i += above ? 1 : -1) {
         Console.CursorLeft = midPos - i + 1;
         for (int j = 1; j <= 2 * i - 1; j++)
            Console.Write ("*", Console.ForegroundColor = (j % 2 == 0) ? Magenta : Blue);
         Console.WriteLine ();
         if (above && i == row) above = false;
      }
      Console.ResetColor ();
   }
}
