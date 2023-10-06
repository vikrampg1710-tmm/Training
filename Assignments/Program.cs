// ---------------------------------------------------------------------------------------
// T13 - Strong Password
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using static System.ConsoleColor;

namespace Spark;

public class T13 {
   public static void Main () {
      Console.WriteLine ("\x1B[4m" + "Password Strength Checker:-" + "\x1B[0m");
      Console.WriteLine ("\nInstructions: Password considered to be strong only if it satisfies all the following criteria: " +
         "\n   1. Its length is at least 6. " +
         "\n   2. It contains at least one digit." +
         "\n   3. It contains at least one lowercase English character." +
         "\n   4. It contains at least one uppercase English character. " +
         "\n   5. It contains at least one special character: [!@#$%^&*()-+].");
      string output;
      (bool, string) answer;
      string[] testCases = { "   ", "1      ", "123456", "abcxyz0", "Abcd123", "@Abcd123", "PASSword-123" };
      Console.WriteLine ("\n\x1B[4m" + "Test Cases:-" + "\x1B[0m");
      for (int i = 0; i < testCases.Length; i++) {
         Console.Write ($"\n{i + 1}. {testCases[i]}", Console.ForegroundColor = Yellow);
         Console.ResetColor ();
         answer = IsStrong (testCases[i]);
         (output, Console.ForegroundColor) = answer.Item1 ? ("Strong", Green) : ("NOT Strong", Red);
         Console.WriteLine ($" - {output}");
         Console.ResetColor ();
         if (!answer.Item1) Console.WriteLine ($" Becasue, {answer.Item2}");
      }
      Console.Write ("\nNow, let's enter a password to check: ");
      answer = IsStrong (Console.ReadLine ());
      (output, Console.ForegroundColor) = answer.Item1 ? ("Strong", Green) : ("NOT Strong", Red);
      Console.WriteLine ($" - {output}");
      Console.ResetColor ();
      if (!answer.Item1) Console.WriteLine ($" Becasue, {answer.Item2}");
   }

   /// <summary>Returns TRUE if the given string is a strong password, otherwise FALSE</summary>
   public static (bool, string) IsStrong (string pswd) {
      var (strong, reason) = (true, "\b");
      var (count, spl, lower, upper, num) = (0, 0, 0, 0, 0);
      foreach (char c in pswd) {
         if (char.IsDigit (c)) count++;
         if (!(char.IsLetterOrDigit (c) || char.IsWhiteSpace (c))) spl++;
         if (char.IsLower (c)) lower++;
         if (char.IsUpper (c)) upper++;
      }
      if (pswd.Length < 6) { reason += $"\n  {++num}) Your password length is lesser than 6. It should >= 6."; strong = false; }
      if (count < 1) { reason += $"\n  {++num}) Your password has no digits. It should contain atleast one."; strong = false; }
      if (lower < 1) { reason += $"\n  {++num}) Your password has no lowercase letters, it should contain atleast one."; strong = false; }
      if (upper < 1) { reason += $"\n  {++num}) Your password has no uppercase letters, it should contain atleast one."; strong = false; }
      if (spl < 1) { reason += $"\n  {++num}) Your password has no special characters [!@#$%^&*()-+], it should contain atleast one."; strong = false; }
      return (strong, reason);
   }
}
