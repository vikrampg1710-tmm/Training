// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// T26 - Implementing MyList<T>
// ---------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace Spark23;

public static class T26 {
   public static void Main () {
      var l = new MyList<int> ();
      l.Add (1);
      l.Remove (1);
      l.Insert (0, 2);
      l.RemoveAt (0);
      l.Clear ();
      Console.WriteLine (l.Count);
      Console.WriteLine (l.Capacity);
      for (int i = 11; i < 17; i++) l.Add (i);
      Console.WriteLine (l.Count);
      Console.WriteLine (l.Capacity);
   }

   /// <summary>Custom class of generic type list/> 
   public class MyList<T> {
      /// <summary>Returns the number of items present in the list</summary>
      public int Count { get => mIndex; }

      /// <summary>Returns the capacity of the list</summary>
      public int Capacity { get => list.Length; }
      public T this[int index] { 
         get => list[index]; 
         set => list[index] = value; 
      }

      /// <summary>Adds an given item to the end of the list</summary>
      public void Add (T a) {
         if (mIndex == list.Length) {
            var temp = new T[mIndex * 2];
            for (int i = 0; i < mIndex; i++) temp[i] = list[i];
            list = temp;
         }
         list[mIndex++] = a;
      }

      /// <summary>Removes the given item from the list</summary>
      public void Remove (T a) {
         if (!list.Contains (a)) throw new InvalidOperationException ("Value not found in the list");
         int pos = Array.FindIndex (list, x => x.Equals(a));
         var dummy = new T[mIndex - pos];
         for (int i = pos + 1, x = 0; i < mIndex; i++, x++) 
            dummy[x] = list[i];
         mIndex--;
         for (int i = pos, x = 0; i < mIndex; i++, x++) 
            list[i] = dummy[x];
      }

      /// <summary>Clears all the items present in the list</summary>
      public void Clear () {
         if (mIndex != 0) {
            list = new T[4];
            mIndex = 0;
         }
      }

      /// <summary>Inserts a given item at the specified index value into the list</summary>
      public void Insert (int index, T a) {
         if (index < 0) throw new IndexOutOfRangeException ("Invalid index range");
         if (index > mIndex) throw new IndexOutOfRangeException ("Index out of range");
         if (mIndex == list.Length) {
            var temp = new T[mIndex * 2];
            for (int i = 0; i < mIndex; i++) temp[i] = list[i];
            list = temp;
         }
         var dummy = new T[mIndex - index];
         for (int i = index, x = 0; i < mIndex; i++, x++) 
            dummy[x] = list[i];  // Simply, dummy = list[list[index]..]
         list[index] = a;
         for (int i = index + 1, x = 0; i < dummy.Length; i++, x++) 
            list[i] = dummy[x];
         mIndex++;
      }

      /// <summary>Removes the item located at the given index value</summary>
      public void RemoveAt (int index) {
         if (index < 0) throw new IndexOutOfRangeException ("Invalid index range");
         if (index >= mIndex) throw new IndexOutOfRangeException ("Index out of range");
         var dummy = new T[mIndex - index - 1];
         for (int i = index + 1, x = 0; i < mIndex; i++, x++) 
            dummy[x] = list[i];
         for (int i = index, x = 0; i < dummy.Length; i++, x++) 
            list[i] = dummy[x];
         mIndex--;
      }

      T[] list = new T[4]; // Array of size 4, which underlines the list data structure internally
      int mIndex = 0; // Initial index value
   }
}
