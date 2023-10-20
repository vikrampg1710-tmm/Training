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
      Console.WriteLine ("\x1B[4m" + "String Permutations Computer:-" + "\x1B[0m");
      List<string> words = new () { "or", "not", "tat", "abcd", "abca" };
      foreach (string word in words) PrintResult (word);
      Console.WriteLine ();
      Console.Write ("Now, let's try entering a word: ");
      PrintResult (Console.ReadLine ());
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

   /// <summary>Returns the list of permutated strings of given string</summary>
   public static HashSet<string> PermutationsOf (string input) {
      int len = input.Length;
      HashSet<string> permutations = new ();
      List<char> chars = new ();
      for (int i = 0; i < len; i++)
         chars.Add ((char)(48 + i));
      var output = Permutated (new string (chars.ToArray ()));
      chars.Clear ();
      for (int i = 0; i < output.Count; i++) {
         for (int j = 0; j < len; j++) {
            int index = int.Parse ($"{output[i][j]}");
            chars.Add (input[index]);
         }
         permutations.Add (new string (chars.ToArray ()));
         chars.Clear ();
      }
      return permutations;

      /// <summary>A local method which returns all the permutations of given isogram string</summary>
      static List<string> Permutated (string input) {
         int n = input.Length;
         input = input.ToUpper ();
         if (n == 2) return new () { $"{input[0]}{input[1]}", $"{input[1]}{input[0]}" };
         List<string> output = new (), store = new ();
         List<char> chars = new ();
         for (int i = 0; i < n; i++) {
            for (int j = 0, k = i; j < n - 1; j++) {
               chars.Add (input[k % n]);
               k += 1;
            }
            store.AddRange (Permutated (new string (chars.ToArray ())));
            chars.Clear ();
         }
         foreach (char c in input) {
            foreach (string s in store) {
               if (!s.Contains (c)) {
                  string a = c + s;
                  if (!output.Contains (a)) output.Add (a);
               }
            }
         }
         return output;
      }
   }
}