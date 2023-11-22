// ---------------------------------------------------------------------------------------
// Spark23 Assignments
// Copyright (c) Metamation India.
// ---------------------------------------------------------------------------------------
// Program.cs
// A4 - Spelling Bee Frequency
// ---------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Spark;
public class A4 {
   public static void Main () {
      // Importing all dictionary words from the text file "c:/etc" into an array.
      string[] discWords = File.ReadAllLines (@"c:/etc/words.txt");

      // Creating a dictionary to store the letter and its frequencies in (key, value) pairs.
      Dictionary<char, int> Frequency = new ();

      //Computing the frequencies of each letter in alphabets and updating that in the Dictionary.
      foreach (var word in discWords) {
         foreach (var c in word) {
            if (Frequency.TryGetValue (c, out int count)) Frequency[c] = count + 1;
            else if (c >= 'A' && c <= 'Z') Frequency.Add (c, 1);
         }
      }

      // Arranging the Dictionary based on the Keys.
      Console.WriteLine ("Alphabets and their occurrence in the English Dictionary:-\n");
      foreach (KeyValuePair<char, int> abc in Frequency.OrderBy (a => a.Key))
         Console.WriteLine ("Letter: {0} | Occurrence: {1}", abc.Key, abc.Value);

      //Printing the top 7 frequent letters.
      Console.WriteLine ("\nHere the top 7 frequent letters are: ");
      foreach (KeyValuePair<char, int> abc in Frequency.OrderByDescending (a => a.Value).Take (7))
         Console.WriteLine ("{0} - {1} times", abc.Key, abc.Value);
   }
}


