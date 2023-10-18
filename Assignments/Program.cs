// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T22 - Swap Indices Values
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;

public class T22 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Index Swaping Game:-" + "\x1B[0m");
      Console.WriteLine ("Random series of numbers will be displayed. "
         + "The user has to enter two indeces value for the values to be swaped.");
      PerformIndexSwap ();
   }

   /// <summary>Performs the index value swapping of a random sequence in the console page</summary>
   public static void PerformIndexSwap () {
      Console.WriteLine ();
      Random r = new ();
      Console.Write ("Enter the length of sequence (2 - 15): ");
      if (int.TryParse (Console.ReadLine (), out int num) && num < 16 && num > 1) {
         List<int> list = new ();
         int ind1 = -1, ind2 = -1, index = -1;
         bool first = true;
         for (int i = 0; i < num; i++) list.Add (r.Next (1, 100));
         Console.WriteLine ($"\nHere is the sequence of {num} numbers...");
         DisplayInBox (list, ind1, ind2);
         Console.WriteLine ("Now, give your index values.");
         while (index < 0) {
            Console.Write ($"Enter {(first ? "1st" : "2nd")} index: ");
            index = Convert.ToInt32 (Console.ReadLine ());
            if (index == ind1 || index > num - 1) {
               WriteLine (index == ind1 ? "Index1 and Index2 should not be same, give different index." : "Index out of range!", Red);
               Console.WriteLine ();
               index = -1;
               continue;
            }
            if (first) { ind1 = index; index = -1; first = false; } 
            else ind2 = index;
         }
         (list[ind1], list[ind2]) = (list[ind2], list[ind1]);
         Console.WriteLine ("\r\nAfter swaping ==>\r\n");
         DisplayInBox (list, ind1, ind2);
         Console.WriteLine ();
      } else WriteLine ("Program terminated due to incorrect input.", Red);


      ///<summary>Displays the input list items using box unicodes</summary>
      static void DisplayInBox (List<int> list, int ind1, int ind2) {
         int lastInd = list.Count;
         for (int x = 0; x < lastInd; x++) {
            Console.ForegroundColor = (x == ind1 || x == ind2) ? Blue : Yellow;
            Console.Write ($"{x,4} ");
         }
         Console.ResetColor ();
         for (int i = 0; i < 2; i++) {
            string s = (i == 0) ? "┌┬┐" : "└┴┘";
            for (int j = 0; j < lastInd + 1; j++)
               Console.Write (j == lastInd ? $"{s[2]}\n" : (j == 0 ? $"\n{s[0]}" : s[1]) + "────");
            if (i == 1) break;
            Console.ResetColor ();
            for (int k = 0; k < lastInd + 1; k++) {
               Console.Write (k == lastInd ? $"│" : $"│ {list[k],2} ");
            }
         }
      }
   }

   /// <summary>Writes the input string in specified foreground color</summary>
   public static void WriteLine (string s, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (s);
      Console.ResetColor ();
   }
}
