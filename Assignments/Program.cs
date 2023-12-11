// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T28 - Queue DataStructure Implementation
// ---------------------------------------------------------------------------------------
using System;

namespace Spark23;

public class T28 {
   public static void Main () {
      var queue = new TQueue<int> ();
      queue.Enqueue (1);
      queue.Dequeue ();
      queue.Peek ();
   }

   #region TQueue Class ---------------------------------------------------------------------------
   /// <summary>
   /// Initiates a new instance of the TQueue class that is empty, and has the default initial capacity, and uses the default growth factor
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class TQueue<T> {

      #region Properties --------------------------------------------
      T[] mData = new T[4];
      int mCount;
      int mFront;
      int mBack;
      public int Count => mCount;
      public bool IsEmpty => mCount == 0;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>Removes and returns the object at the beginning of the TQueue</summary>
      /// <returns>The object that is removed from the beginning of the TQueue</returns>
      /// <param name="a"></param>
      public T Dequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         T item = mData[mFront];
         mFront = (mFront + 1) % mData.Length;
         mCount--;
         if (mCount < mData.Length / 2 && mData.Length > 4) {
            // Scaling down the size to half due to large vacancy
            T[] tmp = new T[mData.Length / 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mFront + i) % mData.Length];
            (mData, mFront, mBack) = (tmp, 0, mCount);
         }
         return item;
      }

      /// <summary>Adds an object to the end of TQueue</summary>
      /// <exception cref="InvalidOperationException"></exception>
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

      /// <summary>Returns the object at the beginning of the TQueue without removing it</summary>
      /// <returns>The object that at the beginning of the TQueue</returns>
      /// <exception cref="InvalidOperationException"></exception>
      public T Peek () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
         return mData[mFront];
      }
      #endregion
   }
   #endregion
}

