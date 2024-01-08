// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A3 - Spelling Bee
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Academy;
public class A3 {
   public static void Main () {
      Console.OutputEncoding = Encoding.Unicode;
      Console.WriteLine ("----------------------------------------------------------------------------------------------------------\n"
      + " Spelling Bee:-\r\n"
      + " ◌ Spelling Bee is a game in which we have to find possible words from given 7 letters, among 1 is a compulsory letter.\r\n"
      + " ◌ A word should atleast contain 4 letters, which scores 1 point. Further, N letter word scores N points.\r\n"
      + " ◌ If a word contains all the given 7 letters called as PANGRAM, which score additional 7 points.\r\n"
      + "----------------------------------------------------------------------------------------------------------\r\n");
      List<char> input = new () { 'U', 'X', 'A', 'T', 'L', 'N', 'E' };
      Console.Write ("(1) Input the 7 letters  [or]\r\n" +
         "(2) See the results of default input letters: [ 'U', X, A, T, L, N, E ]\r\n" +
         "Press 1 or 2: ");
      if (Console.ReadKey ().KeyChar == '1') input = GetInput ();
      Console.WriteLine ();
      PrintResult (input);

      ///<summary>Gets the 7 letters as input from user</summary>
      static List<char> GetInput () {
         List<char> input = new ();
         Console.WriteLine ("\r\n(Note: Letter 1 is the compulsory letter.)");
         GetAgain: Console.Write ($"Enter letter {input.Count + 1}: ");
         char letter = char.ToUpper (Console.ReadLine ()[0]);
         if (char.IsLetter (letter) && !input.Contains (letter)) input.Add (letter);
         else Console.WriteLine (" --> Invalid input!");
         if (input.Count < 7) goto GetAgain;
         return input;
      }

      ///<summary>Prints the output results in the console page</summary>
      static void PrintResult (List<char> input) {
         Console.WriteLine ("\r\n#Possible words with score in the prefix (Pangram words are displayed in GREEN):");
         string[] dictWords = File.ReadAllLines (@"c:/etc/words.txt");
         Dictionary<string, int> answer = new ();
         foreach (var word in dictWords)
            if (IsValid (word)) answer.Add (word, ScoreOf (word));
         foreach (var item in answer.OrderByDescending (a => a.Value)) {
            if (IsPangram (item.Key)) Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ($"{item.Value,3}. {item.Key}");
            Console.ResetColor ();
         }
         Console.WriteLine ($"----\r\n{answer.Sum (x => x.Value)} => Total Score");

         ///<summary>Returns true only if the given word is valid as per spelling bee game rules</summary>
         bool IsValid (string word) {
            if (word.Length <= 3) return false;
            else if (!word.Contains (input[0])) return false;
            return word.All (input.Contains);
         }

         ///<summary>Returns the score of given word</summary>
         int ScoreOf (string word) {
            if (word.Length == 4) return 1;
            else if (IsPangram (word)) return word.Length + 7;
            return word.Length;
         }

         ///<summary>Returns true if the given word is a pangram</summary>
         bool IsPangram (string word)
            => input.All (word.Contains);
      }
   }
}