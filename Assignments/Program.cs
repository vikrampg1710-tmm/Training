// ---------------------------------------------------------------------------------------
// T2 - NUMBER TO WORDS AND ROMAN NUMERALS CONVERTER 
// ---------------------------------------------------------------------------------------

using System;
using System.Globalization;
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
      Console.WriteLine ("\n\x1B[4m" + "Numbers to Romans:" + "\x1B[0m");
      for (int i = 0; i < rTestCases.Count; i++) {
         var item = rTestCases[i];
         Console.Write ($"{i + 1,2}) {item,4} ==> ");
         WriteInYellow (ToRomans (item));
         Console.ResetColor ();
      }
   }

   /// <summary>Converts input number to equivalent words</summary>
   public static string ToWords (int num) {
      if (num == 0) return "Zero";
      string input = num.ToString ().PadLeft (9, '0'),
            output = "";
      List<string> ones = new () { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
      "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" },
                   tens = new () { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
      for (int i = 0; i < 3; i++) {
         int part = int.Parse (input.Substring (i * 3, 3));
         State s = State.A;
         for (int loop = 3; loop > 0; loop--) {
            int n = part / (int)Math.Pow (10, loop - 1) % 10, d3 = part % 10;
            switch (s, n) {
               case (State.A, >= 0 and <= 9): {
                     s = State.B;
                     if (n != 0) {
                        output += ones[n] + " Hundred ";
                        if (part / 10 % 10 > 0 || d3 > 0) output += "and ";
                     }
                     break;
                  }
               case (State.B, 1): {
                     s = State.D;
                     output += ones[10 + d3] + " ";
                     break;
                  }
               case (State.B, 0 or (>= 2 and <= 9)): {
                     s = State.C;
                     if (n != 0) output += tens[n] + ((i == 2 && d3 > 0) ? "-" : " ");
                     break;
                  }
               case (State.C, >= 0 and <= 9): {
                     s = State.D;
                     if (n != 0) output += ones[n] + " ";
                     break;
                  }
               default: { s = State.D; break; }
            }
         }
         if (part > 0 && i != 2) {
            var (cond, key) = (i == 0) ? (num / 1000 % 1000 > 0 || num % 1000 > 0, "Million") : (num % 1000 > 0, "Thousand");
            output += key + (cond ? ", " : " ");
         }
      }
      return output;
   }
   enum State { A, B, C, D };
   #region State Machine Diagram for converting any 3-digit Number to Words:
   /*
    *                                  if (n == 0) -->                          if (n == 0) --->                         if (n == 0) --->
    *                             ┌------------>----------┐                ┌----------->-----------┐                ┌----------->-----------┐
    *                             ↑                       ↓                ↑                       ↓                ↑                       ↓
    *                             │    else --------->    │                │    else ---------->   │                │    else ---------->   │
    *  o---->----A(n = 0...9)-->--⬛--->---[action1]--->---B(n = 0...9)-->--⬛--->---[action3]--->---C(n = 0...9)-->--⬛--->---[action4]--->---⬛-----D       
    *                                                                      │                                                                │    
    *                                                                      ↓     if (n == 1) -->                                            ↑
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
      Console.WriteLine (output, Console.ForegroundColor = Yellow);
      Console.ResetColor ();
   }
}


