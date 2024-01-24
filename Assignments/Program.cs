using System.Text;
using System;
using static System.ConsoleKey;

namespace SparkTest;

class Program {
   public static void Main (string[] args) {
      var t = new TicTacToe_Game ();
      t.PlayTheGame ();
      //t.SetTheBoard ();
      //t.DisplayTheBoard ();
   }
}
public class TicTacToe_Game {
   bool mGameOver = false;
   bool mPlayer;
   int mX, mY;
   public void PlayTheGame () {
      /*Console.WriteLine ("TicTacToe Game!");
      Console.WriteLine ("Player 1: X   Player 2: O");
      Console.WriteLine ("Press Enter to start the game (Player 1 plays first)");*/
      SetTheBoard ();
      DisplayTheBoard ();
      while (mGameOver) {
         UpdateState (Console.ReadKey ().Key);
         DisplayTheBoard ();
      }
      //DisplayTheResult ();
   }

   public void SetTheBoard () {
      Console.Clear ();
      Console.OutputEncoding = Encoding.Unicode;
      mPlayer = true;
      int[] slots = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
      mX = (Console.BufferWidth - 13) / 2;
      mY = 5;
   }
   public void DisplayTheBoard () {
      Console.WriteLine ($"Player {(mPlayer ? "1" : "2")} can play.");
      Console.CursorTop = mY;
      for (int i = 0; i < 4; i++) {
         string s = (i == 0) ? "┌┬┐" : i == 3 ? "└┴┘" : "├┼┤";
         Console.CursorLeft = mX;
         for (int j = 0; j < 4; j++)
            Console.Write (j == 0 ? $"{s[0]}─\u2500\u2500" : j == 3 ? $"{s[2]}\r\n" : $"{s[1]}─\u2500\u2500");
         Console.CursorLeft = mX;
         if (i != 3) Console.WriteLine ($"│   │   │   │");
      }
   }

   public void UpdateState (ConsoleKey input) {
      var n1 = ((int)input);
      switch (input) {
         case NumPad0: break;
         case NumPad1: {
               Console.SetCursorPosition (mX + 3, mY + 1);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad2: {
               Console.SetCursorPosition (mX + 6, mY + 1);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad3: {
               Console.SetCursorPosition (mX + 9, mY + 1);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad4: {
               Console.SetCursorPosition (mX + 3, mY + 4);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad5: {
               Console.SetCursorPosition (mX + 6, mY + 4);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad6: {
               Console.SetCursorPosition (mX + 9, mY + 4);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad7: {
               Console.SetCursorPosition (mX + 3, mY + 7);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad8: {
               Console.SetCursorPosition (mX + 6, mY + 7);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
         case NumPad9: {
               Console.SetCursorPosition (mX + 9, mY + 7);
               Console.Write (mPlayer ? "X" : "O");
               break;
            }
      }
      mPlayer = !mPlayer;

   }

   public void DisplayTheResult () {
      Console.Clear ();
      Console.WriteLine ("Player wins");
      Console.WriteLine ("Game is draw");
   }
}