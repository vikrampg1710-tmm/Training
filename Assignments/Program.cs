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
      Console.WriteLine ("Program to check whether a given string is a valid double or not:\n");
      double N1;  // To store integer.
      double N2;  // To store decimal part.
      double N3;  // To store exponent part.
      char[] NumList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      char[] SymbolList = { '+', '-', '.', 'e' };  // List of allowed symbols.

      // Test Cases.
      List<string> TestCases = new () {"1234", "+1234", "-1234", "&1234", "12.34", "+12.34", "-12.34",
         "00.34", ".34", "-0.34", "12.", "+-1234", "-12.3+4", "+-1234.00", "12.3r", "12,34.56", "-12.+34",
         "123e4", "-12.300e4", "1234.e-5", "e4", "0.e4", "12e+34", "+0.0e0", "12e3.4", "12..34", "-12-34e5", "12.3e+-4" };

      // Reading user choice:
      Console.Write ("Do you want to enter (I)nput string [or] see the (T)est cases result: ");
      char choice = char.ToUpper (Console.ReadKey ().KeyChar);
      Console.WriteLine ();

      if (choice == 'I') {
         Console.Write ("Enter your string: ");
         string Input = Console.ReadLine ();
         Perform (Input);
         Console.WriteLine ();
      } else {
         int count = 1;
         foreach (string input in TestCases) {
            Console.Write ($"\n{count++, 2}) {input, 10}  ==> ");
            Perform (input);
         }
      }

      // Function to perform and print the double parsed string.
      void Perform (string text) {
         // Resetting values of above initiated variables and also the Foreground color.
         N1 = 0;
         N2 = 0;
         N3 = 0;
         Console.ResetColor ();

         // If the string passes the IsValid function, print the parsed value.
         if (IsValid (text)) {
            // string[(string.IndexOf('c') + 1)..] ==> Gives the string part AFTER the 'c'. 
            // string.Remove(string.IndexOf('c'), string[(string.IndexOf('c') + 1)..].Length + 1) ==> Gives the string part BEFORE the 'c'.  ) 
            if (text.Contains ('e')) {
               N3 += IntegerOf (text[(text.IndexOf ('e') + 1)..]);
               text = text.Remove (text.IndexOf ('e'), text[(text.IndexOf ('e') + 1)..].Length + 1);
            }
            if (text.Contains ('.')) {
               N2 += IntegerOf (text[(text.IndexOf ('.') + 1)..]);
               N2 /= Math.Pow (10, text[(text.IndexOf ('.') + 1)..].Length);
               text = text.Remove (text.IndexOf ('.'), text[(text.IndexOf ('.') + 1)..].Length + 1);
            }
            N1 += IntegerOf (text);
            Console.ForegroundColor = ConsoleColor.Green;
            if (N1 < 0) Console.WriteLine ($"VALID.  Value = {(N1 * -1 + N2) * -1 * Math.Pow (10, N3)}");
            else Console.WriteLine ($"VALID.  Value = {(N1 + N2) * Math.Pow (10, N3)}");
            Console.ResetColor ();
         }
         // Else, print INVALID.
         else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("INVALID!");
            Console.ResetColor ();
         }
      }

      // Function to check whether a input string can be double parsed or not.
      bool IsValid (string word) {

         // If the string is null or spaces, ELIMINATE.
         if (word == null || word.Trim ().Length == 0) return false;

         // If the string contains any character from OUT OF allowed symbol list or Number List, ELIMINATE.
         foreach (char c in word)
            if (!(NumList.Contains (c) || SymbolList.Contains (c))) return false;

         if (word.Contains ('e')) {

            string wordE = word[(word.IndexOf ('e') + 1)..];  // Gives exponent part of the string.
            string wordi = word.Remove (word.IndexOf ('e'), wordE.Length + 1);  // Gives integer part of the string.

            // If the integer part has more than one '+' or '-', ELIMINATE.
            int count5 = wordi.Count (c => c == '+');
            int count6 = wordi.Count (c => c == '-');
            if (count5 > 1 || count6 > 1) return false;

            // If the sign symbol in integer part not in 0th index, ELIMINATE.
            if (wordi.Contains ('+'))
               if (word.IndexOf ('+') != 0) return false;
            if (wordi.Contains ('-'))
               if (word.IndexOf ('-') != 0) return false;

            // If the 'e' is followed by a decimal point, ELIMINATE.
            if (wordi.Contains ('.'))
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
            if (wordi.Length == 0) return false;  // If the string starts from 'e', ELIMINATE.
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
         if (!NumList.Any (word.Contains)) return false;

         // Counting '.', 'e', '+', '-'.
         int count1 = word.Count (c => c == '.');
         int count2 = word.Count (c => c == 'e');
         int count3 = word.Count (c => c == '+');
         int count4 = word.Count (c => c == '-');

         // If the string contains more than one decimal point or 'e', ELIMINATE.
         if (count1 > 1 || count2 > 1) return false;

         if (!word.Contains ('e')) {
            if (word.Contains ('+') && (word.IndexOf ('+') != 0)) return false;
            if (word.Contains ('-') && (word.IndexOf ('-') != 0)) return false;
            if (count3 > 1 || count4 > 1) return false; // If the integer part has more than one '+' or '-', ELIMINATE.
         }

         // If above all condition doesn't meet, return TRUE.
         return true;
      }

      // A simple function to int-parse from input string.
      double IntegerOf (string word) {
         bool sign = true;
         double N = 0;
         if (word[0] == '-') { sign = false; word = word[1..]; } else if (word[0] == '+') word = word[1..];
         foreach (char c in word) {
            if (c != '.') {
               for (int j = 0; j < NumList.Length; j++) {
                  if (c == NumList[j]) N = N * 10 + j;
               }
            } else break;
         }
         if (!sign) N *= -1;
         return N;
      }
   }
}


