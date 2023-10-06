// ---------------------------------------------------------------------------------------
// T11 - Digital Root
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;

public class T10 {
   public static void Main () {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine ("\x1B[4m" + "Digital Root:" + "\x1B[0m" +
         "\nThe digital root or digital sum of a non-negative integer is the " +
         "single-digit value obtained by an iterative process of summing digits.");
      int[] testCases = new int[] { 0, 1, 9, 21, 321, 4321, 54321, 654321, 7654321, 87654321, 987654321 };
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"{i}. dgrt({testCases[i]}) = ");
         Console.WriteLine ($"{DigitalRootOf (testCases[i])}", Console.ForegroundColor = ConsoleColor.Cyan);
         Console.ResetColor ();
      }
      Console.Write ("\nNow, lets try! Please enter a number: ");
      if (long.TryParse (Console.ReadLine (), out long input) && input > 0) {
         Console.Write ($"dgrt({input}) = ");
         Console.WriteLine ($"{DigitalRootOf (input)}", Console.ForegroundColor = ConsoleColor.Cyan);
         Console.ResetColor ();
         if (input > 9) {
            Console.Write ("Show the steps (y/n)? ");
            if (char.ToLower (Console.ReadKey ().KeyChar) == 'y') {
               Console.WriteLine ("\n\n" + "\x1B[4m" + "Steps:" + "\x1B[0m");
               ShowSteps (input);
            }
         }
      } 
      else Console.WriteLine ("Incorrect Input", Console.ForegroundColor = ConsoleColor.Red);
      Console.ResetColor ();
   }

   /// <summary>Returns the digital root of a input number</summary>
   public static long DigitalRootOf (long input) {
      if (input == 0) return 0;
      else if (input % 9 == 0 && input > 0) return 9;
      else return input % 9;
   }

   /// <summary>Prints the steps to compute digital roots of input number</summary>
   public static void ShowSteps (long input) {
      int x = input.ToString ().Length;
      Console.Write ($"  dgrt({input}) = ");
      SumOf (input);

      void SumOf (long num) {
         List<int> parts = new ();
         Console.Write ($"{new string (' ', 7 + x)}==> ");
         while (num > 9) {
            int d = (int)(num % 10);
            parts.Add (d);
            num = (num - d) / 10;
         }
         if (num < 10) parts.Add ((int)num);
         parts.Reverse ();
         int sum = parts.Sum ();
         if (parts.Count > 1) {
            Console.CursorLeft = 12 + x;
            for (int d = 0; d < parts.Count; d++)
               Console.Write (d == parts.Count - 1 ? parts[d] : $"{parts[d]} + ");
            Console.CursorLeft = 12 + x * 5;
            Console.WriteLine ($" = {sum}");
            if (sum > 9) SumOf (sum);
            else {
               Console.WriteLine ($"∴ dgrt({input}) =  {sum}", Console.ForegroundColor = ConsoleColor.Yellow);
               Console.ResetColor ();
            }
         }
      }
   }
}
