using static Academy.A15;

namespace Test;

[TestClass]
public class TestsForPriorityQueue {
   [TestMethod]
   // Test 1: To check the Heap sorting property of the priority queue.
   public void TestMethod1 () {
      List<int> list = new ();
      PriorityQueue<int> p = new ();
      Random random = new ();
      for (int i = 1; i <= 20; i++) {
         int element = random.Next (100);
         p.Enqueue (element);
         list.Add (element);
      }
      list.Sort ();
      for (int i = 0; i < 20; i++)
         Assert.AreEqual (list[i], p.Dequeue ());
   }

   [TestMethod]
   // Test 2: To check whether exception is thrown or not, when dequeuing an empty queue.
   public void TestMethod2 () {
      PriorityQueue<int> PQ = new ();
      Assert.ThrowsException<Exception> (() => PQ.Dequeue ());
   }

   [TestMethod]
   // Test 3: To check whether we can able to enqueue and dequeue any number of times.
   public void TestMethod3 () {
      List<int> list = new ();
      PriorityQueue<int> p = new ();
      Random random = new ();
      for (int i = 1; i <= 100; i++) {
         int element = random.Next (100);
         p.Enqueue (element);
         list.Add (element);
      }
      list.Sort ();
      for (int i = 0; i < 20; i++)
         Assert.AreEqual (list[i], p.Dequeue ());
      list.RemoveRange (0, 20);
      for (int i = 0; i < 20; i++) {
         int element = random.Next (100);
         p.Enqueue (element);
         list.Add (element);
      }
      list.Sort ();
      for (int i = 0; i < list.Count; i++)
         Assert.AreEqual (list[i], p.Dequeue ());
   }
}
