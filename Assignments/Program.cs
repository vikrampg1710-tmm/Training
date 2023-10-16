// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T16 - String Permutation
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Spark;
public class T17 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "String Permutator:-" + "\x1B[0m");
      List<string> words = new () { "or", "not", "abcd" };
      foreach (string word in words) PrintResult (word);
      Console.WriteLine ();
      Console.Write ("Now, let's try entering a word: ");
      PrintResult (Console.ReadLine ());
   }

   /// <summary> Prints the perumuted strings of given input word</summary>
   public static void PrintResult (string input) {
      Console.WriteLine ();
      Console.WriteLine ($"Input word: {input}");
      var answer = Permutated (input);
      for (int i = 0; i < answer.Count; i++) {
         Console.Write ($"{i + 1,2}. ");
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine (answer[i]);
         Console.ResetColor ();
      }
   }

   /// <summary>Returns the list of permutated strings of given input word</summary>
   public static List<string> Permutated (string input) {
      int n = input.Length;
      input = input.ToUpper ();
      List<string> output = new (), store = new ();
      if (n == 2) return new () { $"{input[0]}{input[1]}", $"{input[1]}{input[0]}" };
      else if (n == 3) {
         for (int i = 0; i < n; i++)
            store.AddRange (Permutated ($"{input[i]}{input[(i + 1) % n]}"));
      } else {
         for (int i = 0; i < n; i++)
            store.AddRange (Permutated ($"{input[i]}{input[(i + 1) % n]}{input[(i + 2) % n]}"));
      }
      foreach (char c in input) {
         foreach (string s in store) {
            if (!s.Contains (c)) {
               string a = c + s, b = s + c;
               if (!output.Contains (a)) output.Add (a);
               if (!output.Contains (b)) output.Add (b);
            }
         }
      }
      return output;
   }
}