// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A10 - Double Ended Queue
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Academy;
public class A10 {
   public static void Main () {
      TDoubleEndedQueue<int> q = new ();
      List<char> Letters = new ();
      for (char c = 'A'; c <= 'Z'; c++) Letters.Add (c);
      Console.WriteLine ("This is the program to perform DOUBLE-ENDED QUEUE.\n\nInitially declaring a array of size 4 as below:"); // User Guidelines:
      for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "┐\n" : (i == 0 ? "┌───" : "┬───"));   // HEADER:
      for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "│\n" : $"│   ");                      // MIDDLE:
      for (int i = 0; i <= 4; i++) Console.Write (i == 4 ? "┘\n\n" : (i == 0 ? "└───" : "┴───")); // FOOTER:
      Console.WriteLine ("INSTRUCTIONS:\n1. To FRONT Enqueue      --> TYPE 'f' as prefix to your input.  Eg: To enqueue '1', TYPE as f1.\n"
          + "2. To BACK/REAR Enqueue  --> TYPE 'b' [or] 'r' as prefix to your input.  Eg: To enqueue '1', TYPE as b1 [or] r1.\n"
          + "3. To FRONT Dequeue      --> TYPE capital F.\n"
          + "4. To BACK/REAR Dequeue  --> TYPE capital B or R.\n3. Do not use '0' as a enqueuing element.\n"
          + "5. To EXIT, TYPE the word 'Exit'\n\n");
      Console.WriteLine ("Give your input:-"); // Getting User input.
      for (; ; ) {
         Console.Write (">>> ");
         string a = Console.ReadLine ();
         if (a != null && a.Trim ().Length != 0) {
            if (a[0] == 'f' && a.Length != 1 && !Letters.Any (a[1..].ToUpper ().Contains)) q.FrontEnqueue (Convert.ToInt32 (a[1..]));
            else if (a[0] == 'b' || a[0] == 'r' && a.Length != 1) q.BackEnqueue (Convert.ToInt32 (a[1..]));
            else if (a[0] == 'F' && a.Length == 1) q.FrontDequeue ();
            else if (a[0] == 'B' || a[0] == 'R' && a.Length == 1) q.BackDequeue ();
            else if (a.ToUpper () == "EXIT") { q.Terminate (); break; } else {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine ("Incorrect input!\n");
               Console.ResetColor ();
               continue;
            }
         }
      }
   }

   // Generic Double Ended Queue Class:
   public class TDoubleEndedQueue<T> {

      readonly T b;// Value of unassigned slot in array.
      T[] Data = new T[4];
      int Count;   // Count of elements in the Queue
      int Front;   // Index of the Front position in queue
      int Back;    // Index of the Back position in queue
      public int EnqueueCount;  // Index to count no. of enqueue operations.
      int DequeueCount;  // Index to count no. of dequeue operations.

      // Front Enqueue Function:
      public void FrontEnqueue (T a) {
         if (Count == Data.Length) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ("Queue Overflow! Hence, Scaling-up array.");
            Console.ResetColor ();
            T[] tmp = new T[Data.Length * 2];
            for (int i = 0; i < Data.Length; i++)
               tmp[i] = Data[(Front + 1 + i) % Data.Length];
            (Data, Back) = (tmp, Count - 1);
            Front = Data.Length - 1;
         }
         Data[Front] = a;
         Front = (Front - 1) % Data.Length;
         if (Front < 0) Front += Data.Length;
         Count++; EnqueueCount++;
         DisplayElements (Data);
         Console.WriteLine ();
      }

      // Back Enqueue Function:
      public void BackEnqueue (T a) {

         if (Count == Data.Length) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ("Queue Overflow! Hence, Scaling-up array.");
            Console.ResetColor ();
            T[] tmp = new T[Count * 2];
            for (int i = 0; i < Count; i++)
               tmp[i] = Data[(Back + 1 + i) % Count];
            (Data, Back) = (tmp, Count - 1);
            Front = Data.Length - 1;
         }
         if (IsEmpty) Back--;
         Back = (Back + 1) % Data.Length;
         Data[Back] = a;
         Count++; EnqueueCount++;
         DisplayElements (Data);
         Console.WriteLine ();
      }

      // Front Dequeue Function:
      public void FrontDequeue () {
         if (IsEmpty) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Queue Empty!");
            Console.ResetColor ();
            DisplayElements (Data);
            Console.WriteLine ();
         } else {
            Front = (Front + 1) % Data.Length;
            T a = Data[Front];
            Data[Front] = b;
            if (!IsEmpty) {
               Console.ForegroundColor = ConsoleColor.Blue;
               Console.WriteLine ($"Dequeuing {a}");
               Console.ResetColor ();
            }
            Count--; DequeueCount++;
            if (Count < Data.Length / 2 && Data.Length > 4) {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine ("Queue is half empty! Hence, Scaling-down array.");
               Console.ResetColor ();
               T[] tmp = new T[Data.Length / 2];
               for (int i = 0; i < Count; i++)
                  tmp[i] = Data[(Front + 1 + i) % Data.Length];
               (Data, Back) = (tmp, Count - 1);
               Front = Data.Length - 1;
            }
            DisplayElements (Data);
            Console.WriteLine ();
         }
      }

      // Back Dequeue Function:
      public void BackDequeue () {
         if (IsEmpty) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Queue Empty!");
            Console.ResetColor ();
            DisplayElements (Data);
            Console.WriteLine ();
         } else {
            T a = Data[Back];
            Data[Back] = b;
            Back = (Back - 1) % Data.Length;
            if (Back < 0) Back += Data.Length;
            if (!IsEmpty) {
               Console.ForegroundColor = ConsoleColor.Blue;
               Console.WriteLine ($"Dequeuing {a}");
               Console.ResetColor ();
            }
            Count--; DequeueCount++;
            if (Count < Data.Length / 2 && Data.Length > 4) {
               Console.ForegroundColor = ConsoleColor.Green;
               Console.WriteLine ("Queue is half empty! Hence, Scaling-down array.");
               Console.ResetColor ();
               T[] tmp = new T[Data.Length / 2];
               for (int i = 0; i < Count; i++)
                  tmp[i] = Data[(Front + 1 + i) % Data.Length];
               (Data, Back) = (tmp, Count - 1);
               Front = Data.Length - 1;
            }
            DisplayElements (Data);
            Console.WriteLine ();
         }
      }
      public bool IsEmpty => Count == 0; // Bool function to check whether array is empty or not.

      // Function to Display array using Unicode
      public void DisplayElements (T[] a) {
         for (int i = 0; i <= a.Length; i++) 
            Console.Write (i == a.Length ? "┐\n" : (i == 0 ? $"┌──{Align ("─", StringOf (a[i]))}" : $"┬──{Align ("─", StringOf (a[i]))}")); // HEADER
         for (int i = 0; i <= a.Length; i++) 
            Console.Write (i == a.Length ? "│\n" : $"│ {StringOf (a[i])} ");                                                                // MIDDLE
         for (int i = 0; i <= a.Length; i++) 
            Console.Write (i == a.Length ? "┘\n" : (i == 0 ? $"└──{Align ("─", StringOf (a[i]))}" : $"┴──{Align ("─", StringOf (a[i]))}")); // FOOTER

         // Local Function to return space or array element as string.
         string StringOf (T s)
           => Equals (s, b) ? " " : s.ToString ();

         // Local Function for aligning the box sizes according to the filler.
         string Align (string s1, string s2)
            => string.Concat (Enumerable.Repeat (s1, s2.Length));
      }

      // Function to Display texts after user terminated the program.
      public void Terminate () {
         Thread.Sleep (1000);
         Console.ForegroundColor = ConsoleColor.Red;
         Console.WriteLine ("PROGRAM TERMINATED!\n");
         Console.ResetColor ();
         Console.WriteLine ($"So far, you have performed {EnqueueCount} Enqueue operations & {DequeueCount} Dequeue operations.\n==> Totally {EnqueueCount + DequeueCount} Enqueue-Dequeue operations.");
      }
   }
}


