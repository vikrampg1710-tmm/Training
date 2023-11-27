// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T26 - Implementing MyList<T>
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Spark23;

public static class T26 {
   public static void Main () {
      MyList<int> list = new ();
   }

   /// <summary>Custom class of generic type list/> 
   public class MyList<T> {
      /// <summary>Returns the number of items present in the list</summary>
      public int Count => mIndex;

      /// <summary>Returns the capacity of the list</summary>
      public int Capacity => mList.Length;

      /// <summary>Property: Gets the value of specified index from the list 
      ///  [or] Sets the value to the specified index of the list</summary>
      public T this[int index] {
         get {
            if (index < 0 || index >= mIndex)
               throw new IndexOutOfRangeException ("Index was out of range. Must be non-negative and less than the size of the collection.");
            return mList[index];
         }
         set {
            if (index < 0 || index >= mIndex)
               throw new IndexOutOfRangeException ("Index was out of range. Must be non-negative and less than the size of the collection.");
            mList[index] = value;
         }
      }

      /// <summary>Adds an given item to the end of the list</summary>
      public void Add (T a) {
         if (mIndex == mList.Length) Array.Resize (ref mList, mIndex * 2);
         mList[mIndex++] = a;
      }

      /// <summary>Removes the given item from the list</summary>
      public void Remove (T a) {
         if (!mList.Contains (a)) throw new InvalidOperationException ("Value not found in the list");
         int index = 0;
         for (int i = 0; i < mIndex; i++) {
            if (mList[i].Equals (a)) index = i;
         }
         for (int i = index; i < mIndex - 2; i++)
            (mList[i], mList[index + 1]) = (mList[index + 1], mList[i + 2]);
         mIndex--;
         if (index == mIndex) return;
         mList[mIndex - 1] = mList[mIndex - 1];
      }

      /// <summary>Clears all the items present in the list</summary>
      public void Clear () {
         mIndex = 0;
      }

      /// <summary>Inserts a given item at the specified index value into the list</summary>
      public void Insert (int index, T a) {
         if (index < 0 || index > mIndex)
            throw new IndexOutOfRangeException ("Index was out of range. Must be non-negative and less than the size of the collection.");
         if (mIndex == mList.Length) Array.Resize (ref mList, mIndex * 2);
         var (t1, t2) = (a, mList[index]);
         for (int i = index; i < mIndex; i++)
            (mList[i], t1, t2) = (t1, t2, mList[(i + 1) % mIndex]);
         mList[mIndex++] = t1;
      }

      /// <summary>Removes the item located at the given index value</summary>
      public void RemoveAt (int index) {
         if (index < 0 || index >= mIndex)
            throw new IndexOutOfRangeException ("Index was out of range. Must be non-negative and less than the size of the collection.");
         T tmp = mList[index + 1];
         for (int i = index; i < mIndex - 2; i++)
            (mList[i], mList[index + 1]) = (mList[index + 1], mList[i + 2]);
         mIndex--;
         if (index == mIndex) return;
         mList[mIndex - 1] = mList[mIndex - 1];
      }

      T[] mList = new T[4]; // Array of size 4, which underlines the list data structure internally
      int mIndex = 0; // Initial index value
   }
}
