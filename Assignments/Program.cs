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

namespace Academy;
public class A12 {
   public static void Main () {
      Wordle W = new ();
      W.PlayTheGame ();
   }

   public class Wordle {
      string SecretWord, Text = "";
      int count = 0, ithComment = 0;
      bool GameOver;
      readonly int OriginX = Console.CursorLeft + (Console.WindowWidth / 2) - 13, //Reference X-coordinate.
                   OriginY = Console.CursorTop;                                   //Reference Y-coordinate.
      readonly List<List<char>> Word = new (6); // List To display and store user entered letters.
      readonly List<char> GrayList = new (),    // List to collect incorrect letters.
                          BlueList = new (),    // List to collect correct but misplaced letters.
                          GreenList = new ();   // List to collect correct and correctly placed letters.
      readonly string[] DictWords = File.ReadAllLines (@"c:\etc\dict-5.txt"),
                        GuessWords = File.ReadAllLines (@"c:\etc\puzzle-5.txt");

      // Function to run the game.
      public void PlayTheGame () {
         SetTheBoard ();
         DisplayTheBoard ();
         while (!GameOver) {
            ConsoleKeyInfo key = Console.ReadKey (true);
            UpdateGameState (key);
            DisplayTheBoard ();
         }
         Console.CursorTop = 28;
      }

      // Function to initialize the game.
      public void SetTheBoard () {
         Console.OutputEncoding = Encoding.Unicode;
         Console.CursorVisible = false;
         // Creating Type Board elements:
         for (int i = 0; i < 6; i++) 
            Word.Add (new List<char> { '\u00b7', '\u00b7', '\u00b7', '\u00b7', '\u00b7' });

         // Generating a random 5 letter word from imported file.
         var r = new Random ();
         SecretWord = GuessWords[r.Next (GuessWords.Length)];

         // Getting User input to start the game.
         DisplayTheBoard ();
         Console.ReadKey (true);
         Word[count][Text.Length] = '\u25cc';
         ithComment = 1;
      }

      // Function to display the game board in overwritten.
      public void DisplayTheBoard () {
         // TYPING BOARD:
         Console.SetCursorPosition (OriginX, OriginY);
         for (int i = 0; i < Word.Count; i++) {
            for (int j = 0; j < Word[i].Count; j++) {
               if (i < count) {
                  if (SecretWord[j] == Word[i][j]) {
                     Console.ForegroundColor = Green;
                     Console.Write ($"{Word[i][j]}    ");
                     GreenList.Add (Word[i][j]);
                  } else if (SecretWord.Contains (Word[i][j])) {
                     Console.ForegroundColor = Blue;
                     Console.Write ($"{Word[i][j]}    "); 
                     BlueList.Add (Word[i][j]);
                  } else {
                     Console.ForegroundColor = DarkGray;
                     Console.Write ($"{Word[i][j]}    "); 
                     GrayList.Add (Word[i][j]); 
                  }
               } else Console.Write ($"{Word[i][j]}    ");
               Console.ResetColor ();
            }
            Console.SetCursorPosition (OriginX, Console.CursorTop + 3);
         }

         // KEYBOARD:
         Console.SetCursorPosition (OriginX - 5, Console.CursorTop - 1);
         Console.ForegroundColor = DarkGray;
         Console.WriteLine (new string ('_', 31) + "\n"); // Top Border.
         Console.CursorLeft = OriginX - 5;
         for (char c = 'A'; c <= 'Z'; c++) {
            Console.Write ($"{ColorPrint (c)}    ");
            if ((c - 64) % 7 == 0) Console.SetCursorPosition (OriginX - 5, Console.CursorTop + 1); // Go to newline for every 8th letter.
         }
         Console.WriteLine ();
         Console.ForegroundColor = DarkGray;
         Console.CursorLeft = OriginX - 5;
         Console.WriteLine (new string ('_', 31) + "\n"); // Bottom Border.
         DisplayComments ();
         Console.SetCursorPosition (OriginX + Text.Length * 5, OriginY + count * 3); // Setting the Cursor at appropriate position for next character.

         // A local function to display the comments.
         void DisplayComments () {
            Console.CursorTop = OriginY + 25;
            Console.Write (new string (' ', Console.WindowWidth));
            List<string> Comments = new () { "Press any key to start the game", " ", $"Not a Valid Word!", $"Correct Guess! You found the word in {count} tries.",
                                         $"Sorry the Game is OVER. The Secret Word is {SecretWord.ToUpper()}.", "Not enough letters!" };
            Console.ForegroundColor = Yellow;
            Console.CursorLeft = (Console.WindowWidth - Comments[ithComment].Length - 4) / 2;
            Console.WriteLine (Comments[ithComment]);
            if (GameOver) {
               Console.ForegroundColor = Cyan;
               Console.CursorLeft = (Console.WindowWidth / 2) - 16;
               Console.Write ("Press any key to exit the game.");
               Console.ReadKey (true);
            }
            Console.ResetColor ();
         }

         // A local function to color the keyboard letters.
         char ColorPrint (char letter) {
            if (GreenList.Contains (letter)) { Console.ForegroundColor = Green; return letter; } 
            else if (BlueList.Contains (letter)) { Console.ForegroundColor = Blue; return letter; } 
            else if (GrayList.Contains (letter)) { Console.ForegroundColor = DarkGray; return letter; } 
            else Console.ResetColor ();
            return letter;
         }
      }

      // Function to update the game state based on user inputs.
      public void UpdateGameState (ConsoleKeyInfo key) {
         char input = char.ToUpper (Convert.ToChar (key.KeyChar));
         if ((key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.LeftArrow) && Text.Length != 0) {
            if (Text.Length != 5)  Word[count][Text.Length] = '\u00b7';
            Text = Text.Remove (Text.Length - 1, 1);
            Word[count][Text.Length] = '\u25cc';
         } 
         else if (char.IsLetter (input) && Text.Length < 5) {
            Word[count][Text.Length] = input;
            Text += input;
            ithComment = 1;
            if (Text.Length < 5) Word[count][Text.Length] = '\u25cc';
         } 
         else if (key.Key == ConsoleKey.Enter) {
            if (Text.Length == 5) {
               if (DictWords.Contains (Text)) {
                  if (Text == SecretWord) { 
                     ithComment = 3; 
                     count++; 
                     GameOver = true; 
                  } 
                  else if (count == 5) { 
                     ithComment = 4; 
                     count++; 
                     GameOver = true; 
                  } 
                  else { 
                     count++; 
                     Text = ""; 
                  }
               } else ithComment = 2;
            } else ithComment = 5;
         }
      }
   }
}


