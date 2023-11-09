// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T17 - String Permutation
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark;
public class T17 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "String Permutations Computer:-" + "\x1B[0m");
      List<string> words = new () { "or", "not", "tat", "abcd", "abca" };
      foreach (string word in words) PrintResult (word);
      GetUserInput: Console.Write ("\r\nNow, let's try entering a word: ");
      string input = Console.ReadLine ();
      if (input.Trim ().Length < 2) {
         Console.WriteLine ("Please enter a string with a minimum length of 2.");
         goto GetUserInput;
      }
      PrintResult (input);
   }

   /// <summary> Prints the permutated strings of given input word</summary>
   public static void PrintResult (string input) {
      if (input.Length > 7) Console.WriteLine ("Loading...");
      Console.WriteLine ();
      Console.WriteLine ($"Input word: {input}");
      var answer = PermutationsOf (input);
      int count = 0;
      foreach (var item in answer) {
         Console.Write ($"{++count,3}. ");
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine (item);
         Console.ResetColor ();
      }
   }

   /// <summary>A recursive method which returns all the possible permutations of given input string of any length</summary>
   public static List<string> PermutationsOf (string input) {
      int n = input.Length;
      input = input.ToUpper ();
      if (n == 2) return new () { $"{input[0]}{input[1]}", $"{input[1]}{input[0]}" };
      HashSet<string> output = new ();
      List<string> temp = new ();
      List<char> chars = new ();
      for (int i = 0; i < n; i++) {
         for (int j = i; j < n - 1 + i; j++)
            chars.Add (input[j % n]);
         temp.AddRange (PermutationsOf (new string (chars.ToArray ())));
         chars.Clear ();
      }
      foreach (char c in input) {
         foreach (string s in temp) {
            string perm = c + s;
            if (input.OrderBy (c => c).SequenceEqual (perm.OrderBy (c => c)))
               output.Add (perm);
         }
      }
      return output.ToList ();
   }
}