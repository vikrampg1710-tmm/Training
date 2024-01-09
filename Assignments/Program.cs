// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A2.1 - Number Guessing Game
// ---------------------------------------------------------------------------------------

using System;
using static System.ConsoleColor;
namespace Academy;
public class A2 {
   public static void Main () => PerformGame ();

   /// <summary>Performs the Number guess game</summary>
   public static void PerformGame () {
      Random r = new ();
      Console.WriteLine ("\x1B[4m" + "Number Guessing Game:-" + "\x1B[0m" 
         + "\r\nYou have 7 chances, try to guess the correct number that I have between 1 and 100.");
      int num = r.Next (1, 100);
      for (int i = 1; ; i++) {
         Console.Write ($"\r\nTry-{i}: ");
         if (int.TryParse (Console.ReadLine (), out int guess) && guess > 0 && guess < 100) {
            if (guess == num) { PrintResult (true, i, num); return; }
            ColorText ($"--> Try {(guess < num ? "high" : "low")}", Yellow);
         } else { ColorText ("Please enter a valid number number (1-99)", Cyan); i--; }
         if (i == 7) { PrintResult (false, i, num); return ; }
      }

      // Prints the final result to the player if game over
      static void PrintResult (bool isWin, int tries, int answer) {
         if (isWin) ColorText ($"You have won! You guess the number in {tries} tries", Green);
         else ColorText ($"You have lost! The answer is {answer}.", Red);
      }

      // Prints the given text in specified foreground color
      static void ColorText (string text, ConsoleColor color) {
         Console.ForegroundColor = color;
         Console.WriteLine (text);
         Console.ResetColor ();
      }
   }
}


