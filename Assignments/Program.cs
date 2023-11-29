// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T27 - Implementing Stack<T> class
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark23;

public class T27 {
   public static void Main () {
      var st = new Stack<int> ();
   }

   #region Custom Generic Class of Stack: TStack<T>
   /// <summary>Initiates a new instance of the TStack<typeparamref name="T"/> that is empty and has the default initial capacity</summary>
   public class TStack<T> {

      /// <summary>Returns true if the stack is empty</summary>
      public bool IsEmpty => mIndex == 0;

      /// <summary>Gets the number of elements contained in the stack</summary>
      public int Count => mIndex;

      /// <summary>Returns the object on top of the TStack<typeparamref name="T"/> without removing it</summary>
      /// <returns>The object removed from the top of the TStack<typeparamref name="T"/></returns>
      /// <exception cref="InvalidOperationException"></exception>
      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         return mStack[mIndex - 1];
      }

      /// <summary>Removes and returns the object on top of the TStack<typeparamref name="T"/></summary>
      /// <returns>The object removed from the top of the TStack<typeparamref name="T"/></returns>
      /// <exception cref="InvalidOperationException"></exception>
      public T Pop () {
         if (IsEmpty) throw new InvalidOperationException ("Stack Empty");
         return mStack[--mIndex];
      }

      /// <summary>Inserts an object on top of the TStack<typeparamref name="T"/></summary>
      public void Push (T value) {
         if (mIndex == mStack.Length)
            Array.Resize (ref mStack, mIndex * 2);
         mStack[mIndex++] = value;
      }
      T[] mStack = new T[4];
      int mIndex = 0;
   }
   #endregion
}