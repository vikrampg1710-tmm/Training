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
      Console.WriteLine ("\x1B[4m" + "Sorting & Swaping Special Characters:-" + "\x1B[0m");
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
   public static void PrintResult (char[] array, char c, bool sortOrder = true) {
      Console.Write ("Input char array   = ");
      WriteInColor ("[ " + string.Join (", ", array) + " ]", Yellow);
      Console.Write ("Special Character  = ");
      WriteInColor ($"{c}", Yellow);
      Console.Write ("Sort Order         = ");
      WriteInColor (sortOrder ? "Ascending" : "Descending", Yellow);
      if (array.Length == 0) {
         WriteInColor ("Invalid input!  Array is empty here", Red);
         return;
      }
      var output = SpeicalSorted (array, c, sortOrder);
      Console.Write ($"Output char array  = ");
      WriteInColor ("[ " + string.Join (", ", output) + " ]", Green);
      Console.WriteLine ();
   }

   /// <summary>Returns a char array sorted in the given order with special char at last</summary>
   public static char[] SpeicalSorted (char[] inputArray, char specialChar, bool sortOrder = true) {
      int count = 0;
      var list = inputArray.ToList ();
      list = sortOrder ? list.OrderBy (x => x).ToList () : list.OrderByDescending (x => x).ToList ();
      while (list.Contains (specialChar)) {
         list.Remove (specialChar);
         count++;
      }
      for (int i = 0; i < count; i++) list.Add (specialChar);
      return list.ToArray ();
   }

   /// <summary>Writes the given string in given foreground color</summary>
   public static void WriteInColor (string s, ConsoleColor color) {
      Console.ForegroundColor = color;
      Console.WriteLine (s);
      Console.ResetColor ();
   }
}
