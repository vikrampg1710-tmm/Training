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
      List<string> wTestCases = new () { "1", "23", "1234", "5678.345", "0.789", "-2.60", "-0.0123", "-56.90", "-0.98723", "0.005" };
      for (int i = 0; i < wTestCases.Count; i++) {
         var item = wTestCases[i];
         Console.Write ($"{i + 1}) ");
         PrintResult (item);
         Console.WriteLine ();
      }
      Console.Write ("Now, lets try entering a number: ");
      string input = Console.ReadLine ();
      if (double.TryParse(input, out var _)) PrintResult (input);
      else WriteLine ("Program terminated due to incorrect input!", Red);
   }

   /// <summary>Prints the results of segregated digits of given input</summary>
   public static void PrintResult (string input) {
      WriteLine ($"{input}", Yellow);
      var ans = DigitParse (input);
      Console.Write ("Sign: ", 15);
      WriteLine ($"{ans.Item1}", Green);
      Console.Write ("Integer part: ", 15);
      WriteLine ($"{ans.Item2}", Green);
      Console.Write ("Decimal part: ", 15);
      WriteLine ($"{ans.Item3}", Green);
   }

   /// <summary>Digit Parser (using State Machine)</summary>
   public static Tuple<string, string, string> DigitParse (string input) {
      string i = "", d = "";
      bool sign = true;
      State st = State.A;
      foreach (char ch in input + "#") {
         switch (st, ch) {
            case (State.A, '+' or '-'): { st = State.B; sign = ch != '-'; break; }
            case (State.A or State.B or State.C, >= '0' and <= '9'): { st = State.C; i += ch.ToString (); break; }
            case (State.C, '.'): { st = State.D; break; }
            case (State.D or State.E, >= '0' and <= '9'): { st = State.E; d += ch.ToString (); break; }
            case (State.C or State.E, '#'): { st = State.H; break; }
            default: { st = State.H; break; }
         }
      }
      string s;
      (s, i, d) = ((sign ? "+" : "-"), (i == "" ? "0" : i), (d == "" ? "0" : d));

      Tuple<string, string, string> output = new (s, i, d);
      return output;
   }
   enum State { A, B, C, D, E, F, G, H, I, Z };

   /// <summary> Write the input string in specified foreground color</summary>
   public static void WriteLine (string output, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (output);
      Console.ResetColor ();
   }
}


