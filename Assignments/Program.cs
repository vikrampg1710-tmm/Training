// ---------------------------------------------------------------------------------------
// Academy23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A10 - Double Ended Queue
// ---------------------------------------------------------------------------------------

using System;

namespace Academy;
public class A10 {
   public static void Main () {
      TDoubleEndedQueue<int> queue = new ();
      queue.FrontEnqueue (1);
      queue.BackEnqueue (2);
      queue.FrontDequeue ();
      queue.BackDequeue ();
   }

   #region DoubleEndedQueue Class -----------------------------------------------------------------
   /// <summary>
   /// Initializes a new instance of the TDoubleEndedQueue<typeparamref name="T"/> class that is empty, has the default initial capacity, and uses the default growth factor.
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class TDoubleEndedQueue<T> {
      #region Properties --------------------------------------------
      T[] mData = new T[4];
      int mCount;       // Number of elements.
      int mFront;       // The index from which to enqueue or dequeue at the beginning of the queue.
      int mBack;        // The index from which to enqueue or dequeue at the end of the queue.

      /// <summary> Returns true if the <see cref="TDoubleEndedQueue{T}"></see> has no elements in it. </summary>
      public bool IsEmpty => mCount == 0;

      /// <summary> Gets the number of elements contained in the <see cref="TDoubleEndedQueue{T}"></see>. </summary>
      public int Count => mCount;
      #endregion

      #region Methods -----------------------------------------------
      /// <summary>
      /// Removes and returns the object at the end of the<see cref="TDoubleEndedQueue{T}"></see>
      /// </summary>
      /// <returns>Returns the object at the end of the <see cref="TDoubleEndedQueue{T}"></see>after removing it</returns>
      /// <exception cref="InvalidOperationException"></exception>
      public T BackDequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty, can't dequeue.");
         T a = mData[mBack];
         mBack = (mBack - 1) % mData.Length;
         if (mBack < 0) mBack += mData.Length;
         mCount--;
         if (mCount < mData.Length / 2 && mData.Length > 4) {
            T[] tmp = new T[mData.Length / 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mFront + 1 + i) % mData.Length];
            (mData, mBack) = (tmp, mCount - 1);
            mFront = mData.Length - 1;
         }
         return a;
      }

      /// <summary>
      /// Adds an object to the end of the <see cref="TDoubleEndedQueue{T}"></see>
      /// </summary>
      /// <param name="a"></param>
      public void BackEnqueue (T a) {
         if (mCount == mData.Length) {
            T[] tmp = new T[mCount * 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mBack + 1 + i) % mCount];
            (mData, mBack) = (tmp, mCount - 1);
            mFront = mData.Length - 1;
         }
         if (IsEmpty) mBack--;
         mBack = (mBack + 1) % mData.Length;
         mData[mBack] = a;
         mCount++;
      }

      /// <summary>
      /// Removes and returns the object at the beginning of the <see cref="TDoubleEndedQueue{T}"></see>
      /// </summary>
      /// <return>Returns the object at the beginning of the <see cref="TDoubleEndedQueue{T}"></see>after removing it</return>
      /// <exception cref="InvalidOperationException"></exception>
      public T FrontDequeue () {
         if (IsEmpty) throw new InvalidOperationException ("Queue is empty, can't dequeue.");
         mFront = (mFront + 1) % mData.Length;
         T a = mData[mFront];
         mCount--;
         if (mCount < mData.Length / 2 && mData.Length > 4) {
            T[] tmp = new T[mData.Length / 2];
            for (int i = 0; i < mCount; i++)
               tmp[i] = mData[(mFront + 1 + i) % mData.Length];
            (mData, mBack) = (tmp, mCount - 1);
            mFront = mData.Length - 1;
         }
         return a;
      }

      /// <summary>
      /// Adds an object to the beginning of the <see cref="TDoubleEndedQueue{T}"></see>
      /// </summary>
      /// <param name="a"></param>
      public void FrontEnqueue (T a) {
         if (mCount == mData.Length) {
            T[] tmp = new T[mData.Length * 2];
            for (int i = 0; i < mData.Length; i++)
               tmp[i] = mData[(mFront + 1 + i) % mData.Length];
            (mData, mBack) = (tmp, mCount - 1);
            mFront = mData.Length - 1;
         }
         mData[mFront] = a;
         mFront = (mFront - 1) % mData.Length;
         if (mFront < 0) mFront += mData.Length;
         mCount++;
      }
      #endregion 
   }
   #endregion
}


