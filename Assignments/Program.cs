// ---------------------------------------------------------------------------------------
// T7 - Chess Board Printing
// ---------------------------------------------------------------------------------------
using System;
using static System.ConsoleColor;

namespace Spark;

public class T7 {
   public static void Main () {
      Console.OutputEncoding = System.Text.Encoding.UTF8;
      Console.WriteLine ("\n\x1B[4m" + "Printing 8x8 Chess Board:\n" + "\x1B[0m");
      Console.Write ("In which way do you want the Chess Board be printed.\n\t1. WITH Box Unicodes\n\t2. WITHOUT Box Unicodes\nEnter (1) or (2): ");
      string choice = Console.ReadLine ();
      if (choice == "1") PrintChessBoard1 ();
      else PrintChessBoard2 ();
   }

   /// <summary>Prints 8x8 chessboard using unicodes</summary>
   public static void PrintChessBoard1 () {
      string pieces = "♜♞♝♛♚♝♞♜♟ ♖♘♗♕♔♗♘♖♙ ";
      for (int i = 0; i < 9; i++) {
         string s = (i == 0 ? "┌┬┐" : i == 8 ? "└┴┘" : "├┼┤");
         for (int j = 0; j < 9; j++)
            Console.Write (j == 8 ? $"{s[2]}\n" : (j == 0 ? $"   {s[0]}" : $"{s[1]}") + "───");
         if (i == 8) break;
         Console.ForegroundColor = Yellow;
         Console.Write ($" {8 - i} ");
         Console.ResetColor ();
         for (int k = 0; k < 9; k++) {
            int a = (i == 1 || i == 6) ? 8 : ((i == 0 || i == 7) ? k : 9);
            Console.Write (k == 8 ? "│\n" : $"│ {(i < 5 ? pieces[a] : pieces[a + 10])} ");
         }
      }
      Console.ForegroundColor = Yellow;
      Console.WriteLine ("     A   B   C   D   E   F   G   H");
      Console.ResetColor ();
   }

   /// <summary>Prints 8x8 chessboard using alternate background colours</summary>
   public static void PrintChessBoard2 () {
      string pieces = "♜♞♝♛♚♝♞♜♟ ♖♘♗♕♔♗♘♖♙ ";
      for (int i = 0; i < 8; i++) {
         Console.ForegroundColor = Yellow;
         Console.Write ($" {8 - i} ");
         Console.ResetColor ();
         for (int j = 0; j < 8; j++) {
            int a = (i == 1 || i == 6) ? 8 : ((i == 0 || i == 7) ? j : 9);
            Console.BackgroundColor = ((i + j) % 2 == 0) ? White : DarkMagenta;
            Console.ForegroundColor = Black;
            Console.Write ($" {(i < 4 ? pieces[a] : pieces[a + 10])} ");
         }
         Console.ResetColor ();
         Console.WriteLine ();
      }
      Console.ForegroundColor = Yellow;
      Console.WriteLine ("    A  B  C  D  E  F  G  H");
      Console.ResetColor ();
   }
}
