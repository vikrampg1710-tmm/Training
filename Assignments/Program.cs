// ---------------------------------------------------------------------------------------
// T2 - NUMBER TO WORDS AND ROMAN NUMERALS CONVERTER 
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T2 {
   public static void Main () {
      List<int> wTestCases = new () { 0, 1, 4, 9, 10, 11, 16, 25, 36, 49, 50, 81, 100, 109, 125, 216, 343, 729, 1000, 12001, 66000, 123450, 1000909, 987654321, 100000001 };
      List<int> rTestCases = new () { 1, 4, 5, 9, 10, 47, 94, 100, 345, 400, 499, 500, 900, 998, 1000, 1500, 1729, 1888, 3495, 3913, 4000, 4096, 4509, 4761, 4999 };
      Console.WriteLine ("\x1B[4m" + "Numbers to Words:" + "\x1B[0m");
      for (int i = 0; i < wTestCases.Count; i++) {
         var item = wTestCases[i];
         Console.Write ($"{i + 1,2}) {item,9} ==> ");
         WriteInYellow (ToWords (item));
         Console.ResetColor ();
      }
      Console.WriteLine ("\nthDigit\x1B[4m" + "Numbers to Romans:" + "\x1B[0m");
      for (int i = 0; i < rTestCases.Count; i++) {
         var item = rTestCases[i];
         Console.Write ($"{i + 1,2}) {item,4} ==> ");
         WriteInYellow (ToRomans (item));
         Console.ResetColor ();
      }
   }

   /// <summary>Retuns the input number in words using State Machine</summary>
   public static string ToWords (int num) {
      if (num == 0) return "Zero";
      string input = num.ToString ().PadLeft (9, '0'),
            output = "";
      List<string> ones = new () { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
      "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" },
                   tens = new () { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

      // As this methods works till 9-digit number, this first loop segregates the input (string) into 3 + 3 + 3 digits, and parse into integer.
      for (int i = 0; i < 3; i++) {
         // Here part is FIRST 3-digits for (i = 0), SECOND 3-digits for (i = 1), and LAST 3-digits for (i = 2).
         // Eg: num = 123456 ==> input = 000123456.  Then, part = 000 (for i = 0); 123 (for i = 1); 456 (for i = 2).
         int part = int.Parse (input.Substring (i * 3, 3));
         // For any 3-digit number, the place value of first digit is always Hundreds.  So the state s always starts with Hundreds.
         State s = State.Hundreds;
         // Below loop is used to take out digits ONE BY ONE from part and converted into words.
         for (int n = 3; n > 0; n--) {
            // nthDigit is the 3rd digit of part for (n = 3), and so on.
            // onesDigit is the always the 3rd digit of part.
            int nthDigit = part / (int)Math.Pow (10, n - 1) % 10, onesDigit = part % 10;
            // The below switch cases is implemented based on the STATE MACHINE DIAGRAM attached.
            switch (s, nthDigit) {
               case (State.Hundreds, >= 0 and <= 9): {
                     s = State.Tens;
                     if (nthDigit != 0) {
                        output += ones[nthDigit] + " Hundred ";
                        if (part / 10 % 10 > 0 || onesDigit > 0) output += "and ";
                     }
                     break;
                  }
               case (State.Tens, 1): {
                     s = State.End;
                     output += ones[10 + onesDigit] + " ";
                     break;
                  }
               case (State.Tens, 0 or (>= 2 and <= 9)): {
                     s = State.Ones;
                     if (nthDigit != 0) output += tens[nthDigit] + ((i == 2 && onesDigit > 0) ? "-" : " ");
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
            var (cond, key) = (i == 0) ? (num / 1000 % 1000 > 0 || num % 1000 > 0, "Million") : (num % 1000 > 0, "Thousand");
            output += key + (cond ? ", " : " ");
         }
      }
      return output;
   }
   enum State { Hundreds, Tens, Ones, End };
   #region State Machine Diagram for converting any 3-digit Number to Words:
   /*
    *                                  if (nthDigit == 0) -->                          if (nthDigit == 0) --->                         if (nthDigit == 0) --->
    *                             ┌------------>----------┐                ┌----------->-----------┐                ┌----------->-----------┐
    *                             ↑                       ↓                ↑                       ↓                ↑                       ↓
    *                             │    else --------->    │                │    else ---------->   │                │    else ---------->   │
    *  o---->----Hundreds(nthDigit = 0...9)-->--⬛--->---[action1]--->---Tens(nthDigit = 0...9)-->--⬛--->---[action3]--->---Ones(nthDigit = 0...9)-->--⬛--->---[action4]--->---⬛-----End       
    *                                                                      │                                                                │    
    *                                                                      ↓     if (nthDigit == 1) -->                                            ↑
    *                                                                      └----------->---------------[action2]---------------->-----------┘
    *  
    *  Where,
    *    action1: output += (one/two/.../eight/nine) + "Hundred and"
    *    action2: output += (eleven/twelve/.../eighteen/nineteen)
    *    action3: output += (ten/eleven/.../eighty/ninety)
    *    action4: output += (one/two/.../eight/nine)
    */
   #endregion

   /// <summary>Converts input number to Roman Numerals</summary>
   public static string ToRomans (int num) {
      string output = "";
      var romans = new char[7] { 'M', 'D', 'C', 'L', 'X', 'V', 'I' };
      for (int i = 0, r = 1000; i < 7; i++) {
         output += new string (romans[i], num / r);
         num %= r;
         r /= (i % 2 == 0) ? 2 : 5;
      }
      return output.Replace ("VIIII", "IX").Replace ("IIII", "IV").Replace ("LXXXX", "XC").Replace ("XXXX", "XL").Replace ("DCCCC", "CM").Replace ("CCCC", "CD");
   }

   public static void WriteInYellow (string output) {
      Console.ForegroundColor = Yellow;
      Console.WriteLine (output);
      Console.ResetColor ();
   }
}


