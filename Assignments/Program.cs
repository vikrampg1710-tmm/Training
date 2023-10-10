// ---------------------------------------------------------------------------------------
// T1 - Number Conversion Game
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;

public class T1 {
   public static void Main () {
      List<int> testCases = new () { 0, 1, 4, 9, 10, 11, 16, 25, 36, 49, 50, 81, 100, 109, 125, 216, 343, 729, 1000, 12001, 66000, 123450, 1000909, 987654321, 100000001 };
      Console.WriteLine ("\x1B[4m" + "Decimal to Binary & Hexadecimal:" + "\x1B[0m");
      for (int i = 0; i < testCases.Count; i++) {
         var item = testCases[i];
         Console.WriteLine ($"\n{i + 1}) {item}");
         PrintResult (item);
      }
      Console.Write ("\nTry Yourself! Enter input number: ");
      if (int.TryParse (Console.ReadLine (), out int num)) {
         PrintResult (num);
      }
   }

   /// <summary>Prints the input num in both binary and hexadecimal in the console page</summary>
   public static void PrintResult (int num) {
      Console.Write ("In Binary     : ");
      WriteInYellow (BinaryFormOf (num));
      Console.Write ("In Hexadecimal: ");
      WriteInYellow (HexaDecFormOf (num));
   }

   /// <summary>Converts input integer to a binary form</summary>
   public static string BinaryFormOf (int num) {
      if (num == 0) return "0";
      List<char> bin = new ();
      while (num > 0) {
         if (num % 2 == 0) bin.Add ('0');
         else bin.Add ('1');
         num /= 2;
      }
      return new string (Enumerable.Reverse (bin).ToArray ());
   }

   /// <summary>Converts input integer to a hexadecimal form</summary>
   public static string HexaDecFormOf (int num) {
      if (num == 0) return "0";
      List<char> hex = new ();
      while (num > 0) {
         int a = num % 16;
         if (a < 10) hex.Add (Convert.ToChar ($"{a}"));
         // THE ASCII value for the small letter alphabets starts with 97. So if a = 10, then (87 + a) = 'a' and so on.
         else hex.Add ((char)(87 + a)); 
         num /= 16;
      }
      return new string (Enumerable.Reverse(hex).ToArray());
   }

   /// <summary>Writes input string in console with foreground colour as yellow</summary>
   public static void WriteInYellow (string output) {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine (output);
      Console.ResetColor ();
   }
}

