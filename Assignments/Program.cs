// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A15 - Priority Queue
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Academy;
public class A15 {
   public static void Main () {
      var p = new PriorityQueue<int> ();
   }

   #region Priority Queue Class -------------------------------------------------------------------
   /// <summary>Represents a collection of items that have a value and a priority. On dequeue, the item with the lowest priority value is removed.</summary>
   /// <typeparam name="T"></typeparam>
   public class PriorityQueue<T> where T : IComparable<T> {
      #region Propeties ---------------------------------------------
      readonly List<T> mQueue = new ();
      public bool IsEmpty => mQueue.Count == 0;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Removes and returns the minimal element from the PriorityQueue<typeparamref name="T"/> - that is, the element with the lowest priority value.</summary>
      /// <returns>Returns the element with the lowest priority value from the mQueue</returns>
      /// <exception cref="Exception"></exception>
      public T Dequeue () {
         if (IsEmpty) throw new Exception ("Queue is empty!");
         T item = mQueue[0];
         mQueue[0] = mQueue[^1];
         mQueue.RemoveAt (mQueue.Count - 1);
         SiftDown (0);
         return item;

         ///<summary>Rearranges the mQueue elements based on the priority value by sifting down the elements</summary>
         void SiftDown (int parent) {
            int child1 = parent * 2 + 1;
            int child2 = child1 + 1;
            if (child1 > mQueue.Count - 1) return; // Return if the parent has no children.
            if (child2 <= mQueue.Count - 1 && mQueue[child2].CompareTo (mQueue[child1]) < 0) child1 = child2;
            if (mQueue[child1].CompareTo (mQueue[parent]) < 0)
               (mQueue[child1], mQueue[parent]) = (mQueue[parent], mQueue[child1]);
            SiftDown (child1);
         }
      }

      /// <summary>Enqueues a sequence of elements pairs to the PriorityQueue<typeparamref name="T"/>, all associated with the specified priority.</summary>
      /// <param name="value"></param>
      public void Enqueue (T value) {
         mQueue.Add (value);
         SiftUp (mQueue.Count - 1);

         ///<summary>Rearranges the mQueue elements based on the priority value by sifting up the elements</summary>
         void SiftUp (int child) {
            if (child < 1) return;
            int parent = (child - 1) / 2;
            if (mQueue[child].CompareTo (mQueue[parent]) < 0)
               (mQueue[child], mQueue[parent]) = (mQueue[parent], mQueue[child]);
            SiftUp (parent);
         }
      }
      #endregion
   }
   #endregion
}


