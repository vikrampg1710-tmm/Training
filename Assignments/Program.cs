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
      List<string> words = new () { "or", "not" };
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
      input = input.ToUpper ();
      if (input.Length == 2) return Permutated2 (input);
      return Permutated34 (input);

      /// <summary>Permutates given 2-letter word</summary>
      static List<string> Permutated2 (string input)
         => new () { $"{input[0]}{input[1]}", $"{input[1]}{input[0]}" };

      /// <summary>Permutates given 3 [or] 4-letter word</summary>
      static List<string> Permutated34 (string input) {
         int n = input.Length;
         List<string> output = new (),
                       store = new ();
         if (n == 3) {
            for (int i = 0; i < 3; i++)
               store.AddRange (Permutated2 ($"{input[i]}{input[(i + 1) % n]}"));
         } else {
            for (int i = 0; i < 4; i++)
               store.AddRange (Permutated34 ($"{input[i]}{input[(i + 1) % 4]}{input[(i + 2) % 4]}"));
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
}




