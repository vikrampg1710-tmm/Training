// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T25 - Sort & Swap Special Characters
// ---------------------------------------------------------------------------------------

using System;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T25 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Sorting & Swapping Special Characters:-" + "\x1B[0m");
      WriteInColor ("Test Case - 1:", Cyan);
      PrintResult (new char[] { 'a', 'b', 'c', 'a', 'c', 'b', 'd' }, 'a', false);
      WriteInColor ("Test Case - 2:", Cyan);
      PrintResult (new char[] { 'z', 'y', 'x' }, 'a');
      WriteInColor ("Test Case - 3:", Cyan);
      PrintResult (new char[] { 'j', 'w', 'q', 'o', 'f', 'g', 'a', 'j', 'j', 'b', 'd' }, 'j', true);
      WriteInColor ("Test Case - 4:", Cyan);
      PrintResult (new char[] { 'j', 'w', 'q', 'o', 'f', 'g', 'a', 'j', 'j', 'b', 'd' }, 'j', false);
      WriteInColor ("Test Case - 5:", Cyan);
      PrintResult (Array.Empty<char> (), 'a');
   }

   /// <summary>Prints the result in the console page</summary>
   public static void PrintResult (char[] inputArray, char specialChar, bool sortOrder = true) {
      Console.Write ("Input char array   = ");
      WriteInColor ("[ " + string.Join (", ", inputArray) + " ]", Yellow);
      Console.Write ("Special Character  = ");
      WriteInColor ($"{specialChar}", Yellow);
      Console.Write ("Sort Order         = ");
      WriteInColor (sortOrder ? "Ascending" : "Descending", Yellow);
      var output = SpecialSorted (inputArray, specialChar, sortOrder);
      Console.Write ($"Output char array  = ");
      WriteInColor ("[ " + string.Join (", ", output) + " ]", Green);
      Console.WriteLine ();
   }

   /// <summary>Returns a char array sorted in the given order with special char at last</summary>
   public static char[] SpecialSorted (char[] inputArray, char specialChar, bool sortOrder = true) {
      if (inputArray.Length == 0) throw new Exception ("The input char array must not be empty.");
      if (!inputArray.All (char.IsAsciiLetter)) throw new Exception ("The items in the char array must be a alphabet letter");
      if (!char.IsAsciiLetter (specialChar)) throw new Exception ("The special char must be a alphabet letter.");
      var list = inputArray.Where (a => a != specialChar);
      list = sortOrder ? list.OrderBy (x => x) : list.OrderByDescending (x => x);
      return list.Concat (Enumerable.Repeat (specialChar, inputArray.Length - list.Count ())).ToArray ();
   }

   /// <summary>Writes the given string in given foreground color</summary>
   public static void WriteInColor (string s, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (s);
      Console.ResetColor ();
   }
}
