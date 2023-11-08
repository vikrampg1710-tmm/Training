// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// --------------------------------------------------------------------------------------
// Program.cs
// T20 - Digit Segregator
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;
public class T20 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Digit Segregator:-" + "\x1B[0m");
      List<double> wTestCases = new () { 1, 23, 1234, 5678.345, 0.789, -2.60, -0.0123, -56.90, -0.98723, 0.005 };
      for (int i = 0; i < wTestCases.Count; i++) {
         var item = wTestCases[i];
         Console.Write ($"{i + 1}) ");
         PrintResult (item);
         Console.WriteLine ();
      }
      Console.Write ("Now, lets try entering a number: ");
      if (double.TryParse (Console.ReadLine (), out var d)) PrintResult (d);
      else WriteInColor ("Incorrect input!", Red);
   }

   /// <summary>Prints the results of segregated digits of given input</summary>
   public static void PrintResult (double input) {
      WriteInColor (input.ToString(), Yellow);
      var answer = DigitParse (input);
      Console.Write ("Sign: ");
      WriteInColor ($"{answer.Item1}", Green);
      Console.Write ("Integer part: ");
      WriteInColor ($"{answer.Item2}", Green);
      Console.Write ("Decimal part: ");
      WriteInColor ($"{answer.Item3}", Green);
   }

   /// <summary>Returns integer part and decimal part from given input number</summary>
   public static (string, string, string) DigitParse (double num) {
      bool sign = num < 0;
      if (sign) num *= -1;
      string input = num.ToString ();
      int index = input.IndexOf ('.');
      string integerPart = Math.Truncate (num).ToString (),
             decimalPart = index != -1 ? input[(index + 1)..] : "";
      return (sign ? "-" : "+", integerPart, decimalPart);
   }

   /// <summary> Write the input string in specified foreground color</summary>
   public static void WriteInColor (string output, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (output);
      Console.ResetColor ();
   }
}


