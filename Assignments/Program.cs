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

#region WordleGameClass----------------------------------------------------------------------------
public class Wordle {
   # region Properties --------------------------------------------------------
   public string mSecretWord, mWord, mComment;
   public bool mTesting = false, mGameOver;
   int mX, mY;                                // // Reference X & Y coordinates
   public int mTriesCount = 0;                // To count no.of tries made
   public List<List<char>> mWords = new (6);  // List To store user entered letters
   readonly Dictionary<char, ConsoleColor> mColorCode = new (); // To store foreground color to each char
   public string[] mDictWords, mGuessWords;   // To store loaded strings
   public StreamWriter testOutputFile = null; // Created for testing purpose
   #endregion

   #region Methods --------------------------------------------------
   public void PlayTheGame () {
      SetTheBoard ();
      DisplayTheBoard ();
      while (!mGameOver) {
         UpdateGameState (Console.ReadKey (true).Key);
         DisplayTheBoard ();
      }
   }

   /// <summary>Initializes the game</summary>
   public void SetTheBoard () {
      // Initial settings:
      (mWord, mComment) = ("", "");
      (mX, mY) = mTesting ? (49, 3) : (Console.CursorLeft + (Console.WindowWidth / 2) - 13, Console.CursorTop);
      mDictWords = LoadStrings ("dict-5.txt");
      mGuessWords = LoadStrings ("puzzle-5.txt");
      Console.OutputEncoding = Encoding.Unicode;
      if (!mTesting) Console.CursorVisible = false;
      // Creating Typing Board elements:
      for (int i = 0; i < 6; i++)
         mWords.Add (new List<char> { '\u00b7', '\u00b7', '\u00b7', '\u00b7', '\u00b7' });
      // Generating a random 5 letter mWords from loaded strings file:
      mSecretWord = mGuessWords[new Random ().Next (mGuessWords.Length)];
      // Assigning colors to each chars:
      for (char c = 'A'; c <= 'Z'; c++)
         mColorCode.Add (c, White);
      mColorCode.Add ('\u00b7', White);
      mColorCode.Add ('\u25cc', White);
   }

   /// <summary>Displays the updated game board</summary>
   public void DisplayTheBoard () {
      #region 1. TYPING BOARD
      SetCursorAt (mX, mY);
      for (int i = 0; i < mWords.Count; i++) {
         for (int j = 0; j < mWords[i].Count; j++) {
            var (a, b, c) = (' ', ' ', mWords[i][j]);
            ConsoleColor color = mColorCode[c];
            if (i < mTriesCount) {
               if (mTesting) {
                  (a, b, mColorCode[c])
                     = (mSecretWord[j] == c) ? ('{', '}', color == White ? Green : color)
                     : mSecretWord.Contains (c) ? ('[', ']', color == White ? Blue : color)
                     : ('(', ')', DarkGray);
               } else {
                  (Console.ForegroundColor, mColorCode[c])
                     = (mSecretWord[j] == c) ? (Green, color == White ? Green : color)
                     : mSecretWord.Contains (c) ? (Blue, color == White ? Blue : color)
                     : (DarkGray, DarkGray);
               }
            }
            if (mTesting) Write ($" {a}{c}{b} ");
            else { Write ($"{c}    "); Console.ResetColor (); }
         }
         SetCursorAt (mX, mTesting ? 3 : Console.CursorTop + 3);
      }
      #endregion

      #region 2. KEYBOARD
      if (mTesting) SetCursorAt (mX - 3, 1);
      else {
         SetCursorAt (mX - 5, Console.CursorTop);
         Console.ForegroundColor = DarkGray;
      }
      WriteLine (new string ('_', 31) + "\r\n");
      SetCursorAt (mX - 5, mTesting ? 0 : Console.CursorTop);
      for (char c = 'A'; c <= 'Z'; c++) {
         if (!mTesting) Console.ForegroundColor = mColorCode[c];
         var (a, b) = mColorCode[c] == Green ? ('{', '}')
                     : mColorCode[c] == Blue ? ('[', ']')
                     : mColorCode[c] == DarkGray ? ('(', ')')
                     : (' ', ' ');
         Write (mTesting ? $" {a}{c}{b} " : $"{c}    ");
         if ((c - 64) % 7 == 0) SetCursorAt (mX - 5, mTesting ? 1 : Console.CursorTop + 1);
      }
      if (mTesting) SetCursorAt (mX - 3, 1);
      else {
         SetCursorAt (mX - 5, Console.CursorTop + 1);
         Console.ForegroundColor = DarkGray;
      }
      WriteLine (new string ('_', 31) + "\r\n");
      #endregion

      #region 3. COMMENT
      DisplayComments ();
      if (!mTesting) SetCursorAt (mX + mWord.Length * 5, mY + mTriesCount * 3);
      #endregion

      #region Helping Local Methods:
      void SetCursorAt (int x, int y) {
         if (mTesting) {
            testOutputFile.Write (string.Join ("", Enumerable.Repeat ("\r\n", y)));
            testOutputFile.Write (new string (' ', x));
         } else Console.SetCursorPosition (x, y);
      }

      void WriteLine (string s) {
         if (mTesting) testOutputFile.WriteLine (s);
         else Console.WriteLine (s);
      }

      void Write (string s) {
         if (mTesting) testOutputFile.Write (s);
         else Console.Write (s);
      }

      /// <summary>Displays the comment below the keyboard</summary>
      void DisplayComments () {
         var (width, height) = mTesting ? (120, 1) : (Console.WindowWidth, Console.CursorTop);
         if (!mTesting) Console.ForegroundColor = Yellow;
         SetCursorAt ((width - mComment.Length - 4) / 2, height);
         WriteLine (mComment);
         if (!mTesting) Console.ResetColor ();
      }
      #endregion
   }

   /// <summary>Gets the input key from user and updates the game state</summary>
   public void UpdateGameState (ConsoleKey key) {
      char input = char.ToUpper (Convert.ToChar (key));
      if ((key == ConsoleKey.Backspace || key == ConsoleKey.LeftArrow) && mWord.Length != 0) {
         if (mWord.Length != 5) mWords[mTriesCount][mWord.Length] = '\u00b7';
         mWord = mWord.Remove (mWord.Length - 1, 1);
         mWords[mTriesCount][mWord.Length] = '\u25cc';
      } else if (input >= 'A' && input <= 'Z' && mWord.Length < 5) {
         mWords[mTriesCount][mWord.Length] = input;
         mWord += input;
         mComment = "";
         if (mWord.Length < 5) mWords[mTriesCount][mWord.Length] = '\u25cc';
      } else if (key == ConsoleKey.Enter) {
         if (mWord.Length == 5) {
            if (mDictWords.Contains (mWord)) {
               if (mWord == mSecretWord) {
                  mComment = $"Correct! You have won in {++mTriesCount} tries.";
                  mGameOver = true;
               } else if (mTriesCount == 5) {
                  mComment = $"GAME OVER, you have lost! Secret word: {mSecretWord.ToUpper ()}";
                  mTriesCount++;
                  mGameOver = true;
               } else {
                  mTriesCount++;
                  mWord = "";
               }
            } else mComment = "Not a Valid words!";
         } else mComment = "Not enough letters!";
      }
   }

   /// <summary>Returns the strings by loading from an assembly-manifest resource file</summary>
   public static string[] LoadStrings (string file) {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"Assignments.Files.{file}");
      using var reader = new StreamReader (stream);
      return reader.ReadToEnd ().Split ("\r\n");
   }
   #endregion
}
#endregion