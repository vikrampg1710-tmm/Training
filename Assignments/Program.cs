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

   public class PriorityQueue<T> where T : IComparable<T> {
      readonly List<T> Queue = new ();
      public void Enqueue (T value) {
         Queue.Add (value);
         SiftUp (Queue.Count - 1);

         void SiftUp (int ChildIndex) {
            if (ChildIndex > 0) {
               int ParentIndex = (ChildIndex - 1) / 2;
               if (Queue[ChildIndex].CompareTo (Queue[ParentIndex]) < 0)
                  (Queue[ChildIndex], Queue[ParentIndex]) = (Queue[ParentIndex], Queue[ChildIndex]);
               SiftUp (ParentIndex);
            }
         }
      }

      public T Dequeue () {
         if (IsEmpty) throw new Exception ("Queue Empty");
         else {
            var item = Queue[0];
            Queue[0] = Queue[^1];
            Queue.RemoveAt (Queue.Count - 1);
            SiftDown (0);
            return item;
         }

         void SiftDown (int ParentIndex) {
            int Child1_Index = ParentIndex * 2 + 1;
            int Child2_Index = Child1_Index + 1;
            if (Child1_Index > Queue.Count - 1) return; // Return if the parent has no children.
            if (Child2_Index <= Queue.Count - 1 && Queue[Child2_Index].CompareTo (Queue[Child1_Index]) < 0) 
               Child1_Index = Child2_Index;
            if (Queue[Child1_Index].CompareTo (Queue[ParentIndex]) < 0)
               (Queue[Child1_Index], Queue[ParentIndex]) = (Queue[ParentIndex], Queue[Child1_Index]);
            SiftDown (Child1_Index);
         }
      }
      public bool IsEmpty {
         get => Queue.Count == 0;
      }
   }
}


