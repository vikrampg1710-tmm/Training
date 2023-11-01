// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T18 - The Armstrong Number
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;
public class T18 {
   public static void Main () {
      List<int> task = new () { 0, 1, 2, 5, 8, 11, 15, 17, 20, 25, 26, 10, 9 },
                task2 = new () { 0, 1, 7, 10, 151, 370, 371, 372, 9474, 93085, 93084, 4210816, 4210818, 24678050, 24678051 };
      Console.WriteLine ("\x1B[4m" + "Nth Armstrong Number Computer:-" + "\x1B[0m");
      PerformTasks (task, true);
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Armstrong Number Checker:-" + "\x1B[0m");
      PerformTasks (task2, false);
   }

   /// <summary>Prints the results in the console page</summary>
   public static void PerformTasks (List<int> input, bool computeNthArmstrong) {
      for (int i = 0; i < input.Count; i++) {
         int num = input[i];
         string ordinal = (num > 3 || num == 0) ? "th" : (num == 1 ? "st" : (num == 2 ? "nd" : "rd"));
         Console.Write ($"{i + 1,2}. {num}");
         if (computeNthArmstrong) {
            Console.Write ($"{ordinal} Armstrong Number = ");
            Console.ForegroundColor = Green;
            Console.WriteLine (NthArmstrongNum (num));
            Console.ResetColor ();
         } else {
            Console.Write (" - ");
            (string answer, Console.ForegroundColor) = IsArmstrong (num) ? ("An Armstrong Number", Green) : ("Not an Armstrong Number", Red);
            Console.WriteLine (answer);
            Console.ResetColor ();
         }
      }
   }

   /// <summary>Returns the Nth Armstrong number of given input</summary>
   public static int NthArmstrongNum (int input) {
      int start = sArmstrongs.Count == 0 ? 0 : sArmstrongs.Last () + 1;
      for (int i = start; ; i++) {
         if (sArmstrongs.Count > input) return sArmstrongs[input];
         if (!sArmstrongs.Contains (i) && IsArmstrong (i)) sArmstrongs.Add (i);
      }
   }
   static List<int> sArmstrongs = new ();

   /// <summary>Checks whether the given input number is an armstrong number or not</summary>
   public static bool IsArmstrong (int num) {
      int temp = num, sum = 0, d;
      int n = num.ToString ().Length;
      while (temp > 0) {
         d = temp % 10;
         temp = (temp - d) / 10;
         sum += (int)Math.Pow (d, n);
      }
      return num == sum;
   }
}