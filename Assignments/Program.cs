// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T11 - Digital Root
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

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
         PrintInColor ($"{DigitalRootOf (testCases[i])}", Cyan);
      }
      Console.Write ("\nNow, lets try! Please enter a number: ");
      if (long.TryParse (Console.ReadLine (), out long input) && input > 0) {
         Console.Write ($"dgrt({input}) = ");
         PrintInColor ($"{DigitalRootOf (input)}", Cyan);
         if (input > 9) {
            Console.Write ("Show the steps (y/n)? ");
            if (char.ToLower (Console.ReadKey ().KeyChar) == 'y') {
               Console.WriteLine ("\n\n" + "\x1B[4m" + "Steps:" + "\x1B[0m");
               ShowSteps (input);
            }
         }
      } else PrintInColor ("Incorrect Input", Red);
      Console.ResetColor ();
   }

   /// <summary>Returns the digital root of a input number</summary>
   public static long DigitalRootOf (long input) {
      if (input == 0) return 0;
      else return 1 + (input - 1) % 9;
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
            else PrintInColor ($"∴ dgrt({input}) =  {sum}", Green);
         }
      }
   }

   /// <summary>Prints the given string in specified foreground color</summary>
   public static void PrintInColor (string input, ConsoleColor ColorName) {
      Console.ForegroundColor = ColorName;
      Console.WriteLine (input);
      Console.ResetColor ();
   }
}
