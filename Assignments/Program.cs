using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using static System.ConsoleColor;
using System.Reflection;

namespace Academy;
public class A12 {
   public static void Main ()
      => new Wordle ().PlayTheGame ();
}

#region WordleGameClass----------------------------------------------------------------------------
public class Wordle {
   # region Properties --------------------------------------------------------
   string mSecretWord, mGuessWord, mComment;
   bool mTesting, mGameOver;
   int mX, mY, mTryCount = 0;               // Reference (X,Y) coordinates & To count the tries made
   List<List<char>> mTriedWords = new (6);  // List To store user entered letters
   readonly Dictionary<char, ConsoleColor> mColorCodes = new (); // To store foreground color to each char
   string[] mDictList, mGuessList;          // To store loaded strings
   StreamWriter mTextFile = null;           // Created for testing purpose
   public string SecretWord {
      get => mSecretWord;
      set => mSecretWord = value;
   }
   public List<List<char>> TriedWords => mTriedWords;
   protected string GuessWord {
      get => mGuessWord;
      set => mGuessWord = value;
   }
   protected string Comment => mComment;
   protected bool GameOver => mGameOver;
   protected bool Testing {
      get => mTesting;
      set => mTesting = value;
   }
   protected int TryCount => mTryCount;
   protected StreamWriter TextFile { set => mTextFile = value; }
   #endregion

   #region Methods --------------------------------------------------
   public void PlayTheGame () {
      ResetTheBoard ();
      DisplayTheBoard ();
      while (!mGameOver) {
         UpdateGameState (Console.ReadKey (true).Key);
         DisplayTheBoard ();
      }
   }
   /// <summary>Initializes the game</summary>
   public void ResetTheBoard () {
      // Initial settings:
      (mGuessWord, mComment) = ("", "");
      (mX, mY) = mTesting ? (49, 3) : (Console.CursorLeft + (Console.WindowWidth / 2) - 13, Console.CursorTop);
      mDictList = LoadStrings ("dict-5.txt");
      mGuessList = LoadStrings ("puzzle-5.txt");
      Console.OutputEncoding = Encoding.Unicode;
      if (!mTesting) Console.CursorVisible = false;
      // Creating Typing Board elements:
      for (int i = 0; i < 6; i++)
         mTriedWords.Add (new List<char> { '\u00b7', '\u00b7', '\u00b7', '\u00b7', '\u00b7' });
      // Generating a random 5 letter mTriedWords from loaded strings file:
      mSecretWord = mGuessList[new Random ().Next (mGuessList.Length)];
      // Assigning colors to each chars:
      for (char c = 'A'; c <= 'Z'; c++)
         mColorCodes.Add (c, White);
      mColorCodes.Add ('\u00b7', White);
      mColorCodes.Add ('\u25cc', White);
   }

   /// <summary>Displays the updated game board</summary>
   public void DisplayTheBoard () {
      #region 1. TYPING BOARD
      SetCursorAt (mX, mY);
      for (int i = 0; i < mTriedWords.Count; i++) {
         for (int j = 0; j < mTriedWords[i].Count; j++) {
            var (a, b, c) = (' ', ' ', mTriedWords[i][j]);
            ConsoleColor color = mColorCodes[c];
            if (i < mTryCount) {
               if (mTesting) {
                  (a, b, mColorCodes[c])
                     = (mSecretWord[j] == c) ? ('{', '}', color == White ? Green : color)
                     : mSecretWord.Contains (c) ? ('[', ']', color == White ? Blue : color)
                     : ('(', ')', DarkGray);
               } else {
                  (Console.ForegroundColor, mColorCodes[c])
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
         if (!mTesting) Console.ForegroundColor = mColorCodes[c];
         var (a, b) = mColorCodes[c] == Green ? ('{', '}')
                     : mColorCodes[c] == Blue ? ('[', ']')
                     : mColorCodes[c] == DarkGray ? ('(', ')')
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
      DisplayComments ();
      if (mTesting && mGameOver) mTextFile.Close ();
      #endregion

      #region Helping Local Methods:
      void SetCursorAt (int x, int y) {
         if (mTesting)
            mTextFile.Write (string.Join ("", Enumerable.Repeat ("\r\n", y)) + new string (' ', x));
         else Console.SetCursorPosition (x, y);
      }

      void WriteLine (string s) {
         if (mTesting) mTextFile.WriteLine (s);
         else Console.WriteLine (s);
      }

      void Write (string s) {
         if (mTesting) mTextFile.Write (s);
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
      if ((key == ConsoleKey.Backspace || key == ConsoleKey.LeftArrow) && mGuessWord.Length != 0) {
         if (mGuessWord.Length != 5) mTriedWords[mTryCount][mGuessWord.Length] = '\u00b7';
         mGuessWord = mGuessWord.Remove (mGuessWord.Length - 1, 1);
         mTriedWords[mTryCount][mGuessWord.Length] = '\u25cc';
      } else if (input >= 'A' && input <= 'Z' && mGuessWord.Length < 5) {
         mTriedWords[mTryCount][mGuessWord.Length] = input;
         mGuessWord += input;
         mComment = new string (' ', mTesting ? 60 : Console.BufferWidth / 2);
         if (mGuessWord.Length < 5) mTriedWords[mTryCount][mGuessWord.Length] = '\u25cc';
      } else if (key == ConsoleKey.Enter) {
         if (mGuessWord.Length == 5) {
            if (mDictList.Contains (mGuessWord)) {
               if (mGuessWord == mSecretWord) {
                  mComment = $"Correct! You have won in {++mTryCount} tries.";
                  mGameOver = true;
               } else if (mTryCount == 5) {
                  mComment = $"GAME OVER, you have lost! Secret word: {mSecretWord.ToUpper ()}";
                  mTryCount++;
                  mGameOver = true;
               } else {
                  mTryCount++;
                  mGuessWord = "";
               }
            } else mComment = "Not a valid word!";
         } else mComment = "Not enough letters!";
      }
   }

   /// <summary>Returns the strings by loading from an assembly-manifest resource file</summary>
   public string[] LoadStrings (string file) {
      using var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ($"Assignments.Files.{file}");
      using var reader = new StreamReader (stream);
      return reader.ReadToEnd ().Split ("\r\n");
   }
   #endregion
}
#endregion