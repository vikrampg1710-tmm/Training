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
      stack.Push (1);
      stack.Peek ();
      stack.Pop ();
   }
   public class TStack<T> {
      public bool IsEmpty => mIndex == 0;
      public int Size => mIndex;
      public void Push (T value) {
         if (mIndex == mStack.Length)
            Array.Resize (ref mStack, mIndex * 2);
         mStack[mIndex++] = value;
      }

      public T Pop () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         return mStack[--mIndex];
      }

      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         var x = mIndex - 1;
         return mStack[x];
      }

      T[] mStack = new T[4];
      int mIndex = 0;
   }
}