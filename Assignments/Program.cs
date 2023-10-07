// ---------------------------------------------------------------------------------------
// T13 - Strong Password
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T15 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Check for Isogram Word:-" + "\x1B[0m");
      Console.WriteLine ("An isogram is a word that has no duplicate letters.");
      string output;
      List<string> testCases = new () { "apple", "banana", "cat", "doctrine", "elephant", "fruits", "pebble", "rubiks", "yellow", "zebra" };
      Console.WriteLine ();
      Console.WriteLine ("\x1B[4m" + "Test Cases:" + "\x1B[0m");
      for (int i = 0; i < testCases.Count; i++) {
         Console.Write ($"{i + 1}. {testCases[i]} - ");
         (output, Console.ForegroundColor) = IsIsogram (testCases[i]) ? ("Isogram", Green) : ("Not an Isogram", Red);
         Console.WriteLine ($"{output}");
         Console.ResetColor ();
      }
      Console.Write ("\nNow, let's enter a word: ");
      string input = Console.ReadLine ().ToUpper ();
      (output, Console.ForegroundColor) = IsIsogram (input) ? ("Isogram", Green) : ("Not an Isogram", Red);
      Console.WriteLine ($"{output}");
      Console.ResetColor ();
   }

   /// <summary>Returns true if input string is an isogram, otherwise false</summary>
   public static bool IsIsogram (string input) {
      input = input.ToUpper ();
      bool isIsogram = true;
      if (input.All (char.IsLetter)) {
         List<int> HashTable = new ();
         for (int j = 0; j < 26; j++) HashTable.Add (0);
         foreach (char c in input) {
            if (HashTable[c - 65] == 0) HashTable[c - 65]++;
            else isIsogram = false;
         }
      }
      return isIsogram;
   }
}
