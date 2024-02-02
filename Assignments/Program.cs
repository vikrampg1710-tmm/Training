// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A6 - NQueens Problem
// ---------------------------------------------------------------------------------------

using System.Collections.Generic;
using System;
using System.Text;
using static System.ConsoleColor;
using static System.ConsoleKey;
using System.Linq;

namespace Academy23;
public class A6 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "NQueens Problem:-" + "\x1B[0m");
      int n = GetSize ();
      Console.Write ("\r\nDo you want to print only UNIQUE solution(s) [y/n]? ");
      bool choice = Console.ReadKey ().Key == Y;
      List<int[]> solns = new ();
      SolveTheBoard (new bool[n, n], 0);
      PrintAllBoards (choice ? Filtered (solns) : solns);

      #region Helper methods:
      int GetSize () {
         GetSize: Console.Write ("\r\nEnter the n of Chess Board, n = ");
         if (int.TryParse (Console.ReadLine (), out int size)) {
            switch (size) {
               case 0: ColorPrint ("0x0 Chess Board is not possible. Try with a greater n.", Cyan); goto GetSize;
               case 1: ColorPrint ("1x1 Chess Board has only one solution. Try with a greater n.", Cyan); goto GetSize;
               case 2: goto case 3;
               case 3: ColorPrint ($"{size}x{size} Chess Board has 0 solution.  Try with a greater n.", Cyan); goto GetSize;
               case > 10: ColorPrint ($"Please try with a n less than 10.", Cyan); goto GetSize;
               default: return size;
            }
         } else {
            ColorPrint ("Enter a valid input.", Yellow);
            goto GetSize;
         }
      }
      bool SolveTheBoard (bool[,] ChessBoard, int col) {
         for (int i = 0; i < n; i++) {
            if (IsSafe (ChessBoard, i, col)) {
               ChessBoard[i, col] = true;
               if (SolveTheBoard (ChessBoard, col + 1))
                  ChessBoard[i, col] = false;
            }
         }
         if (col == n) {
            int[] pos = new int[n];
            for (int i = 0; i < n; i++) {
               for (int j = 0; j < n; j++)
                  if (ChessBoard[i, j]) pos[i] = j;
            }
            solns.Add (pos);
         }
         return true;

         bool IsSafe (bool[,] Chess, int row, int col) {
            int i, j;
            for (i = 0; i < col; i++)
               if (ChessBoard[row, i]) return false;
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
               if (ChessBoard[i, j]) return false;
            for (i = row, j = col; j >= 0 && i < n; i++, j--)
               if (ChessBoard[i, j]) return false;
            return true;
         }
      }
      List<int[]> Filtered (List<int[]> solns) {
         List<int[]> list = new ();
         for (int i = 0; i < solns.Count; i++) {
            var s = solns[i];
            bool ok = true;
            for (int j = 0; j < 4; j++) {
               if (Exists (s = RotatedTo90 (s)) || Exists (s = YMirror (s))) {
                  ok = false; break;
               }
               s = YMirror (s);
            }
            if (ok) list.Add (s);
         }

         int[] RotatedTo90 (int[] m) {
            var l = new int[n];
            for (int i = 0; i < n; i++) l[m[i]] = n - i - 1;
            return l;
         }

         int[] YMirror (int[] m) {
            var l = new int[n];
            for (int i = 0; i < n; i++) l[i] = n - m[i] - 1;
            return l;
         }

         bool Exists (int[] m)
           => list.Any (a => a.SequenceEqual (m));
         return list;
      }
      void PrintAllBoards (List<int[]> solns) {
         Console.WriteLine ($"\r\n\r\nPreparing the {(choice ? "unique" : "all possible")} solutions to be printed...");
         int boardWidth = 4 * (n + 1);
         int x = (Console.BufferWidth - boardWidth) / 2, y = Console.CursorTop;
         Console.CursorVisible = false;
         for (int i = 0; i < solns.Count;) {
            int count = solns.Count;
            Console.SetCursorPosition (x + (boardWidth - 8) / 2, y + 1);
            ColorPrint ($"Sol {i + 1}/{count}".PadRight (15), Green);
            Console.SetCursorPosition (x, y + 2);
            PrintBoard (solns[i]);
            ColorPrint ($"\r\n {(i != 0 ? "<--   to go PREVIOUS" : new string(' ', 20))}\r\n {(i + 1 != count ? "-->   to go NEXT" : new string (' ', 20))}\r\n enter to EXIT", Cyan);
            var input = Console.ReadKey ().Key;
            if (i != 0 && input == LeftArrow) i--;
            else if (i != count - 1 && input == RightArrow) i++;
            else if (input == Enter) { ColorPrint ("Program terminated!\r\n", Red); break; }
         }

         void PrintBoard (int[] pos) {
            Console.OutputEncoding = Encoding.Unicode;
            for (int i = 0; i < (n + 1); i++) {
               string s = (i == 0) ? "┌┬┐" : i == n ? "└┴┘" : "├┼┤";
               Console.CursorLeft = x;
               for (int j = 0; j < (n + 1); j++) {
                  Console.Write (j == n ? $"{s[2]}\n"
                              : (j == 0 ? $"   {s[0]}" : s[1])
                              + "───");
               }
               if (i == n) break;
               Console.CursorLeft = x - 1;
               ColorPrint ($" {n - i,2} ", Yellow);
               for (int k = 0; k < (n + 1); k++)
                  Console.Write (k == n ? "│\n" : $"│ {(pos[i] == k ? "Q" : " ")} "); // Q = ♕
            }
            Console.CursorLeft = x + 2;
            for (char c = (char)65; c < (char)(65 + n); c++)
               ColorPrint ($"   {c}", Yellow);
         }
      }
      void ColorPrint (string s, ConsoleColor color) {
         Console.ForegroundColor = color;
         Console.Write (s);
         Console.ResetColor ();
      }
      #endregion
   }
}