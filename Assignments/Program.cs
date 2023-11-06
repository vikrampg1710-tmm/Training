﻿// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T23 - The Chocolate Wrapper
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;
public class T23 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Chocolate Wrappers Problem:-" + "\x1B[0m");
      List<(int givenMoney, int priceOfChocolate, int reqWrappers)> testCases = new () { (15, 4, 3), (20, 3, 5), (100, 10, 4), (1, 1, 1), (0, 5, 1) };
      for (int i = 0; i < testCases.Count; i++) {
         PrintInColor ($"\r\nTest Case - {i + 1}:", Yellow);
         PrintResult (testCases[i]);
      }
      Console.Write ("\r\nNow, let's try out.\r\nEnter the total money given = ");
      int money = Convert.ToInt32 (Console.ReadLine ());
      Console.Write ("Enter the price of chocolate = ");
      int price = Convert.ToInt32 (Console.ReadLine ());
      Console.Write ("Enter the required number of wrappers for a chocolate exchange = ");
      int wrappersNeeded = Convert.ToInt32 (Console.ReadLine ());
      PrintResult ((money, price, wrappersNeeded));
   }

   /// <summary>Print the results in the console page</summary>
   public static void PrintResult ((int, int, int) input) {
      if (input.Item3 < 2) { PrintInColor ("Hypothetical input!", Red); return; }
      var (c, r, w) = ComputeResult (input.Item1, input.Item2, input.Item3);
      Console.Write ("(Given money, Chocolate price, Wrapper needed) = ");
      PrintInColor ($"({input.Item1}, {input.Item2}, {input.Item3})", Cyan);
      Console.Write ("(No of Chocolates, Money left, Wrappers left)  = ");
      PrintInColor ($"({c}, {r}, {w})", Green);
   }

   /// <summary>Returns the answer to the Chocolate Wrappers Problem</summary>
   public static (int c, int r, int w) ComputeResult (int x, int p, int w) {
      int nChocolates = 0, remPrice = 0, nWrappers = 0;
      nChocolates += x / p;
      remPrice += x % p;
      nWrappers += nChocolates;

      while (nWrappers >= w) {
         int a = nWrappers / w;
         nWrappers %= w;
         nChocolates += a;
         nWrappers += a;
      }
      return (nChocolates, remPrice, nWrappers);
   }

   /// <summary>Prints the given string in given foreground color</summary>
   public static void PrintInColor (string s, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (s);
      Console.ResetColor ();
   }
}

