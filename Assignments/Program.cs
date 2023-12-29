// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A7 - Double Parse
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using static Academy.A7.State;

namespace Academy;
public class A7 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Implementing double.Parse ():-" + "\x1B[0m");
      List<string> testCases = new () {"1234", "+1234", "-1234", "&1234", "12.34", "+12.34", "-12.34",
         "00.34", ".34", "-0.34", "12.","0..34", "+-1234", "-12.3+4", "+-1234.00", "12.3r", "12,34.56", "-12.+34",
         "123e4", "-12.300e4", "1234.e-5", "e4", "0.e4", "12e+34", "+0.0e0", "12e3.4", "12..34", "-12-34e5", "12.3e+-4", "1e-3" };
      for (int i = 0; i < testCases.Count; i++) {
         string t = testCases[i];
         double answer = DoubleParse (t);
         Console.Write ($"{i + 1,2}) {t,10}  ==> ");
         Console.ForegroundColor = !double.IsNaN (DoubleParse (t)) ? ConsoleColor.Green : ConsoleColor.Red;
         Console.WriteLine (answer);
         Console.ResetColor ();
      }
   }

   ///<summary>Returns true if given string is a valid double</summary>
   public static double DoubleParse (string input) {
      input = input.Trim () + "#";
      var (sign, esign, value, fraction, exponent) = (1, 1, 0.0, 0.1, 0.0);
      State st = A;
      foreach (char ch in input) {
         int d = ch - '0';
         switch (st, ch) {
            case (A, '+' or '-'): { st = B; sign = (ch == '-') ? -1 : 1; break; }
            case (A or B or C, >= '0' and <= '9'): { st = C; value = (value * 10) + d; break; }
            case (C, '.'): { st = D; break; }
            case (C or E, 'e' or 'E'): { st = F; break; }
            case (D or E, >= '0' and <= '9'): { st = E; value += d * fraction; fraction /= 10; break; }
            case (F, '+' or '-'): { st = G; esign = (ch == '-') ? -1 : 1; break; }
            case (F or G or H, >= '0' and <= '9'): { st = H; exponent = (exponent * 10) + d; break; }
            case (C or E or H, '#'): { st = I; break; }
            default: { st = Z; break; }
         };
      }
      return (st == I) ? sign * value * Math.Pow (10, esign * exponent) : double.NaN;
   }
   public enum State { A, B, C, D, E, F, G, H, I, Z };
}


