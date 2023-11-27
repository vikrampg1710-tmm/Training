using static Spark23.T27;

namespace Test;

[TestClass]
public class UnitTest1 {
   [TestMethod]
   ///<summary>Checking the functioning of PUSH method</summary>
   public void TestMethod1 () {
      TStack<int> stack = new ();
      Assert.IsTrue (stack.IsEmpty);
      for (int i = 0; i < 10; i++) {
         Assert.AreEqual (stack.Size, i);
         stack.Push (i);
      }
      Assert.IsFalse (stack.IsEmpty);
      Assert.IsTrue (stack.Size == 10);
   }

   [TestMethod]
   ///<summary>Check the functioning of POP method</summary>
   public void TestMethod2 () {
      TStack<int> stack = new ();
      for (int i = 0; i < 10; i++) stack.Push (i);
      for (int i = 9; i >= 0; i--) {
         Assert.AreEqual (stack.Size, i + 1);
         Assert.IsTrue (stack.Pop () == i);
      }
      Assert.IsTrue (stack.IsEmpty);
      Assert.ThrowsException<InvalidOperationException> (() => stack.Pop ());
   }

   [TestMethod]
   ///<summary>Check the functioning of PEEK method</summary>
   public void TestMethod3 () {
      TStack<int> stack = new ();
      for (int i = 0; i < 10; i++) stack.Push (i);
      Assert.IsFalse (stack.IsEmpty);
      for (int i = 9; i >= 0; i--) {
         Assert.IsTrue (stack.Peek () == i);
         stack.Pop ();
      }
      Assert.IsTrue (stack.IsEmpty);
      Assert.ThrowsException<InvalidOperationException> (() => stack.Peek ());
   }
}