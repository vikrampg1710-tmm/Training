// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T16 - String Permutation
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

   /* Working of this method is explained as follows:
   1. Firstly, a char array named 'indexWord' is created and characters '0' to 'len - 1' is stored. (len = length of input string)
   Eg: if input string is a 3-letter word, then indexWord = [ '0', '1', '2' ].
   
   2. This array is converted to a string and all its output are generated using a local recursive method
   which works only for an isogram word, i.e. the word without any repeated characters in it.  The generated output of indexWord
   are stored in list 'indexPerms'.  Eg: if indexWord = ['0', '1', '2'], indexPerms = { 012, 021, 102, 120, 201, 210 }

   3. Now, each item is taken out from the above list one by one and applied to the input string to generate final permutated strings.
   Eg: if input string = "NOT"
                                        index value: 0     1     2
                                        chars      : N     O     T
   Permutations are { 012 ==> NOT, 021 ==> NTO, 102 ==> ONT, 120 ==> OTN, 201 ==> TNO, 210 ==> TON }
   
   4. For 4-letter input word, indexWord = [ '0', '1', '2', '3' ] and the further steps are same as follows.  Therefore, this way works for any length of word.
   
   5. Finally, the output strings are stored in a HashSet to avoid storing the duplicates.*/

   public static HashSet<string> PermutationsOf (string input) {
      input = input.ToUpper ();
      int len = input.Length;
      List<char> indexWord = new ();
      for (char i = '0'; indexWord.Count < len; i++)
         indexWord.Add (i);
      List<string> indexPerms = Permutated (new string (indexWord.ToArray ()));
      indexWord.Clear ();
      HashSet<string> output = new ();
      for (int i = 0; i < indexPerms.Count; i++) {
         for (int j = 0; j < len; j++) {
            int index = indexPerms[i][j] - '0';
            indexWord.Add (input[index]);
         }
         output.Add (new string (indexWord.ToArray ()));
         indexWord.Clear ();
      }
      return output;

      /// <summary>A local method which returns all the output of given isogram string</summary>
      static List<string> Permutated (string input) {
         int n = input.Length;
         if (n == 2) return new () { $"{input[0]}{input[1]}", $"{input[1]}{input[0]}" };
         List<string> store = new ();
         HashSet<string> output = new ();
         List<char> chars = new ();
         for (int i = 0; i < n; i++) {
            for (int j = 0, k = i; j < n - 1; j++, k++)
               chars.Add (input[k % n]);
            store.AddRange (Permutated (new string (chars.ToArray ())));
            chars.Clear ();
         }
         foreach (char c in input) {
            foreach (string s in store) {
               if (!s.Contains (c))
                  output.Add (c + s);
            }
         }
         return output.ToList ();
      }
   }
}