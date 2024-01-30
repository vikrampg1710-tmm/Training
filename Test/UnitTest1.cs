using Academy;
using static System.ConsoleKey;
namespace Test;

[TestClass]
public class WordleTest : Wordle {
   Random r = new ();
   [TestMethod]
   ///<summary>Checks the correct coloring of guessed word and keyboard letters at the bottom</summary>
   public void TestForCheckingColorCoding () {
      SetConditions ();
      TextFile = File.CreateText ("../../../../Test/TestFiles/Board_RHS.txt");
      List<ConsoleKey> keys = new () { T, O, R, F, J, Enter, Backspace, LeftArrow, C, H, Enter,
         S, H, A, R, P, Enter, K, N, O, W, S, Enter, S, T, A, R, K, Enter };
      DisplayTheBoard ();
      foreach (var c in keys) {
         UpdateGameState (c);
         DisplayTheBoard ();
      }
      Assert.IsTrue (CheckTextFilesEqual ("Board_LHS", "Board_RHS"));
   }

   [TestMethod]
   ///<summary>Checks the program handling when the correct guess is made by the player (WIN)</summary>
   public void TestForWinState () {
      SetConditions ();
      List<ConsoleKey> keyInputs = new () { S, T, A, R, K, Enter };
      foreach (var c in keyInputs) UpdateGameState (c);
      Assert.AreEqual (GuessWord, SecretWord);
      Assert.IsTrue (Comment == $"Correct! You have won in {TryCount} tries.");
      Assert.IsTrue (GameOver);
   }

   [TestMethod]
   ///<summary>Checks the program handling when the correct guess is NOT made by the player (LOSS)</summary>
   public void TestForLossState () {
      SetConditions ();
      List<ConsoleKey> keyInputs = new () { T, O, R, C, H, Enter };
      for (int i = 0; i < 6; i++) {
         foreach (var c in keyInputs)
            UpdateGameState (c);
      }
      Assert.AreNotSame (GuessWord, SecretWord);
      Assert.IsTrue (Comment == $"GAME OVER, you have lost! Secret word: {SecretWord.ToUpper ()}");
      Assert.IsTrue (TryCount == 6);
      Assert.IsTrue (GameOver);
   }

   [TestMethod]
   ///<summary>Checks the correct behavior when various keystrokes are pressed</summary>
   public void TestForStates () {
      SetConditions ();
      string[] dictWords = LoadStrings ("dict-5.txt");
      List<ConsoleKey> keys = new () { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P,
         Q, R, S, T, U, V, W, X, Y, Z, Tab, Spacebar, Escape };
      for (int i = 0; i < 1000; i++) {
         int num = r.Next (0, keys.Count);
         ConsoleKey c = num % 3 == 0 ? Backspace
                      : num % 3 == 1 ? Enter
                      : keys[num];
         int len1 = GuessWord.Length, tryCount1 = TryCount;
         UpdateGameState (c);
         int len2 = GuessWord.Length, tryCount2 = TryCount;
         // 1. If key = VALID LETTER  
         if (char.IsAsciiLetter (Convert.ToChar (c)) && len2 != 5) {
            Assert.AreEqual (len1 + 1, len2);
            Assert.IsTrue (TriedWords[tryCount2][len2] == '\u25cc');
         }
         // 2. If key = ENTER   
         else if (c == Enter && len2 == 5 && dictWords.Contains (GuessWord)) {
            Assert.IsTrue (tryCount2 == tryCount1 + 1);
            if (!GameOver) Assert.IsTrue (len2 == 0);
         }
         // 3. If key = BACKSPACE [or] LEFTARROW
         else if (c == Backspace || c == LeftArrow) {
            if (len1 != 0) {
               Assert.AreEqual (len1 - 1, len2);
               Assert.IsTrue (TriedWords[tryCount2][len2] == '\u25cc');
            }
            if (len1 != 5 && len2 != 0) Assert.IsTrue (TriedWords[tryCount2][len1] == '\u00b7');
         }
      }
   }

   [TestMethod]
   ///<summary>Checks whether the correct comment is produced or not during the game.</summary>
   public void TestForValidComment () {
      SetConditions ();
      List<ConsoleKey> keyInputs = new () { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P,
         Q, R, S, T, U, V, W, X, Y, Z };
      /* Checking the comment,
            1. when try to submit a word without sufficient letters.
            2. when try to submit an invalid word. */
      for (int i = 0, x = r.Next (1, 6); i < 25; i++) {
         for (int j = 0; j < x; j++) {
            var key = keyInputs[r.Next (0, keyInputs.Count)];
            UpdateGameState (key);
         }
         Assert.IsTrue (Comment.Trim ().Length == 0); // No comment is shown otherwise.
         UpdateGameState (Enter);
         Assert.IsTrue (Comment == (x == 5 ? "Not a valid word!" : "Not enough letters!"));
         GuessWord = string.Empty;
      }
   }

   void SetConditions () {
      Testing = true;
      ResetTheBoard ();
      SecretWord = "STARK";
   }

   bool CheckTextFilesEqual (string f1, string f2) {
      string path = Directory.GetCurrentDirectory () + "/../../../TestFiles/", type = ".txt";
      return File.ReadAllText (path + f1 + type) == File.ReadAllText (path + f2 + type);
   }
}