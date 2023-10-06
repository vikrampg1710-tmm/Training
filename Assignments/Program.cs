// ---------------------------------------------------------------------------------------
// T1 - Number Conversion Game
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T1 {
   public static void Main () {
      List<int> testCases = new () { 0, 1, 4, 9, 10, 11, 16, 25, 36, 49, 50, 81, 100, 109, 125, 216, 343, 729, 1000, 12001, 66000, 123450, 1000909, 987654321, 100000001 };
      Console.WriteLine ("\x1B[4m" + "Decimal to Binary & Hexadecimal:" + "\x1B[0m");
      for (int i = 0; i < testCases.Count; i++) {
         var item = testCases[i];
         Console.WriteLine ($"\n{i + 1}) {item}: ");
         Console.Write ("Binary: ", 16);
         WriteInYellow (BinaryFormOf (item));
         Console.Write ("Hexadecimal: ", 16);
         WriteInYellow (HexaDecFormOf (item));
      }
      Console.Write ("\nTry Yourself! Enter input number: ");
      if (int.TryParse (Console.ReadLine (), out int num)) {
         Console.Write ("In Binary: ", 16);
         WriteInYellow (BinaryFormOf (num));
         Console.Write ("In Hexadecimal: ", 16);
         WriteInYellow (HexaDecFormOf (num));
      }
   }
   /// <summary>Converts input integer to a binary form</summary>
   public static string BinaryFormOf (int num) {
      if (num == 0) return "0";
      string s = "";
      while (num > 0) {
         s = ((num % 2 == 0) ? "0" : "1") + s;
         num /= 2;
      }
      return s;
   }

   /// <summary>Converts input integer to a hexadecimal form</summary>
   public static string HexaDecFormOf (int num) {
      if (num == 0) return "0";
      string s = "";
      while (num > 0) {
         int a = num % 16;
         s = ((a < 10) ? a : $"{(char)(87 + a)}") + s;
         num /= 16;
      }
      return s;
   }

   /// <summary>Writes input string in console with foreground colour as yellow</summary>
   public static void WriteInYellow (string output) {
      Console.WriteLine (output, Console.ForegroundColor = Yellow);
      Console.ResetColor ();
   }
}

