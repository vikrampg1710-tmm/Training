using Academy;
using static System.ConsoleKey;
namespace Test;

[TestClass]
public class WordleTest {
   Wordle w = new ();
   Random r = new ();

   [TestMethod]
   ///<summary>Checks the correct coloring of guessed word and keyboard letters at the bottom</summary>
   public void TestForCheckingColorCoding () {
      SetConditions ();
      w.testOutputFile = File.CreateText ("../../../../Test/TestFiles/Board_RHS.txt");
      List<ConsoleKey> keys = new () { T, O, R, C, J, Backspace, H, Enter, S, H, A, R, P, Enter, K, N, O, W, S, Enter, S, T, A, R, K, Enter };
      w.DisplayTheBoard ();
      foreach (var c in keys) {
         w.UpdateGameState (c);
         w.DisplayTheBoard ();
      }
      w.testOutputFile.Close ();
      Assert.IsTrue (CheckTextFilesEqual ("Board_LHS", "Board_RHS"));
   }

   [TestMethod]
   ///<summary>Checks the program handling when the correct guess is made by the player (WIN)</summary>
   public void TestForWinState () {
      SetConditions ();
      List<ConsoleKey> keyInputs = new () { S, T, A, R, K, Enter };
      foreach (var c in keyInputs) w.UpdateGameState (c);
      Assert.AreEqual (w.text, w.secretWord);
      Assert.IsTrue (w.ithComment == 2);
      Assert.IsTrue (w.gameOver);
   }

   [TestMethod]
   ///<summary>Checks the program handling when the correct guess is NOT made by the player (LOSS)</summary>
   public void TestForLossState () {
      SetConditions ();
      List<ConsoleKey> keyInputs = new () { T, O, R, C, H, Enter };
      for (int i = 0; i < 6; i++) {
         foreach (var c in keyInputs)
            w.UpdateGameState (c);
      }
      Assert.AreNotSame (w.text, w.secretWord);
      Assert.IsTrue (w.ithComment == 3);
      Assert.IsTrue (w.triesCount == 6);
      Assert.IsTrue (w.gameOver);
   }

   [TestMethod]
   ///<summary>Checks the correct behavior when various keystrokes are pressed</summary>
   public void TestForStates () {
      SetConditions ();
      List<ConsoleKey> keys = new () { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P,
         Q, R, S, T, U, Enter, V, W, X, Y, Z, Tab, Spacebar, Escape };
      for (int i = 0; i < 1000; i++) {
         int num = r.Next (0, keys.Count);
         ConsoleKey c = num % 3 == 0 ? Backspace
                      : num % 3 == 1 ? Enter
                      : keys[num];
         int len1 = w.text.Length, tryCount1 = w.triesCount;
         w.UpdateGameState (c);
         int len2 = w.text.Length, tryCount2 = w.triesCount;
         bool isLastLetter = len2 == 5;
         if (char.IsAsciiLetter (Convert.ToChar (c)) && !isLastLetter) {
            Assert.AreEqual (len1 + 1, len2);
            Assert.IsTrue (w.word[tryCount2][len2] == '\u25cc');
         } else if (c == Enter && len2 == 5 && w.dictWords.Contains (w.text)) {
            Assert.IsTrue (tryCount2 == tryCount1 + 1);
            if (!w.gameOver) Assert.IsTrue (len2 == 0);
         } else if (c == Backspace || c == LeftArrow) {
            if (len1 != 0) {
               Assert.AreEqual (len1 - 1, len2);
               Assert.IsTrue (w.word[tryCount2][len2] == '\u25cc');
            }
            if (len1 != 5 && len2 != 0) Assert.IsTrue (w.word[tryCount2][len1] == '\u00b7');
         }
      }
   }

   void SetConditions () {
      w.testing = true;
      w.SetTheBoard ();
      w.secretWord = "STARK";
   }

   bool CheckTextFilesEqual (string f1, string f2) {
      string path = "C:/Work/Training/Test/TestFiles/", type = ".txt";
      return File.ReadAllText (path + f1 + type) == File.ReadAllText (path + f2 + type);
   }
}