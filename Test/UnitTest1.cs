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
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      Assert.ThrowsException<InvalidOperationException> (() => list.Remove (100));
      for (int i = 0; i < 10; i++) {
         list.Remove (0);
         Assert.IsTrue (list.Count == 9 - i);
      }
      Assert.IsTrue (list.Count == 0);
   }

   [TestMethod]
   // Test 3: Checking the functioning of index PROPERTY
   public void TestMethod3 () {
      MyList<int> list = new ();
      int temp;
      for (int i = 0; i < 10; i++) list.Add (i);
      // Exception should be thrown when trying to Get the value
      // [or] Set the index with value by an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list[11] = 1);
      Assert.ThrowsException<IndexOutOfRangeException> (() => list[-1] = 5);
      Assert.ThrowsException<IndexOutOfRangeException> (() => temp = list[20]);
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
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      for (int i = 0; i < 10; i++) {
         Assert.IsTrue (list.Count == 10 + i);
         list.Insert (0, 100 + i);
      }
      for (int i = 0; i < 10; i++) 
         Assert.IsTrue (list[i] == 109 - i);
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.Insert (25, 4));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.Insert (-1, 100));
   }

   [TestMethod]
   // Test 6: Checking the functioning of REMOVEAT method
   public void TestMethod6 () {
      MyList<int> list = new ();
      for (int i = 0; i < 10; i++) list.Add (i);
      Assert.IsTrue (list.Count == 10);
      for (int i = 0; i < 10; i++) {
         list.RemoveAt (0);
         Assert.IsTrue (list.Count == 9 - i);
      }
      Assert.IsTrue (list.Count == 0);
      // Exception should be thrown when trying to remove an element with an invalid index
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (-1));
      Assert.ThrowsException<IndexOutOfRangeException> (() => list.RemoveAt (0));
   }
}