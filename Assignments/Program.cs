// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A6 - NQueens Problem
// ---------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A6 - NQueens Problem
// ---------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Academy23;

public class A6 {

   public static void Main () {
      // Heading:
      Console.WriteLine ("Solutions of N-Queens Problem.\n(Press ENTER to continue)");
      Console.ReadLine ();
      // Getting N value from User.
      Console.Write ("This program will display the solutions of N Queens problem.\nNow, please enter the size of chess board, N: ");
      int N;
      for (; ; ) {
         var Number = Console.ReadLine ();
         bool isNumber = int.TryParse (Number, out N);
         if (isNumber) {
            if (N == 0) DisplayInCyan ("0x0 Chess Board is not possible.  So please enter a greater number for N: ");
            else if (N == 1) DisplayInCyan ("1x1 Chess Board has no more than 1 solution.  So please enter a greater number for N: ");
            else if (N == 2 || N == 3) DisplayInCyan ($"{N}x{N} Chess Board has 0 solution.  So please enter a greater number for N: ");
            else if (N > 3) break;
         } else DisplayInCyan ("Please enter a number for N: ");
      }
      if (N > 10) Console.Write ("\nAs the number you entered is large, your computer will take a while to process.\nPlease be patience...");

      // Creating a empty NxN bool matrix.
      bool[,] ChessBoard = new bool[N, N];

      // Creating list of alphabets for the purpose of indexing the chess board.
      List<char> Alphabets = new ();
      for (char c = 'A'; c <= 'Z'; c++) {
         Alphabets.Add (c);
      }
      // Creating a list to store the list of all queens positions, after solving the ChessBoard.
      List<List<int>> ListOfPositions = new ();
      int count = 1;  // Just to count number of solutions.

      // Solving the matrix to find the all possible solutions using a RECURSIVE FUNCTION.
      SolveTheBoard (ChessBoard, 0);

      // While solving each solution, positions of queen will stored in a temporary list, which will be again added to a list called ListOfPositions.
      // After solving, converting each list from 'ListOfPositions' into bool matrices, and storing them in a separate list called AllSolutions.
      List<bool[,]> AllSolutions = new ();
      for (int i = 0; i < ListOfPositions.Count; i++)
         AllSolutions.Add (Converted (ListOfPositions[i]));

      // Now, filtering out all UNIQUE solutions by excluding mirrors and rotations and its combinations from AllSolutions list.
      // And storing them in a separate list called UniqueSolutions.
      List<bool[,]> UniqueSolutions = new ();
      for (int i = 0; i < AllSolutions.Count; i++) {
         if (IsUnique (AllSolutions[i]))
            UniqueSolutions.Add (AllSolutions[i]);
      }

      // Just to correct grammar error.
      string bverb;
      if (UniqueSolutions.Count == 1) bverb = "is";
      else bverb = "are";
      // Getting USER INPUT whether to print all solutions or only the unique solutions.
      Console.Write ($"\n{N} Queen problem has {AllSolutions.Count} different solutions."
            + $" But among them, only {UniqueSolutions.Count} {bverb} Unique."
            + $"\nDo you want to print (A)ll the solutions [or] only the (U)nique solutions? ", Console.ForegroundColor = ConsoleColor.Yellow);
      for (; ; ) {
         char choice = Char.ToUpper (Console.ReadKey ().KeyChar);
         if (choice == 'A') {
            Console.WriteLine ($"\n\nOkay, here are the all {AllSolutions.Count} solutions...[Press <Enter> to print next solution]\n");
            Thread.Sleep (1500);
            foreach (var solutions in AllSolutions) {
               PrintInBoard (solutions);
               Console.ReadLine ();
            }
            break;
         } else if (choice == 'U') {
            Console.WriteLine ($"\n\nOkay, here {bverb} the {UniqueSolutions.Count} unique solutions...\n");
            Thread.Sleep (1500);
            foreach (var solutions in UniqueSolutions) {
               PrintInBoard (solutions);
               Console.ReadLine ();
            }
            break;
         } else {
            DisplayInCyan ("\n\nPlease enter 'A' to display all solutions [or] 'U' to display only the unique solutions:  ");
            continue;
         }
      }

      //--------------------Below are the helping functions--------------------//

      // The RECURSIVE FUNCTION to solve the ChessBoard.
      bool SolveTheBoard (bool[,] ChessBoard, int col) {

         // Try placing queen in all rows one by one.
         for (int i = 0; i < N; i++) {
            // Placing the queen, if Chess[i, col] is safe.
            if (IsSafe (ChessBoard, i, col)) {
               ChessBoard[i, col] = true;
               // BACKTRACKING if placing queen at IsSafe(Chess[i, col + 1]) is ALSO true.
               if (SolveTheBoard (ChessBoard, col + 1))
                  ChessBoard[i, col] = false;
            }
         }

         // Adding the list of queen positions if ChessBoard is solved.
         if (col == N) {
            // List to note the position index of queens.
            List<int> Positions = new ();
            for (int i = 0; i < N; i++) {
               for (int j = 0; j < N; j++)
                  if (ChessBoard[i, j]) Positions.Add (j);
            }
            ListOfPositions.Add (Positions);
         }
         return true;

         // This local function will check the SAFETY of Queens to be placed.
         // (Here, it is sufficient to check only on left sides.) 
         bool IsSafe (bool[,] Chess, int row, int col) {
            int i, j;
            // Check this row on left side.
            for (i = 0; i < col; i++)
               if (ChessBoard[row, i]) return false;

            // Check upper diagonal on left side.
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
               if (ChessBoard[i, j]) return false;

            // Check lower diagonal on left side.
            for (i = row, j = col; j >= 0 && i < N; i++, j--)
               if (ChessBoard[i, j]) return false;

            return true;
         }
      }

      // This function returns TRUE only if given matrix is unique.
      // Filtering out all the ROTATED (θ = 0, 90, 180, 270 degree) matrices(R) & ROTATED + MIRRORED matrices (M + R).
      // It is sufficient to consider any one of the rotations(Horizontal or Vertical).  So the Horizontal Rotation is accounted here.
      bool IsUnique (bool[,] Matrix) {
         foreach (var matrix in UniqueSolutions) {
            if (IsEqualTo (Matrix, matrix) ||                                          // R(0)
               IsEqualTo (Matrix, RotatedTo90 (matrix)) ||                             // R(90)
               IsEqualTo (Matrix, RotatedTo90 (RotatedTo90 (matrix))) ||               // R(180)
               IsEqualTo (Matrix, RotatedTo90 (RotatedTo90 (RotatedTo90 (matrix)))) || // R(270)
               IsEqualTo (Matrix, HorizontalMirrorOf (matrix)) ||                      // M + R(0)
               IsEqualTo (Matrix, TransposeOf (matrix)) ||                             // M + R(90) [Transpose = Horizontal Mirror + 90 degree Rotation]
               IsEqualTo (Matrix, RotatedTo90 (TransposeOf (matrix))) ||               // M + R(180)
               IsEqualTo (Matrix, RotatedTo90 (RotatedTo90 (TransposeOf (matrix)))))   // M + θ(270)
               return false;
         }
         return true;
      }

      // This function converts the 1D array to a bool matrix.
      bool[,] Converted (List<int> list) {
         bool[,] Matrix = new bool[N, N];
         for (int k = 0; k < N; k++) {
            for (int i = 0; i < N; i++)
               for (int j = 0; j < N; j++) {
                  if (i == k && j == list[k])
                     Matrix[i, j] = true;
               }
         }
         return Matrix;
      }

      // This function will print a Chess board using Unicode with a matrix as input.
      void PrintInBoard (bool[,] Matrix) {
         // Encoding Unicode text to print box shapes.
         Console.OutputEncoding = Encoding.Unicode;

         // Column Index of NxN Chess Board.
         Console.Write ("     "); // For alignment.
         for (int i = 0; i < N; i++)
            DisplayInDarkYellow (Alphabets[i] + "   ");

         // Printing NxN Chess Board.
         // (1) Header:
         Console.Write ("\n   \u250c");
         for (int x = 0; x < N - 1; x++)
            Console.Write ("\u2500\u2500\u2500\u252c");
         Console.WriteLine ("\u2500\u2500\u2500\u2510");

         // (2) Queens:
         int number = N;  // Row Index of NxN Chess Board.
         for (int i = 0; i < N; i++) {
            DisplayInDarkYellow ($" {number}");
            if (number > 9) Console.Write ("\u2502");
            else Console.Write (" \u2502");
            for (int j = 0; j < N; j++) {
               Console.Write (' ');
               DisplayInYellow (Queen (Matrix[i, j]));
               Console.Write (" \u2502");
               if (j == N - 1) Console.WriteLine ();
            }

            // (3) Median:
            if (i != N - 1) {
               Console.Write ("   \u251c");
               for (int x = 0; x < N - 1; x++)
                  Console.Write ("\u2500\u2500\u2500\u253c");
               Console.WriteLine ("\u2500\u2500\u2500\u2524");
            }

            // (4) Footer:
            else {
               Console.Write ("   \u2514");
               for (int x = 0; x < N - 1; x++)
                  Console.Write ("\u2500\u2500\u2500\u2534");
               Console.WriteLine ("\u2500\u2500\u2500\u2518");
            }
            number--;
         }

         // Printing the count of solution number in Green color.
         Console.ForegroundColor = ConsoleColor.Green;
         int space = (int)(2 * N - 1.5); // For alignment.
         for (int i = 0; i < space; i++)
            Console.Write (" ");
         Console.WriteLine ($"Solution {count}\n\n");
         Console.ResetColor ();
         count++;
      }

      // This function will replace TRUE with queen symbol, FALSE with empty space.
      string Queen (bool answer) {
         if (answer == true) return "♕";
         return " ";
      }

      // This function will print the given texts in Yellow color.
      void DisplayInDarkYellow (string text) {
         Console.ForegroundColor = ConsoleColor.DarkYellow;
         Console.Write (text);
         Console.ResetColor ();
      }

      // This function will print the given texts in Yellow color.
      void DisplayInYellow (string text) {
         Console.ForegroundColor = ConsoleColor.Yellow;
         Console.Write (text);
         Console.ResetColor ();
      }

      // This function will print the given texts in Cyan color.
      void DisplayInCyan (string text) {
         Console.ForegroundColor = ConsoleColor.Cyan;
         Console.Write (text);
         Console.ResetColor ();
      }

      // This function TRANSPOSE the given Matrix.
      bool[,] TransposeOf (bool[,] Matrix) {
         bool[,] Transposed_Matrix = new bool[N, N];
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++)
               Transposed_Matrix[i, j] = Matrix[j, i];
         }
         return Transposed_Matrix;
      }

      // This function MIRRORs the given Matrix w.r.t Horizontal axis.
      bool[,] HorizontalMirrorOf (bool[,] Matrix) {
         bool[,] HMirrored_Matrix = new bool[N, N];
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++)
               HMirrored_Matrix[N - 1 - i, j] = Matrix[i, j];
         }
         return HMirrored_Matrix;
      }

      // This function rotates the matrix to 90 degree.
      bool[,] RotatedTo90 (bool[,] Matrix) {
         //R(90) of a Matrix = Transpose of Horizontally Mirrored Matrix
         bool[,] Rotated_Matrix = TransposeOf (HorizontalMirrorOf (Matrix));
         return Rotated_Matrix;
      }

      // This function compares the given two matrices.
      bool IsEqualTo (bool[,] Matrix1, bool[,] Matrix2) {
         bool Equal = true;
         for (int i = 0; i < N; i++) {
            for (int j = 0; j < N; j++)
               if (Matrix1[i, j] != Matrix2[i, j]) {
                  Equal = false; break;
               }
            if (!Equal) break;
         }
         return Equal;
      }
   }
}
