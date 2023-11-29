using static Spark23.T28;

namespace Test;

[TestClass]
public class UnitTest1 {

   Random r = new ();

   [TestMethod]
   ///<summary>Checking the functionality of ENQUEUE method</summary>
   public void EnqueueTest () {
      var q1 = new TQueue<int> ();
      var q2 = new Queue<int> ();
      Assert.IsTrue (q1.IsEmpty);
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            var (num1, num2) = (r.Next (0, 100), r.Next (100, 200));
            q1.Enqueue (num1); q2.Enqueue (num1);
            q1.Enqueue (num2); q2.Enqueue (num2);
         } else if (!q1.IsEmpty) {
            q1.Dequeue ();
            q2.Dequeue ();
         }
         Assert.IsTrue (q1.Count == q2.Count);
      }
      for (int i = 0; i < q1.Count; i++)
         Assert.AreEqual (q1.Dequeue (), q2.Dequeue ());
   }

   [TestMethod]
   ///<summary>Checking the functionality of DEQUEUE method</summary>
   public void DequeueTest () {
      var q1 = new TQueue<int> ();
      var q2 = new Queue<int> ();
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            int num = r.Next (0, 100);
            q1.Enqueue (num);
            q2.Enqueue (num);
         } else {
            if (q1.IsEmpty) Assert.ThrowsException<InvalidOperationException> (() => q1.Dequeue ());
            else {
               q1.Dequeue ();
               q2.Dequeue ();
            }
         }
      }
      for (int i = 0; i < q1.Count; i++)
         Assert.AreEqual (q1.Dequeue (), q2.Dequeue ());
   }

   [TestMethod]
   ///<summary>Checking the functionality of PEEK method</summary>
   public void PeekTest () {
      var q1 = new TQueue<int> ();
      var q2 = new Queue<int> ();
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            int num = r.Next (0, 100);
            q1.Enqueue (num);
            q2.Enqueue (num);
         } else if (!q1.IsEmpty) {
            q1.Dequeue ();
            q2.Dequeue ();
         }
         if (q1.IsEmpty) Assert.ThrowsException<InvalidOperationException>(() => q1.Peek ());
         else Assert.AreEqual (q1.Peek (), q2.Peek ());
      }
      for (int i = 0; i < q1.Count; i++)
         Assert.AreEqual (q1.Dequeue (), q2.Dequeue ());
   }
}