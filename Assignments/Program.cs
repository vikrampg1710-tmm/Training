// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T2 - NUMBER TO WORDS AND ROMAN NUMERALS CONVERTER 
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.ConsoleColor;

namespace Spark;

public class T2 {
   public static void Main () {
      List<int> wTestCases = new () { 0, 1, 4, 9, 10, 11, 16, 25, 36, 49, 50, 81, 100, 109, 125, 216,
                                    343, 729, 1000, 12001, 66000, 123450, 1000909, 987654321, 100000001 },
                rTestCases = new () { 0, 1, 4, 5, 9, 10, 47, 94, 100, 345, 400, 499, 500, 900, 998, 1000,
                                    1500, 1729, 1888, 3495, 4000, 3999 };
      Console.WriteLine ("\x1B[4m" + "Numbers to Words:" + "\x1B[0m");
      PrintResult (wTestCases, true);
      Console.WriteLine ("\n\x1B[4m" + "Numbers to Romans:" + "\x1B[0m");
      PrintResult (rTestCases, false);
   }

   /// <summary>Prints the Words or Roman numerals equivalent to the input number</summary>
   public static void PrintResult (List<int> input, bool toWords) {
      for (int i = 0; i < input.Count; i++) {
         int n = input[i];
         Console.Write ($"{i + 1,2}) {input[i],10} ==> ");
         if (!toWords && n > 3999) {
            Console.WriteLine ($"Invalid input!");
            continue;
         }
         Console.ForegroundColor = Green;
         Console.WriteLine (toWords ? ToWords (input[i]) : ToRomans (input[i]));
         Console.ResetColor ();
      }
   }

   /// <summary>Returns the input number in words using State Machine</summary>
   public static string ToWords (int num) {
      if (num == 0) return "Zero";
      string input = num.ToString ().PadLeft (9, '0'),
            output = "";
      List<string> ones = new () { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
      "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" },
                   tens = new () { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

      /*
       This method works for a maximum of 9-digit number, and its process is explained below:
      1. In the FIRST for-loop, input string of length 9 is segregated into 3 + 3 + 3 digits, each parsed into
         3 digit integer, and made run into the second for-loop.
         Eg: num = 123456, then input = "000123456" => part = { 000 (for i = 0); 123 (for i = 1); 456 (for i = 2) }
      
      2. In the SECOND for-loop, each single digits from 'part' stored in 'nthDigit' is taken out one by one and made
         run through the state machine.
         Eg: part = 123 => nthDigit = { 1 (for n = 3); 2 (for n = 2); 3 (for n = 1) }
             thirdDigit = 3 (3rd digit of part)

      3. The state machine starts with the state of HUNDREDS as the maximum place values of any 3-digit number is 100.
     
      Overall, the first loop segregates the input string into three 3-digit numbers and supplies to the second loop,
      which converts the supplied 3-digit number to equivalent words.  Finally all the words are added together and returned.
       */

      // FIRST for-loop:
      for (int i = 0; i < 3; i++) {
         int part = int.Parse (input[(i * 3)..((i + 1) * 3)]);
         if (part == 0) continue;
         State s = State.Hundreds;
         // SECOND for-loop:
         for (int n = 3; n > 0; n--) {
            int nthDigit = part / (int)Math.Pow (10, n - 1) % 10, thirdDigit = part % 10;
            switch (s, nthDigit) {
               case (State.Hundreds, >= 0 and <= 9): {
                     s = State.Tens;
                     if (nthDigit != 0) {
                        output += ones[nthDigit] + " Hundred ";
                        if (part / 10 % 10 > 0 || thirdDigit > 0) output += "and ";
                     }
                     break;
                  }
               case (State.Tens, 1): {
                     s = State.End;
                     output += ones[10 + thirdDigit] + " ";
                     break;
                  }
               case (State.Tens, 0 or (>= 2 and <= 9)): {
                     s = State.Ones;
                     if (nthDigit != 0) output += tens[nthDigit] + ((i == 2 && thirdDigit > 0) ? "-" : " ");
                     break;
                  }
               case (State.Ones, >= 0 and <= 9): {
                     s = State.End;
                     if (nthDigit != 0) output += ones[nthDigit] + " ";
                     break;
                  }
               default: { s = State.End; break; }
            }
         }
         if (part > 0 && i != 2) {
            int lastThreeDigit = num / 1000;
            var (cond, key) = (i == 0) ? (num / 1000 % 1000 > 0 || lastThreeDigit > 0, "Million") : (lastThreeDigit > 0, "Thousand");
            output += key + (cond ? ", " : " ");
         }
      }
      return output;
   }
   enum State { Hundreds, Tens, Ones, End };

   /// <summary>Converts input number to Roman numerals</summary>
   public static StringBuilder ToRomans (int num) {
      StringBuilder output = new ();
      if (num == 0) output.Append ("Zero has no symbol in Roman numerals system.");
      else {
         int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
         string[] romans = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
         int start = Array.IndexOf (values, Array.Find (values, a => a <= num));
         for (int i = start; i < values.Length && num != 0; i++) {
            int x = values[i], y = num / x;
            output.Append (string.Concat (Enumerable.Repeat (romans[i], y)));
            num %= x;
         }
      }
      return output;
   }
}