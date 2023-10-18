﻿// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T18 - The Armstrong Number
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;
public class T18 {
   public static void Main () {
      List<int> task1 = new () { 0, 1, 2, 5, 9, 10, 15, 20, 25, 27 },
                task2 = new () { 0, 1, 7, 10, 151, 370, 371, 372, 9474, 93085, 93084, 4210816, 4210818, 24678050, 24678051 };
      Console.WriteLine ("\x1B[4m" + "Nth Armstrong Number Computer:-" + "\x1B[0m");
      PerformTasks (task1, true);
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Armstrong Number Checker:-" + "\x1B[0m");
      PerformTasks (task2, false);
   }

   /// <summary>Print the results in the console page</summary>
   public static void PerformTasks (List<int> input, bool isTask1) {
      for (int i = 0; i < input.Count; i++) {
         int num = input[i];
         string th = (num > 3 || num == 0) ? "th" : (num == 1 ? "st" : (num == 2 ? "nd" : "rd"));
         Console.Write ($"{i + 1,2}. {num}");
         if (isTask1) {
            Console.Write ($"{th} Armstrong Number = ");
            Console.ForegroundColor = Green;
            Console.WriteLine (NthArmstrongNum (num));
            Console.ResetColor ();
         }
         else {
            Console.Write (" - ");
            (bool, int?) pair = IsArmsNum (num);
            (string answer, Console.ForegroundColor) = pair.Item1 ? ($"{pair.Item2}{th} Armstrong Number", Green) : ("Not an Armstrong Number", Red);
            Console.WriteLine (answer);
            Console.ResetColor ();
         }
      }
   }

   /// <summary>Returns the Nth Armstrong number of given input</summary>
   public static int NthArmstrongNum (int input) {
      int nCount = 0;
      for (int i = 0; nCount <= input; i++) {
         if (IsArmsNum (i).Item1) {
            if (!sArmstrong.Contains (i)) sArmstrong.Add (i);
            nCount++;
         }
         if (nCount == input) return i + 1;
      }

      return 0;
   }

   /// <summary>Checks wherether the given input number is an armstrong number or not</summary>
   public static (bool, int?) IsArmsNum (int num) {
      int temp = num, sum = 0, d;
      int n = num.ToString ().Length;
      while (temp > 0) {
         d = temp % 10;
         temp = (temp - d) / 10;
         sum += (int)Math.Pow (d, n);
      }
      return sum == num ? (true, sArmstrong.IndexOf (num)) : (false, null);
   }
   static List<int> sArmstrong = new ();
}


