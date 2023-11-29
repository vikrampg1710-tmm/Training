using static Spark23.T27;

namespace Test;

[TestClass]
public class UnitTest1 {
   Random r = new ();
   [TestMethod]
   ///<summary>Checking the functioning of PUSH method</summary>
   public void PushTest () {
      TStack<int> stack1 = new ();
      Stack<int> stack2 = new ();

      Assert.IsTrue (stack1.IsEmpty);
      for (int i = 0; i < 50; i++) {
         bool push = r.Next (0, 2) == 1;
         if (push) {
            var (num1, num2) = (r.Next (0, 100), r.Next (0, 100));
            stack1.Push (num1); stack1.Push (num2);
            stack2.Push (num1); stack2.Push (num2);
         } else if (!stack1.IsEmpty) {
            stack1.Pop ();
            stack2.Pop ();
         }
         Assert.IsTrue (stack1.Count == stack2.Count);
      }
      for (int x = 0; x < stack1.Count; x++)
         Assert.AreEqual (stack1.Pop (), stack2.Pop ());
   }

   [TestMethod]
   ///<summary>Check the functioning of POP method</summary>
   public void PopTest () {
      TStack<int> stack1 = new ();
      Stack<int> stack2 = new ();
      for (int i = 0; i < 50; i++) {
         bool push = r.Next (0, 2) == 1;
         if (push) {
            int num1 = r.Next (0, 100);
            stack1.Push (num1);
            stack2.Push (num1);
         } else {
            if (stack1.IsEmpty) Assert.ThrowsException<InvalidOperationException> (() => stack1.Pop ());
            else {
               stack1.Pop ();
               stack2.Pop ();
            }
         }
      }
      for (int x = 0; x < stack1.Count; x++)
         Assert.AreEqual (stack1.Pop (), stack2.Pop ());
   }

   [TestMethod]
   ///<summary>Check the functioning of PEEK method</summary>
   public void PeekTest () {
      TStack<int> stack1 = new ();
      Stack<int> stack2 = new ();
      for (int i = 0; i < 50; i++) {
         bool push = r.Next (0, 2) == 1;
         if (push) {
            int num1 = r.Next (0, 100);
            stack1.Push (num1);
            stack2.Push (num1);
         } else if (!stack1.IsEmpty) {
            stack1.Pop ();
            stack2.Pop ();
         }
         if (stack1.IsEmpty) Assert.ThrowsException<InvalidOperationException> (() => stack1.Peek ());
         else Assert.AreEqual (stack1.Peek (), stack2.Peek ());
      }
      for (int x = 0; x < stack1.Count; x++)
         Assert.AreEqual (stack1.Pop (), stack2.Pop ());
   }
}