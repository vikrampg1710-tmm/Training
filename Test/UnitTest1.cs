using Spark;

namespace Test;

[TestClass]
public class UnitTest1 {
   [TestMethod]
   // Test 1: Exception should be thrown when passing an empty array
   public void TestMethod1 () {
      var t25 = new T25 ();
      Assert.ThrowsException<Exception> (() => T25.SpecialSorted (Array.Empty<char> (), 'a'));
   }

   [TestMethod]
   // Test 2: Exception should be thrown when passing an char array with non-alphabet char
   public void TestMethod2 () {
      var t25 = new T25 ();
      Assert.ThrowsException<Exception> (() => T25.SpecialSorted (new char[] { 'a', 'b', '3', 'd', ' ' }, 'a', true));
   }

   [TestMethod]
   // Test 3: Exception should be thrown when passing an non-alphabet char as the special char
   public void TestMethod3 () {
      var t25 = new T25 ();
      Assert.ThrowsException<Exception> (() => T25.SpecialSorted (new char[] { 'x', 'y', 'z', 'w' }, '1', false));
   }

   [TestMethod]
   // Test 4: Output array should be sorted as per given order, excluding the special chars at last
   public void TestMethod4 () {
      var t25 = new T25();
      char[] input = new char[] { 't', 'r', 'U', 'm', 'P', 'M', 'E', 't', 'a', 'm', 'A', 'T', 'I', 'o', 'N' },
            output = new char[10];
      Random random = new ();
      char specialChar = 'm';
      bool sortOrder = random.Next (0, 2) == 1;
      input = sortOrder ? input.OrderBy (x => x).ToArray () : input.OrderByDescending (x => x).ToArray ();
      output = T25.SpecialSorted (input.ToArray (), specialChar, sortOrder);
      (input, output) = (input.Where (a => a != specialChar).ToArray (), output.Where (a => a != specialChar).ToArray ());
      Assert.AreEqual (new string (input), new string (output));
   }

   [TestMethod]
   // Test 5: The count of special character in the input array should be equal to the count in the output array
   public void TestMethod5 () {
      var t25 = new T25 ();
      char[] input = new char[] { 't', 'r', 'U', 'm', 'P', 'M', 'E', 't', 'a', 'm', 'A', 'T', 'I', 'o', 'N' };
      char specialChar = 'T';
      var output = T25.SpecialSorted (input.ToArray (), specialChar);
      Assert.AreEqual (input.Count (c => c == char.ToLower(specialChar) || c == char.ToUpper (specialChar)), 
         output.Count (c => c == char.ToLower (specialChar) || c == char.ToUpper (specialChar))) ;
   }

   [TestMethod]
   // Test 6: All the special characters should be placed at the end of the output array
   public void TestMethod6 () {
      var t25 = new T25 ();
      char[] input = new char[] { 't', 'r', 'U', 'm', 'P', 'M', 'E', 't', 'a', 'm', 'A', 'T', 'I', 'o', 'N' };
      char specialChar = 'r';
      int n = input.Count (c => c == specialChar);
      var output = T25.SpecialSorted (input.ToArray (), specialChar);
      Assert.AreEqual (Array.IndexOf (output, specialChar), output.Length - n);
   }
}