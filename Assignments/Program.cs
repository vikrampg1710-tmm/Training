// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T28 - Queue DataType Implementation
// ---------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

TQueue<int> q = new ();

// List of Alphabets:
List<char> Letters = new ();
for (char c = 'A'; c <= 'Z'; c++) Letters.Add (c);

// User Guidelines:
Console.WriteLine ("This is the program to perform QUEUE actions.\n\nLet's use an Array to perform the action and initially declaring a array of size 4 as below:");
for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "┐\n" : (i == 0 ? "┌───" : "┬───"));   // HEADER:
for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "│\n" : $"│   ");                      // MIDDLE:
for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "┘\n\n" : (i == 0 ? "└───" : "┴───")); // FOOTER:
Console.WriteLine ("INSTRUCTIONS:\n1. To Enqueue --> use letter 'e' as prefix to your input.  Eg: To enqueue '1', TYPE as e1.\n"
    + "2. To Dequeue --> TYPE letter 'd'.\n3. Do not use '0' as a enqueuing element.\n4. To exit, TYPE the word 'Exit'\n\n");

// Getting User input.
Console.WriteLine ("Give your input:-");
for (; ; ) {
   Console.Write (">>> ");
   string a = Console.ReadLine ()!.ToUpper ().Trim ();
   if (a.Length == 0) continue;
   if (a[0] == 'E' && a.Length != 1 && !Letters.Any (a[1..].Contains) && a[1..] != "0") q.Enqueue (Convert.ToInt32 (a[1..]));
   else if (a[0] == 'D' && a.Length == 1) q.Dequeue ();
   else if (a.ToUpper () == "EXIT") {
      Thread.Sleep (1000);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine ("PROGRAM TERMINATED!\n");
      Console.ResetColor ();
      break;
   } else {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine ("Incorrect input!\n");
      Console.ResetColor ();
      continue;
   }
}

// A Generic Class of Queue:
public class TQueue<T> {

   readonly T b;// Value of unassigned slot in array
   T[] Data = new T[4]; // Initially declaring an array of size 4.
   int Count;   // Count of elements in the Queue
   int Front;   // Index of the Front position in queue
   int Back;    // Index of the Back position in queue

   // Enqueue Function:
   public void Enqueue (T a) {
      if (Count == Data.Length) {
         Console.ForegroundColor = ConsoleColor.Green;
         Console.WriteLine ("Queue Overflow! Hence, Scaling-up array.");
         Console.ResetColor ();
         T[] tmp = new T[Count * 2];
         for (int i = 0; i < Count; i++)
            tmp[i] = Data[(Front + i) % Count];
         (Data, Front, Back) = (tmp, 0, Count);
      }
      Data[Back] = a;
      Back = (Back + 1) % Data.Length;
      Count++;
      DisplayElements (Data);
      Console.WriteLine ();
   }

   // Dequeue Function:
   public void Dequeue () {
      if (IsEmpty) {
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ("Queue Empty!");
         Console.ResetColor ();
         DisplayElements (Data);
         Console.WriteLine ();
      } else {
         T a = Data[Front];
         Data[Front] = b;
         Front = (Front + 1) % Data.Length;
         if (!IsEmpty) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine ($"Dequeuing {a}");
            Console.ResetColor ();
         }
         Count--;
         if (Count < Data.Length / 2 && Data.Length > 4) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ("Queue is half empty! Hence, Scaling-down array.");
            Console.ResetColor ();
            T[] tmp = new T[Data.Length / 2];
            for (int i = 0; i < Count; i++)
               tmp[i] = Data[(Front + i) % Data.Length];
            (Data, Front, Back) = (tmp, 0, Count);
         }
         DisplayElements (Data);
         Console.WriteLine ();
      }
   }

   public bool IsEmpty => Count == 0; // Bool function to check whether array is empty or not.

   // Function to Display array using Unicode
   public void DisplayElements (T[] a) {
      for (int i = 0; i <= a.Length; i++)
         Console.Write (i == a.Length ? "┐\n" : (i == 0 ? $"┌──{AlignmentFor ("─", StringOf (a[i]))}" : $"┬──{AlignmentFor ("─", StringOf (a[i]))}")); // HEADER
      for (int i = 0; i <= a.Length; i++)
         Console.Write (i == a.Length ? "│\n" : $"│ {StringOf (a[i])} ");                                                                              // MIDDLE
      for (int i = 0; i <= a.Length; i++)
         Console.Write (i == a.Length ? "┘\n" : (i == 0 ? $"└──{AlignmentFor ("─", StringOf (a[i]))}" : $"┴──{AlignmentFor ("─", StringOf (a[i]))}")); // FOOTER

      // Local Function to return space or array element as string.
      string StringOf (T s) {
         if (Equals (s, b)) return " ";
         return Convert.ToString (s)!;
      }

      // Local Function for aligning the box sizes according to the filler.
      string AlignmentFor (string s1, string s2) {
         return string.Concat (Enumerable.Repeat (s1, s2.Length));
      }
   }
}