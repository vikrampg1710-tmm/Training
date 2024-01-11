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
      List<ConsoleKey> keys = new () { T, O, R, F, J, Backspace, LeftArrow, C, H, Enter, 
         S, H, A, R, P, Enter, K, N, O, W, S, Enter, S, T, A, R, K, Enter };
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
      Assert.AreEqual (w.mWord, w.mSecretWord);
      Assert.IsTrue (w.mComment == $"Correct! You have won in {w.mTriesCount} tries.");
      Assert.IsTrue (w.mGameOver);
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
      Assert.AreNotSame (w.mWord, w.mSecretWord);
      Assert.IsTrue (w.mComment == $"GAME OVER, you have lost! Secret word: {w.mSecretWord.ToUpper ()}");
      Assert.IsTrue (w.mTriesCount == 6);
      Assert.IsTrue (w.mGameOver);
   }

   [TestMethod]
   ///<summary>Checks the correct behavior when various keystrokes are pressed</summary>
   public void TestForStates () {
      SetConditions ();
      List<ConsoleKey> keys = new () { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P,
         Q, R, S, T, U, V, W, X, Y, Z, Tab, Spacebar, Escape };
      for (int i = 0; i < 1000; i++) {
         int num = r.Next (0, keys.Count);
         ConsoleKey c = num % 3 == 0 ? Backspace
                      : num % 3 == 1 ? Enter
                      : keys[num];
         int len1 = w.mWord.Length, tryCount1 = w.mTriesCount;
         w.UpdateGameState (c);
         int len2 = w.mWord.Length, tryCount2 = w.mTriesCount;
         if (char.IsAsciiLetter (Convert.ToChar (c)) && len2 != 5) {     // If input key is an alphabet
            Assert.AreEqual (len1 + 1, len2);
            Assert.IsTrue (w.mWords[tryCount2][len2] == '\u25cc');
         } else if (c == Enter && len2 == 5 && w.mDictWords.Contains (w.mWord)) { // If input key is ENTER
            Assert.IsTrue (tryCount2 == tryCount1 + 1);
            if (!w.mGameOver) Assert.IsTrue (len2 == 0);
         } else if (c == Backspace || c == LeftArrow) {      // If input key is BACKSPACE [or] LEFTARROW
            if (len1 != 0) {
               Assert.AreEqual (len1 - 1, len2);
               Assert.IsTrue (w.mWords[tryCount2][len2] == '\u25cc');
            }
            if (len1 != 5 && len2 != 0) Assert.IsTrue (w.mWords[tryCount2][len1] == '\u00b7');
         }
      }
   }

   void SetConditions () {
      w.mTesting = true;
      w.SetTheBoard ();
      w.mSecretWord = "STARK";
   }

   bool CheckTextFilesEqual (string f1, string f2) {
      string path = "C:/Work/Training/Test/TestFiles/", type = ".txt";
      return File.ReadAllText (path + f1 + type) == File.ReadAllText (path + f2 + type);
   }
}