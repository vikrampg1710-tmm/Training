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
   bool mGameOver = false, mPlayer;
   int mX, mY;
   char[,] coins = { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } };
   bool[] slots = new bool[9];
   string mComment;
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
      Console.WriteLine ("Player 1: X   Player 2: O");
      Console.WriteLine ("Press Enter to start the game (Player 1 plays first)");
      Console.ReadLine ();
      Console.Clear ();
   }
   public void SetTheBoard () {
      Console.Clear ();
      Console.CursorVisible = false;
      Console.OutputEncoding = Encoding.Unicode;
      mPlayer = true;
      mX = (Console.BufferWidth - 13) / 2;
      mY = 5;
      mComment = $"Player {(mPlayer ? 1 : 2)} can play";
   }
   public void DisplayTheBoard () {
      Console.CursorTop = mY;
      for (int i = 0; i < 4; i++) {
         string s = (i == 0) ? "┌┬┐" : i == 3 ? "└┴┘" : "├┼┤";
         Console.CursorLeft = mX;
         for (int j = 0; j < 4; j++)
            Console.Write (j == 0 ? $"{s[0]}─────" : j == 3 ? $"{s[2]}\r\n" : $"{s[1]}─────");
         if (i == 3) break;
         Console.CursorLeft = mX;
         for (int k = 0; k < 3; k++) {
            var v = coins[i, k];
            Console.Write ("│  ");
            Console.ForegroundColor = char.IsNumber (v) ? DarkGray : v == 'X' ? Yellow : Cyan;
            Console.Write (v);
            Console.ResetColor ();
            Console.Write (k == 2 ? $"  │\r\n" : $"  ");
         }
      }
      
      Console.CursorTop += 1;
      Console.ForegroundColor = Green;
      Console.Write (new string (' ', Console.BufferWidth));
      Console.CursorLeft = 3 + (Console.BufferWidth - mComment.Length) / 2;
      Console.WriteLine (mComment);
      Console.ResetColor ();
   }

   public void UpdateState (ConsoleKey input) {
      var x = mPlayer ? 'X' : 'O';
      int v = (char)input - 49;
      if (slots[v]) {
         mComment = $"Position {v + 1} already occupied. Enter different number 1 - 9";
         return;
      } else mComment = $"Player {(mPlayer ? 1 : 2)} can play";
      switch (input) {
         case D1: { slots[0] = true; coins[0, 0] = x; break; }
         case D2: { slots[1] = true; coins[0, 1] = x; break; }
         case D3: { slots[2] = true; coins[0, 2] = x; break; }
         case D4: { slots[3] = true; coins[1, 0] = x; break; }
         case D5: { slots[4] = true; coins[1, 1] = x; break; }
         case D6: { slots[5] = true; coins[1, 2] = x; break; }
         case D7: { slots[6] = true; coins[2, 0] = x; break; }
         case D8: { slots[7] = true; coins[2, 1] = x; break; }
         case D9: { slots[8] = true; coins[2, 2] = x; break; }
         default: break;
      }
      mPlayer = !mPlayer;
      mGameOver = IsGameOver (coins);
      if (mGameOver)
         mComment = slots.All (a => a) ? "The game end in a draw!" : $"Game Over! Player {(mPlayer ? 1 : 2)} wins";
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
         || slots.All (a => a);
}