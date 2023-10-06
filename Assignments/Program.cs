// ---------------------------------------------------------------------------------------
// T7 - Chess Board Printing
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using static System.ConsoleColor;

namespace Spark;

public class T7 {
   public static void Main () {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      List<string> pieces = new () { "♜", "♞", "♝", "♛", "♚", "♝", "♞", "♜", "♟", " ", "♖", "♘", "♗", "♕", "♔", "♗", "♘", "♖", "♙", " " };
      Console.WriteLine ("\n\x1B[4m" + "Printing 8x8 Chess Board:\n" + "\x1B[0m");
      Console.Write ("In which way do you want the Chess Board be printed.\n\t1. WITH Unicode\n\t2. WITHOUT Unicode\nEnter (1) or (2): ");
      string choice = Console.ReadLine ();
      if (choice == "1") PrintChessBoard1 (pieces);
      else PrintChessBoard2 (pieces);
   }
   /// <summary>Prints 8x8 chessboard using unicodes</summary>
   public static void PrintChessBoard1 (List<string> pieces) {
      for (int i = 0; i < 9; i++) {
         string s = (i == 0 ? "┌┬┐" : i == 8 ? "└┴┘" : "├┼┤");
         for (int j = 0; j < 9; j++)
            Console.Write (j == 8 ? $"{s[2]}\n" : (j == 0 ? $"   {s[0]}" : $"{s[1]}") + "───");
         if (i == 8) break;
         Console.Write ($" {8 - i} ", Console.ForegroundColor = Yellow);
         Console.ResetColor ();
         for (int k = 0; k < 9; k++) {
            int a = (i == 1 || i == 6) ? 8 : ((i == 0 || i == 7) ? k : 9);
            Console.Write (k == 8 ? "│\n" : $"│ {(i < 5 ? pieces[a] : pieces[a + 10])} ");
         }
      }
      Console.WriteLine ("     A   B   C   D   E   F   G   H", Console.ForegroundColor = Yellow);
      Console.ResetColor ();
   }

   /// <summary>Prints 8x8 chessboard using alternate background colours</summary>
   public static void PrintChessBoard2 (List<string> pieces) {
      for (int i = 0; i < 8; i++) {
         Console.Write ($" {8 - i} ", Console.ForegroundColor = Yellow);
         Console.ResetColor ();
         for (int j = 0; j < 8; j++) {
            int a = (i == 1 || i == 6) ? 8 : ((i == 0 || i == 7) ? j : 9);
            Console.BackgroundColor = ((i + j) % 2 == 0) ? White : DarkMagenta;
            Console.Write ($" {(i < 4 ? pieces[a] : pieces[a + 10])} ", Console.ForegroundColor = Black);
         }
         Console.ResetColor ();
         Console.WriteLine ();
      }
      Console.WriteLine ("    A  B  C  D  E  F  G  H", Console.ForegroundColor = Yellow);
   }
}
