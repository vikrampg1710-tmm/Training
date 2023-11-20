// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T27 - Implementing Stack<T> class
// ---------------------------------------------------------------------------------------

using System;

namespace Spark23;

public class T27 {

   public static void Main () {
      var stack = new TStack<int> ();
      Console.WriteLine ("An Empty (int)stack is created.");
      Console.WriteLine ($"Is stack empty: {(stack.IsEmpty ? "Yes" : "No")}");

      // PUSH:
      Console.WriteLine ("Now, press ENTER To push int values from 1 to 3 into the stack.");
      Console.ReadLine ();
      for (int i = 1; i < 4; i++) stack.Push (i);
      Console.WriteLine ("Stack is updated.");
      Console.WriteLine ($"Is stack empty: {(stack.IsEmpty ? "Yes" : "No")}");

      // PEEK:
      Console.WriteLine ("now, press ENTER to peek.");
      Console.ReadLine ();
      Console.WriteLine ($"Recent item = {stack.Peek ()}");

      //POP:
      Console.WriteLine ("Now, press ENTER To pop.");
      for (int i = 0; i < 3; i++) {
         Console.ReadLine ();
         stack.Pop ();
         Console.WriteLine ($"Recent item = {stack.Peek ()}");
         Console.WriteLine ("ENTER To pop again.");
      }
   }
   public class TStack<T> {
      public void Push (T value) {
         if (mIndex == values.Length) {
            T[] temp = new T[mIndex * 2];
            for (int i = 0; i < mIndex; i++) temp[i] = values[i];
            values = temp;
         }
         values[mIndex++] = value;
      }

      public T Pop () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         return values[--mIndex];
      }

      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         var x = mIndex - 1;
         return values[x];
      }

      public bool IsEmpty {
         get => mIndex == 0;
      }

      T[] values = new T[4];
      int mIndex = 0;
   }
}