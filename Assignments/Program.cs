// ---------------------------------------------------------------------------------------
// Spark23 Assignments
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
using System.Threading;

namespace Spark;
public class T16 {
   public static void Main () {
      Console.OutputEncoding = Encoding.Unicode;
      Console.WriteLine ("----------------------------------------------------------------------------------------------------------\n"
      + " A.03.P01 - Spelling Bee Cheat Code.\n"
      + " ◌ Spelling Bee is a game in which we have to find possible words from given 7 letters, among 1 is a compulsory letter.\n"
      + " ◌ A word should atleast contain 4 letters, which scores 1 point. Further, n letter word scores n points.\n"
      + " ◌ If a word contains all the given 7 letters called as PANGRAM, which score additional 7 points.\n"
      + "----------------------------------------------------------------------------------------------------------\n\n");


      // Getting 7 input letters from user.
      List<char> targetLetters = new () { 'U', 'X', 'A', 'T', 'L', 'N', 'E' };
      Console.Write ("Do you want to input 7 letters? ");
      for (; ; ) {
         Console.Write ("(Y)es/(N)o? : ");
         char choice = Char.ToUpper (Console.ReadKey ().KeyChar);
         Console.WriteLine ();
         if (choice == 'Y') {
            Console.WriteLine ("\nThen, Enter 7 different alphabet letters:-");
            targetLetters.Clear ();
            for (int i = 1; i <= 7;) {
               Console.Write ($"Enter letter {i}: ");
               char letter = Char.ToUpper (Console.ReadKey ().KeyChar);
               Console.WriteLine ();
               if (Char.IsLetter (letter)) {
                  if (!targetLetters.Contains (letter)) {
                     targetLetters.Add (letter);
                     i++;
                  } else {
                     Console.ForegroundColor = ConsoleColor.Cyan;
                     Console.WriteLine ("Please enter a different letter\n");
                     Console.ResetColor ();
                     continue;
                  }
               } else {
                  Console.ForegroundColor = ConsoleColor.Red;
                  Console.WriteLine ("Incorrect input!  Please enter a letter.\n");
                  Console.ResetColor ();
               }
            }
            break;
         } else if (choice == 'N') {
            Console.WriteLine ("\nOkay! Here is the default 7 letters: U, X, A, T, L, N, E.\nPress ENTER to display the answers");
            Console.ReadLine ();
            break;
         } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Incorrect answer.  Enter 'Y' for YES | 'N' for NO.");
            Console.ResetColor ();
         }
      }

      string head = "These are the possible words, with score in prefix:\n(Pangram words are displayed in GREEN)\n";
      foreach (char i in head) {
         Console.Write (i == ')' ? i + "\n" : i);
         Thread.Sleep (20);
      }

      // Creating a separate list to store non-given letters.
      List<char> remLetters = new ();
      for (char c = 'A'; c <= 'Z'; c++) {
         if (targetLetters.Contains (c)) continue;
         else remLetters.Add (c);
      }

      // Adding all valid words from dictionary file to a separate list called validWords.
      List<string> validWords = new ();
      string[] discWords = File.ReadAllLines (@"c:/etc/words.txt");
      foreach (var word in discWords)
         if (IsValid (word)) validWords.Add (word);

      // Ordering dictionary elements based on score of each words (High to Low). 
      Dictionary<string, int> Answer = new ();
      foreach (string word in validWords)
         Answer.Add (word, calcScore (word));
      var sortedDict = Answer.OrderByDescending (a => a.Value);

      // Making the pangram word to display in green color.
      int count = 0;
      foreach (KeyValuePair<string, int> abc in sortedDict) {
         if (IsPangram (abc.Key)) {
            count++;
            Console.ForegroundColor = ConsoleColor.Green;
            Thread.Sleep (100);
            Console.WriteLine ($"{abc.Value,3}. {abc.Key}");
            Console.ResetColor ();
         }
      }

      // Printing all Answers score-wise & alphabetically.
      foreach (KeyValuePair<string, int> abc in sortedDict) {
         if (!IsPangram (abc.Key)) {
            Thread.Sleep (100);
            Console.WriteLine ($"{abc.Value,3}. {abc.Key}");
         }
      }

      // Printing Total score and No. of Pangram words.
      Console.WriteLine ($"----\nTotal Score = {sortedDict.Sum (x => x.Value)}"
      + $"\nNumber of Pangram words: {count}.");

      //---------Main program OVER.  Below are the methods used.--------------//
      // Filtering conditions of words:
      bool IsValid (string word) {
         if (word.Length <= 3) return false; // To remove all words having length less than 4.
         foreach (char c in remLetters)  // To remove all words having non target letters.
            if (word.Contains (c)) { return false; } else if (!word.Contains (targetLetters[0])) { return false; } // To remove all words that not having center letter, in this case 'U'.
         return true;
      }

      // Calculating Score:
      int calcScore (string word) {
         if (word.Length == 4) return 1;
         else if (IsPangram (word)) return word.Length + 7;
         else return word.Length;
      }

      // Checking for Pangram:
      bool IsPangram (string word) {
         foreach (char c1 in targetLetters) {
            bool present = false;
            foreach (char c2 in word)
               if (c2 == c1) { present = true; break; }
            if (!present) return false;
         }
         return true;
      }
   }
}


