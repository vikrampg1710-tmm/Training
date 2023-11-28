using static Spark23.T26;

namespace Test;

[TestClass]
public class UnitTest1 {
   [TestMethod]
   // Test 1: Checking the functioning of ADD method 
   public void TestMethod1 () {
      MyList<int> list = new ();
      Assert.IsTrue (list.Count == 0);
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      for (int i = 0; i < 10; i++) 
         Assert.IsTrue (list[i] == i);
   }

   [TestMethod]
   // Test 2: Checking the functioning of REMOVE method
   public void TestMethod2 () {
      MyList<int> list = new ();
      var r = new Random ();
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      Assert.ThrowsException<InvalidOperationException> (() => list.Remove (r.Next (10, 20)));
      for (int i = 0; i < 10; i++) {
         list.Remove (i);
         Assert.IsTrue (list.Count == 9 - i);
      }
      Assert.IsTrue (list.Count == 0);
      for (int i = 100; i < 125; i++) list.Add (i);
      for (int i = 0; i < 25; i++) {
         var item = list[r.Next (0, list.Count)];
         list.Remove (item);
         list.Add (i);
         Assert.IsTrue (list.Count == 25);
      }
   }

   [TestMethod]
   // Test 3: Checking the functioning of INDEX property
   public void TestMethod3 () {
      MyList<int> list = new ();
      int temp;
      for (int i = 0; i < 10; i++) list.Add (i);
      for (int i = 0; i < 10; i++)
         Assert.IsTrue (list[i] == i);

      for (int i = 0; i < 10; i++)
         list[i] = 100 + i;
      for (int i = 0; i < 10; i++)
         Assert.IsTrue (list[i] == 100 + i);

      // Exception should be thrown when trying to Get the value
      // [or] Set the index with value by an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list[list.Count] = 1);
      Assert.ThrowsException<IndexOutOfRangeException> (() => list[-1] = 5);
      Assert.ThrowsException<IndexOutOfRangeException> (() => temp = list[list.Count]);
      Assert.ThrowsException<IndexOutOfRangeException> (() => temp = list[-2]);
   }

   [TestMethod]
   // Test 4: Checking the functioning of CLEAR method
   public void TestMethod4 () {
      MyList<int> list = new ();
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count > 0);
      list.Clear ();
      Assert.IsTrue (list.Count == 0);
   }

   [TestMethod]
   // Test 5: Checking the functioning of INSERT method
   public void TestMethod5 () {
      MyList<int> list = new ();
      for (int i = 10; i < 20; i++) list.Add (i);
      for (int i = 0; i < 10; i++) {
         Assert.IsTrue (list.Count == 10 + i);
         list.Insert (0, 9 - i);
      }
      for (int i = 0; i < 20; i++) 
         Assert.IsTrue (list[i] == i);
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.Insert (list.Count + 1, 100));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.Insert (-1, 100));
   }

   [TestMethod]
   // Test 6: Checking the functioning of REMOVEAT method
   public void TestMethod6 () {
      MyList<int> list = new ();
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      for (int i = 0; i < 10; i++) {
         Assert.IsTrue (list[0] == i);
         list.RemoveAt (0);
         Assert.IsTrue (list.Count == 9 - i);
      }
      for (int i = 100; i < 125; i++) list.Add (i);
      for (int i = 0; i < 10; i++) {
         var list2 = list;
         int temp = list[i];
         list.RemoveAt (i);
         list.Insert (i, temp);
         Assert.AreEqual (list, list2);
      }
      // Exception should be thrown when trying to remove an element with an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (-1));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (list.Count));
   }
}