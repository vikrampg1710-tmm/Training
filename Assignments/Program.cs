using System.Text;
using System;
using static System.ConsoleKey;
using static System.ConsoleColor;
using System.Linq;

namespace SparkTest;

class Program {
   public static void Main () 
      => new TicTacToe_Game ().PlayTheGame();
}
public class TicTacToe_Game {
   bool mGameOver, mPlayer;
   int mX, mY;
   char[,] mCoins = { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
   bool[] mSlots = new bool[9];
   (string, ConsoleColor) mComment;
   public void PlayTheGame () {
      Welcome ();
      SetTheBoard ();
      DisplayTheBoard ();
      while (!mGameOver) {
         UpdateState (Console.ReadKey (true).Key);
         DisplayTheBoard ();
      }
   }

   public static void Welcome () {
      Console.WriteLine ("TicTacToe Game!");
      Console.WriteLine ("Instructions:\n\t1. Player 1: X   Player 2: O" +
         "\n\t2. To enter your input at any slot, press the number shown in that slot");
      Console.WriteLine ("\nPress Enter to start the game (Player 1 plays first)");
      Console.ReadLine ();
      Console.Clear ();
   }
   public void SetTheBoard () {
      Console.CursorVisible = false;
      Console.OutputEncoding = Encoding.Unicode;
      (mGameOver, mPlayer) = (false, true);
      (mX, mY) = ((Console.BufferWidth - 13) / 2, 5);
      mComment = (string.Empty, White);
   }
   public void DisplayTheBoard () {
      Console.ForegroundColor = Yellow;
      Console.SetCursorPosition (mX + 1, mY - 2);
      Console.WriteLine ($"Player {(mPlayer ? 1 : 2)} to play...");
      Console.ResetColor ();
      Console.CursorTop = mY;
      for (int i = 0; i < 4; i++) {
         string s = (i == 0) ? "┌┬┐" : i == 3 ? "└┴┘" : "├┼┤";
         Console.CursorLeft = mX;
         for (int j = 0; j < 4; j++)
            Console.Write (j == 0 ? $"{s[0]}─────" : j == 3 ? $"{s[2]}\r\n" : $"{s[1]}─────");
         if (i == 3) break;
         Console.CursorLeft = mX;
         for (int k = 0; k < 3; k++) {
            var v = mCoins[i, k];
            Console.Write ("│  ");
            Console.ForegroundColor = char.IsNumber (v) ? DarkGray : v == 'X' ? Yellow : Cyan;
            Console.Write (v);
            Console.ResetColor ();
            Console.Write (k == 2 ? $"  │\r\n" : $"  ");
         }
      }
      Console.CursorTop += 1;
      Console.ForegroundColor = mComment.Item2;
      Console.Write (new string (' ', Console.BufferWidth));
      Console.CursorLeft = 3 + (Console.BufferWidth - mComment.Item1.Length) / 2;
      Console.WriteLine (mComment.Item1);
      Console.ResetColor ();
   }

   public void UpdateState (ConsoleKey input) {
      bool isOne = mSlots.Count (a => a) % 2 == 0;
      char x = isOne ? 'X' : 'O';
      int i = (char)input;
      int key = (i >= (int)D1 && i <= (int)D9) ? i - (int)D1 
         : (i >= (int)NumPad1 && i <= (int)NumPad9) ? i - (int)NumPad1 
         : -1;
      if (key < 0) { mComment = ("Invalid key input", Red); return; }
      if (mSlots[key]) {
         mComment = ($"Position {key + 1} already occupied. Enter a different number", Blue);
         return;
      } else mComment = (string.Empty, White);
      switch (input) {
         case D1 or NumPad1: { mSlots[0] = true; mCoins[0, 0] = x; break; }
         case D2 or NumPad2: { mSlots[1] = true; mCoins[0, 1] = x; break; }
         case D3 or NumPad3: { mSlots[2] = true; mCoins[0, 2] = x; break; }
         case D4 or NumPad4: { mSlots[3] = true; mCoins[1, 0] = x; break; }
         case D5 or NumPad5: { mSlots[4] = true; mCoins[1, 1] = x; break; }
         case D6 or NumPad6: { mSlots[5] = true; mCoins[1, 2] = x; break; }
         case D7 or NumPad7: { mSlots[6] = true; mCoins[2, 0] = x; break; }
         case D8 or NumPad8: { mSlots[7] = true; mCoins[2, 1] = x; break; }
         case D9 or NumPad9: { mSlots[8] = true; mCoins[2, 2] = x; break; }
         default: return;
      }
      mPlayer = !mPlayer;
      mGameOver = IsGameOver (mCoins);
      if (mGameOver)
         mComment = mSlots.All (a => a) ? ("The game end in a draw!", Green) : ($"Game Over! Player {(isOne ? 1 : 2)} wins", Green);
   }

   bool IsGameOver (char[,] input)
      => input[0, 0] == input[0, 1] && input[0, 1] == input[0, 2]
         || input[1, 0] == input[1, 1] && input[1, 1] == input[1, 2]
         || input[2, 0] == input[2, 1] && input[2, 1] == input[2, 2]
         || input[0, 0] == input[1, 0] && input[1, 0] == input[2, 0]
         || input[0, 1] == input[1, 1] && input[1, 1] == input[2, 1]
         || input[0, 2] == input[1, 2] && input[1, 2] == input[2, 2]
         || input[0, 0] == input[1, 1] && input[1, 1] == input[2, 2]
         || input[0, 2] == input[1, 1] && input[1, 1] == input[2, 0]
         || mSlots.All (a => a);
}