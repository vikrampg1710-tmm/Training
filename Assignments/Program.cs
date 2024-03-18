// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A2.2 - Reverse Number Guessing Game (LSB to MSB)
// ---------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace Academy;
public class A2 {
   public static void Main () => PerformGame ();

   /// <summary>Performs the Reverse number guess game</summary>
   public static void PerformGame () {
      Console.WriteLine ("\x1B[4m" + "Reverse Number Guessing Game:-" + "\x1B[0m"
         + "\r\nThink of a number between 1 and 100, and answer the following 7 questions so that I can guess your number");
      Console.WriteLine ("Enter to start.");
      Console.ReadKey ();
      int[] bitList = { 0, 0, 0, 0, 0, 0 },
          twoPowers = { 64, 32, 16, 8, 4, 2, 1 };

      //Question 1:
      Console.WriteLine ();
      Console.Write ("Is your number even or odd? (e/o): ");
      string firstLSD = Console.ReadLine ().ToLower ();
      if (firstLSD == "o") bitList[6] = 1;
      Console.WriteLine ($"The 1st least significant digit = {bitList[6]}");

      //Question 2 to 7:
      for (int i = 5, n = 2; i >= 0; i--, n++) {
         Console.Write ($"\nYour number mod {Math.Pow (2, n)}, = [{Math.Pow (2, n - 1)}, {Math.Pow (2, n) - 1}]? (y/n): ");
         string nthLSD = Console.ReadLine ().ToLower ();
         if (nthLSD == "y") bitList[i] = 1;
         Console.WriteLine ($"The 2nd least significant digit = {bitList[i]}");
      }
      for (int i = 0; i <= 6; i++) twoPowers[i] = twoPowers[i] * bitList[i];
      ColorText ($"\nAnswer = {string.Join ("", bitList)} = {twoPowers.Sum ()}", ConsoleColor.Green);

      // Prints the given text in specified foreground color
      static void ColorText (string text, ConsoleColor color) {
         Console.ForegroundColor = color;
         Console.WriteLine (text);
         Console.ResetColor ();
      }
   }
}