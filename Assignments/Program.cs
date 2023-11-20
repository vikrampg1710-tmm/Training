// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T28 - Queue DataType Implementation
// ---------------------------------------------------------------------------------------
using System;

namespace spark23;

public class T28 {

   public static void Main () {
      var q = new TQueue<int> ();
   }
   public class TQueue<T> {
      public void Enqueue (T a) {
         if (mCount == mData.Length) {
            // Doubling the size due to overflow
            T[] tmp = new T[mCount * 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mFront + i) % mCount];
            (mData, mFront, mBack) = (tmp, 0, mCount);
         }
         mData[mBack] = a;
         mBack = (mBack + 1) % mData.Length;
         mCount++;
      }

      public void Dequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         mFront = (mFront + 1) % mData.Length;
         mCount--;
         if (mCount < mData.Length / 2 && mData.Length > 4) {
            // Scaling down the size to half due to large vacancy
            T[] tmp = new T[mData.Length / 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mFront + i) % mData.Length];
            (mData, mFront, mBack) = (tmp, 0, mCount);
         }
      }

      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         return mData[mFront];
      }
      public bool IsEmpty => mCount == 0; // Bool function to check whether array is empty or not

      T[] mData = new T[4]; // Initially declaring an array of size 4.
      int mCount;   // Count of elements in the Queue
      int mFront;   // Index of the Front position in queue
      int mBack;    // Index of the Back position in queue
   }
}

