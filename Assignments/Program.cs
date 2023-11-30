// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A7 - Double Parse
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Academy;
public class A7 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Implementing double.TryParse ():-" + "\x1B[0m");
      List<string> testCases = new () {"1234", "+1234", "-1234", "&1234", "12.34", "+12.34", "-12.34",
         "00.34", ".34", "-0.34", "12.","0..34", "+-1234", "-12.3+4", "+-1234.00", "12.3r", "12,34.56", "-12.+34",
         "123e4", "-12.300e4", "1234.e-5", "e4", "0.e4", "12e+34", "+0.0e0", "12e3.4", "12..34", "-12-34e5", "12.3e+-4", "1e-3" };
      for (int i = 0; i <testCases.Count; i++) {
         var t = testCases[i];
         Console.Write ($"\n{i + 1,2}) {t,10}  ==> ");
         PrintResult (t);
      }

      ///<summary>Prints the double parsed result in the console page</summary>
      void PrintResult (string text) {
         var (integerPart, decimalPart, exponentialPart, result) = (0.0, 0.0, 0.0, 0.0);
         if (IsDouble (text)) {
            int sign = 1;
            // string[(string.IndexOf('c') + 1)..]
            //       ==> Gives the string part AFTER the 'c'. 
            // string.Remove(string.IndexOf('c'), string[(string.IndexOf('c') + 1)..].Length + 1)
            //       ==> Gives the string part BEFORE the 'c'.  ) 
            if (text.Contains ('e')) {
               exponentialPart += int.Parse (text[(text.IndexOf ('e') + 1)..]);
               text = text.Remove (text.IndexOf ('e'), text[(text.IndexOf ('e') + 1)..].Length + 1);
            }
            if (text.Contains ('.')) {
               if (text[0] == '-') sign *= -1;
               decimalPart += int.Parse (text[(text.IndexOf ('.') + 1)..]);
               decimalPart /= Math.Pow (10, text[(text.IndexOf ('.') + 1)..].Length);
               text = text.Remove (text.IndexOf ('.'), text[(text.IndexOf ('.') + 1)..].Length + 1);
            }
            integerPart += int.Parse (text);
            result = (integerPart < 0) ? (integerPart * -1 + decimalPart) * -1 * Math.Pow (10, exponentialPart)
                                       : (integerPart + decimalPart) * sign * Math.Pow (10, exponentialPart);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine (result);
            Console.ResetColor ();
         }  else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("INVALID");
            Console.ResetColor ();
         }
      }

      ///<summary>Returns true if input string is a valid double</summary>
      bool IsDouble (string word) {
         char[] numList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
         char[] symbolList = { '+', '-', '.', 'e' };  // List of allowed symbols.

         // If the string is null or of spaces, ELIMINATE.
         if (word == null || word.Trim ().Length == 0) return false;

         // If the string contains any character from OUT OF allowed symbol list or Number List, ELIMINATE.
         foreach (char c in word)
            if (!(numList.Contains (c) || symbolList.Contains (c))) return false;

         if (word.Contains ('e')) {
            string wordE = word[(word.IndexOf ('e') + 1)..];                    // Gives exponent part of the string.
            string wordI = word.Remove (word.IndexOf ('e'), wordE.Length + 1);  // Gives integer part of the string.

            // If the integer part has more than one '+' or '-', ELIMINATE.
            int count5 = wordI.Count (c => c == '+');
            int count6 = wordI.Count (c => c == '-');
            if (count5 > 1 || count6 > 1) return false;

            // If the sign symbol in integer part not in 0th index, ELIMINATE.
            if (wordI.Contains ('+'))
               if (word.IndexOf ('+') != 0) return false;
            if (wordI.Contains ('-'))
               if (word.IndexOf ('-') != 0) return false;

            // If the 'e' is followed by a decimal point, ELIMINATE.
            if (wordI.Contains ('.'))
               if (word.IndexOf ('e') == word.IndexOf ('.') + 1) return false;

            // If the exponent part has more than one '+' or '-', ELIMINATE.
            int count7 = wordE.Count (c => c == '+');
            int count8 = wordE.Count (c => c == '-');
            if (count7 > 1 || count8 > 1) return false;

            // If the sign symbol in exponent part not in 0th index, ELIMINATE.
            if (wordE.Contains ('+'))
               if (wordE.IndexOf ('+') != 0) return false;
            if (wordE.Contains ('-'))
               if (wordE.IndexOf ('-') != 0) return false;

            if (wordE.Contains ('.')) return false;  // If exponential part has decimal point, ELIMINATE.
            if (wordE.Length == 0) return false;     // If there is no character after 'e', ELIMINATE.
            if (wordI.Length == 0) return false;     // If the string starts from 'e', ELIMINATE.
         }

         // If there is no character after or before decimal point, ELIMINATE.
         if (word.Contains ('.')) {
            if (word[(word.IndexOf ('.') + 1)..].Length == 0) return false;
            if (word.Remove (word.IndexOf ('.'), word[(word.IndexOf ('.') + 1)..].Length + 1).Length == 0) return false;
         }

         // If the decimal point is present after the sign symbol, ELIMINATE
         if ((word.Contains ('+') || word.Contains ('-')) && word.Contains ('.'))
            if (word.IndexOf ('.') == (word.IndexOf ('+') + 1) || word.IndexOf ('.') == word.IndexOf ('+') + 1) return false;

         // If the string doesn't contain any number character, ELIMINATE.
         if (!numList.Any (word.Contains)) return false;

         // Counting '.', 'e', '+', '-'.
         int countDot = word.Count (c => c == '.');
         int countE = word.Count (c => c == 'e');
         int countPlus = word.Count (c => c == '+');
         int countMinus = word.Count (c => c == '-');

         // If the string contains more than one decimal point or 'e', ELIMINATE.
         if (countDot > 1 || countE > 1) return false;

         // If the string not having the exponential part, and contains the sign symbol anywhere in between, ELIMINATE.
         if (!word.Contains ('e')) {
            if (countPlus > 1 || countMinus > 1) return false; // If the integer part has more than one '+' or '-', ELIMINATE.
            if (word.Contains ('+') && word.IndexOf ('+') != 0) return false;
            if (word.Contains ('-') && word.IndexOf ('-') != 0) return false;
         }

         // If above all condition doesn't meet, return TRUE.
         return true;
      }
   }
}


