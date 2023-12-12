using static Academy.A10;

namespace Test;

[TestClass]
public class UnitTest1 {
   List<int> q2 = new ();
   Random r = new ();
   void Do(ref List<int> list, string perform, int item = -1) {
      if (perform == "fEnqueue") list.Insert (0, item);
      else if (perform == "bEnqueue") list.Add (item);
      else if (perform == "fDequeue") list.RemoveAt (0);
      else if (perform == "bDequeue") list.RemoveAt (list.Count - 1);
   }

   [TestMethod]
   /// <summary>Checking the functionality of FRONTENQUEUE method</summary>
   public void FrontEnqueueTest () {
      var q1 = new TDoubleEndedQueue<int> ();
      Assert.IsTrue (q1.IsEmpty);
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            var (num1, num2) = (r.Next (0, 100), r.Next (100, 200));
            q1.FrontEnqueue (num1);
            q1.FrontEnqueue (num2);
            Do (ref q2, perform: "fEnqueue", item: num1);
            Do (ref q2, perform: "fEnqueue", item: num2);
         } else if (!q1.IsEmpty) {
            q1.FrontDequeue ();
            Do (ref q2, perform: "fDequeue");
         }
         Assert.IsTrue (q1.Count == q2.Count);
      }
      for (int i = 0; i < q1.Count; i++) {
         Assert.AreEqual (q1.FrontDequeue (), q2[0]);
         Do (ref q2, perform: "fDequeue");
      }
   }

   
    [TestMethod]
   /// <summary>Checking the functionality of BACKENQUEUE method</summary>
   public void BackEnqueueTest () {
      var q1 = new TDoubleEndedQueue<int> ();
      Assert.IsTrue (q1.IsEmpty);
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            var (num1, num2) = (r.Next (0, 100), r.Next (100, 200));
            q1.BackEnqueue (num1);
            q1.BackEnqueue (num2);
            Do (ref q2, perform: "bEnqueue", item: num1);
            Do (ref q2, perform: "bEnqueue", item: num2);
         } else if (!q1.IsEmpty) {
            q1.BackDequeue ();
            Do (ref q2, perform: "bDequeue");
         }
         Assert.IsTrue (q1.Count == q2.Count);
      }
      for (int i = 0; i < q1.Count; i++) {
         Assert.AreEqual (q1.BackDequeue (), q2[^1]);
         Do (ref q2, perform: "bDequeue");
      }
   }

   
   [TestMethod]
   /// <summary>Checking the functionality of FRONTDEQUEUE method</summary>
   public void FrontDequeueTest () {
      var q1 = new TDoubleEndedQueue<int> ();
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            int num = r.Next (0, 100);
            q1.FrontEnqueue (num);
            Do (ref q2, perform: "fEnqueue", item: num);
         } else {
            if (q1.IsEmpty) Assert.ThrowsException<InvalidOperationException> (() => q1.FrontDequeue ());
            else {
               q1.FrontDequeue ();
               Do (ref q2, "fDequeue");
            }
         }
      }
      for (int i = 0; i < q1.Count; i++) {
         Assert.AreEqual (q1.FrontDequeue (), q2[0]);
         Do (ref q2, "fDequeue");
      }
   }

   [TestMethod]
   /// <summary>Checking the functionality of BACKDEQUEUE method</summary>
   public void BackDequeueTest () {
      var q1 = new TDoubleEndedQueue<int> ();
      for (int i = 0; i < 50; i++) {
         if (r.Next (0, 2) == 1) {
            int num = r.Next (0, 100);
            q1.BackEnqueue (num);
            Do (ref q2, perform: "bEnqueue", item: num);
         } else {
            if (q1.IsEmpty) Assert.ThrowsException<InvalidOperationException> (() => q1.BackDequeue ());
            else {
               q1.BackDequeue ();
               Do (ref q2, "bDequeue");
            }
         }
      }
      for (int i = 0; i < q1.Count; i++) {
         Assert.AreEqual (q1.BackDequeue (), q2[^1]);
         Do (ref q2, "bDequeue");
      }
   }
}