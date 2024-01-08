// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A12 - Wordle Game
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using static System.ConsoleColor;
using System.Reflection;

namespace Academy;
public class A12 {
   public static void Main () => new Wordle ().PlayTheGame ();
}

public class Wordle {
   public string secretWord, text;
   public int triesCount = 0, ithComment = 0;
   public bool testing = false, gameOver;
   public int originX, originY;               // Reference X & Y coordinates.
   public List<List<char>> word = new (6);    // List To display and store user entered letters.
   readonly List<char> grayList = new (),     // List to collect incorrect letters.
                       blueList = new (),     // List to collect correct but misplaced letters.
                       greenList = new ();    // List to collect correct and correctly placed letters.
   public string[] dictWords, guessWords;     // To store loaded strings
   public StreamWriter testOutputFile = null; // Created for testing purpose.

   public void PlayTheGame () {
      SetTheBoard ();
      DisplayTheBoard ();
      while (!gameOver) {
         ConsoleKeyInfo key = Console.ReadKey (true);
         UpdateGameState (key.Key);
         DisplayTheBoard ();
      }
      Console.CursorTop = 28;
   }

   /// <summary>Initializes the game</summary>
   public void SetTheBoard () {
      text = "";
      (originX, originY) = testing ? (49, 3) : (Console.CursorLeft + (Console.WindowWidth / 2) - 13, Console.CursorTop);
      dictWords = LoadStrings ("dict-5.txt");
      guessWords = LoadStrings ("puzzle-5.txt");
      Console.OutputEncoding = Encoding.Unicode;
      if (!testing) Console.CursorVisible = false;
      // Creating Type Board elements:
      for (int i = 0; i < 6; i++)
         word.Add (new List<char> { '\u00b7', '\u00b7', '\u00b7', '\u00b7', '\u00b7' });
      // Generating a random 5 letter word from loaded strings file.
      var r = new Random ();
      secretWord = guessWords[r.Next (guessWords.Length)];
   }

   /// <summary>Displays the updated game board</summary>
   public void DisplayTheBoard () {
      if (testing) {
         // TYPING BOARD:
         testOutputFile.Write (string.Join ("", Enumerable.Repeat ("\r\n", 3)));
         if (text.Length == 1) testOutputFile.WriteLine ($"Try-{triesCount + 1}");
         testOutputFile.Write (new string (' ', originX));
         for (int i = 0; i < word.Count; i++) {
            for (int j = 0; j < word[i].Count; j++) {
               var (a, b, c) = (' ', ' ', word[i][j]);
               if (i < triesCount) {
                  // Green --> {} | Blue  --> [] | Grey  --> ()
                  if (secretWord[j] == c) { (a, b) = ('{', '}'); greenList.Add (c); } else if (secretWord.Contains (c)) { (a, b) = ('[', ']'); blueList.Add (c); } else { (a, b) = ('(', ')'); grayList.Add (c); }
               }
               testOutputFile.Write ($" {a}{c}{b} ");
            }
            testOutputFile.Write (string.Join ("", Enumerable.Repeat ("\r\n", 3)));
            testOutputFile.Write (new string (' ', originX));
         }
         // KEYBOARD:
         testOutputFile.WriteLine ();
         testOutputFile.Write (new string (' ', originX - 3));
         testOutputFile.WriteLine (new string ('_', 31));
         testOutputFile.WriteLine ();
         testOutputFile.Write (new string (' ', originX - 5));
         for (char c = 'A'; c <= 'Z'; c++) {
            var (a, b) = greenList.Contains (c) ? ('{', '}')
                        : blueList.Contains (c) ? ('[', ']')
                        : grayList.Contains (c) ? ('(', ')')
                        : (' ', ' ');
            testOutputFile.Write ($" {a}{c}{b} ");
            if ((c - 64) % 7 == 0) {
               testOutputFile.WriteLine ();
               testOutputFile.Write (new string (' ', originX - 5));
            }
         }
         testOutputFile.WriteLine ();
         testOutputFile.Write (new string (' ', originX - 3));
         testOutputFile.WriteLine (new string ('_', 31));
         DisplayComments ();
      } else {
         // TYPING BOARD:
         Console.SetCursorPosition (originX, originY);
         for (int i = 0; i < word.Count; i++) {
            for (int j = 0; j < word[i].Count; j++) {
               char c = word[i][j];
               if (i < triesCount) {
                  if (secretWord[j] == c) {
                     Console.ForegroundColor = Green;
                     greenList.Add (c);
                  } else if (secretWord.Contains (c)) {
                     Console.ForegroundColor = Blue;
                     blueList.Add (c);
                  } else {
                     Console.ForegroundColor = DarkGray;
                     grayList.Add (c);
                  }
               }
               Console.Write ($"{c}    ");
               Console.ResetColor ();
            }
            Console.SetCursorPosition (originX, Console.CursorTop + 3);
         }
         // KEYBOARD:
         Console.SetCursorPosition (originX - 5, Console.CursorTop - 1);
         Console.ForegroundColor = DarkGray;
         Console.WriteLine (new string ('_', 31) + "\n");
         Console.CursorLeft = originX - 5;
         for (char c = 'A'; c <= 'Z'; c++) {
            Console.ForegroundColor = grayList.Contains (c) ? DarkGray
                                    : blueList.Contains (c) ? Blue
                                    : greenList.Contains (c) ? Green
                                    : White;
            Console.Write ($"{c}    ");
            if ((c - 64) % 7 == 0) Console.SetCursorPosition (originX - 5, Console.CursorTop + 1);
         }
         Console.WriteLine ();
         Console.ForegroundColor = DarkGray;
         Console.CursorLeft = originX - 5;
         Console.WriteLine (new string ('_', 31) + "\n");
         DisplayComments ();
         Console.SetCursorPosition (originX + text.Length * 5, originY + triesCount * 3);
      }

      /// <summary>Displays the comment below the keyboard</summary>
      void DisplayComments () {
         List<string> comments = new () { "", "Not a Valid word!", $"Correct! You have won in {triesCount} tries.",
                                         $"GAME OVER, you have lost! Secret word: {secretWord.ToUpper()}", "Not enough letters!" };
         if (testing) {
            testOutputFile.WriteLine ();
            // Console Window Width = 120, which is hard-coded below for testing purpose.
            testOutputFile.Write (new string (' ', (120 - comments[ithComment].Length) / 2));
            testOutputFile.WriteLine (comments[ithComment]);
         } else {
            Console.CursorTop = originY + 25;
            Console.Write (new string (' ', Console.WindowWidth));
            Console.ForegroundColor = Yellow;
            Console.CursorLeft = (Console.WindowWidth - comments[ithComment].Length - 4) / 2;
            Console.WriteLine (comments[ithComment]);
            Console.ResetColor ();
         }
      }
   }
   /// <summary>Gets the input key from user and updates the game state</summary>
   public void UpdateGameState (ConsoleKey key) {
      char input = char.ToUpper (Convert.ToChar (key));
      if ((key == ConsoleKey.Backspace || key == ConsoleKey.LeftArrow) && text.Length != 0) {
         if (text.Length != 5) word[triesCount][text.Length] = '\u00b7';
         text = text.Remove (text.Length - 1, 1);
         word[triesCount][text.Length] = '\u25cc';
      } else if (char.IsLetter (input) && text.Length < 5) {
         word[triesCount][text.Length] = input;
         text += input;
         ithComment = 0;
         if (text.Length < 5) word[triesCount][text.Length] = '\u25cc';
      } else if (key == ConsoleKey.Enter) {
         if (text.Length == 5) {
            if (dictWords.Contains (text)) {
               if (text == secretWord) {
                  ithComment = 2;
                  triesCount++;
                  gameOver = true;
               } else if (triesCount == 5) {
                  ithComment = 3;
                  triesCount++;
                  gameOver = true;
               } else {
                  triesCount++;
                  text = "";
               }
            } else ithComment = 1;
         } else ithComment = 4;
      }
   }

   /// <summary>Returns the strings by loading from an assembly-manifest resource file</summary>
   public static string[] LoadStrings (string file) {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"Assignments.Files.{file}");
      using var reader = new StreamReader (stream);
      return reader.ReadToEnd ().Split ("\r\n");
   }
}
