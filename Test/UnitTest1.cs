using Spark23;
using System.Collections.Generic;

namespace Test;

[TestClass]
public class UnitTest1 {
   [TestMethod]
   // Test 1: Checking the functioning of Add() method 
   public void TestMethod1 () {
      T26.MyList<int> list = new ();
      var (cap1, cnt1) = (list.Capacity, list.Count);
      for (int i = 0; i < 8; i++) list.Add (i);
      var (cap2, cnt2) = (list.Capacity, list.Count);
      Assert.AreEqual (cap1 * 2, cap2);
      Assert.AreEqual (cnt1 + 8, cnt2); 
      Assert.IsTrue (list[0] == 0);
      Assert.IsTrue (list[7] == 7);
   }

   [TestMethod]
   // Test 2: Checking the functioning of Remove() method
   public void TestMethod2 () {
      T26.MyList<int> list = new ();
      int cnt = list.Count;
      list.Add (1);
      list.Add (2);
      list.Add (3);
      Assert.AreEqual (cnt + 3, list.Count);
      // Exception should be thrown when trying to remove an element with an invalid index
      Assert.ThrowsException<InvalidOperationException> (() => list.Remove (4));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (-1));
   }

   [TestMethod]
   // Test 3: Checking the functioning of index property
   public void TestMethod3 () {
      T26.MyList<int> list = new ();
      int temp;
      list.Add (1);
      list.Add (2);
      list.Add (3);
      // Exception should be thrown when trying to Get the value
      // [or] Set the index with value by an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list[4] = 1);
      Assert.ThrowsException<IndexOutOfRangeException> (() => temp = list[-1]);
   }

   [TestMethod]
   // Test 4: Checking the functioning of Clear() method
   public void TestMethod4 () {
      T26.MyList<int> list = new ();
      list.Add (1);
      list.Add (2);
      list.Add (3);
      list.Clear ();
      Assert.IsTrue (list.Count == 0);
   }

   [TestMethod]
   // Test 5: Checking the functioning of Insert() method
   public void TestMethod5 () {
      T26.MyList<int> list = new ();
      list.Add (1);
      list.Add (2);
      list.Add (3);
      int cnt = list.Count;
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.Insert (4, 4));
      list.Insert (0, 0);
      Assert.IsTrue (list[0] == 0);
      Assert.IsTrue (cnt + 1 == list.Count);
      Assert.IsTrue (list[3] == 3);
   }

   [TestMethod]
   // Test 6: Checking the functioning of RemoveAt() method
   public void TestMethod6 () {
      T26.MyList<int> list = new ();
      list.Add (1);
      list.Add (2);
      list.Add (3);
      int cnt = list.Count;
      // Exception should be thrown when trying to remove an element with an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (-1));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (4));
      list.RemoveAt (0);
      Assert.AreEqual (list.Count, cnt - 1);
      Assert.IsTrue (list[0] == 2);
     
   }
}