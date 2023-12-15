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
   /// <summary>
   /// Represents a collection of items that have a value and a priority. 
   /// On dequeue, the item with the lowest priority value is removed.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class PriorityQueue<T> where T : IComparable<T> {
      #region Propeties ---------------------------------------------
      readonly List<T> queue = new ();
      public bool IsEmpty => queue.Count == 0;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>
      /// Removes and returns the minimal element from the PriorityQueue<typeparamref name="T"/> - that is, the element with the lowest priority value.
      /// </summary>
      /// <returns>Returns the element with the lowest priority value from the queue</returns>
      /// <exception cref="Exception"></exception>
      public T Dequeue () {
         if (IsEmpty) throw new Exception ("Queue is empty!");
         else {
            var item = queue[0];
            queue[0] = queue[^1];
            queue.RemoveAt (queue.Count - 1);
            ShiftDown (0);
            return item;
         }

         ///<summary>Rearranges the queue elements based on the priority value by shifting down the elements</summary>
         void ShiftDown (int parentIndex) {
            int child1_Index = parentIndex * 2 + 1;
            int child2_Index = child1_Index + 1;
            if (child1_Index > queue.Count - 1) return; // Return if the parent has no children.
            if (child2_Index <= queue.Count - 1 && queue[child2_Index].CompareTo (queue[child1_Index]) < 0) 
               child1_Index = child2_Index;
            if (queue[child1_Index].CompareTo (queue[parentIndex]) < 0)
               (queue[child1_Index], queue[parentIndex]) = (queue[parentIndex], queue[child1_Index]);
            ShiftDown (child1_Index);
         }
      }

      /// <summary>
      /// Enqueues a sequence of elements pairs to the PriorityQueue<typeparamref name="T"/>, all associated with the specified priority.
      /// </summary>
      /// <param name="value"></param>
      public void Enqueue (T value) {
         queue.Add (value);
         ShiftUp (queue.Count - 1);

         ///<summary>Rearranges the queue elements based on the priority value by shifting up the elements</summary>
         void ShiftUp (int childIndex) {
            if (childIndex > 0) {
               int parentIndex = (childIndex - 1) / 2;
               if (queue[childIndex].CompareTo (queue[parentIndex]) < 0)
                  (queue[childIndex], queue[parentIndex]) = (queue[parentIndex], queue[childIndex]);
               ShiftUp (parentIndex);
            }
         }
      }
      #endregion
   }
   #endregion
}


