using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SparkTest;
class Program {
   static void Main (string[] args) {
      string[] input = File.ReadAllLines ("C:/etc/input.txt");
      Dictionary<int, string[]> dict = new ();
      foreach (string s in input) {
         int end = s.IndexOf (':');
         int id = int.Parse(s[4..end]);
         var values = s[(end + 1)..].Split (';');
         dict[id] = values;
      }

      bool Filter (string[] strings) {
         int red = 0, green = 0, blue = 0;
         for (int i = 0; i < strings.Length; i++) {
            string s = strings[i];
            int start = 4, end = 1;
            int g = s.IndexOf ("green");
            int r = s.IndexOf ("red");
            int b = s.IndexOf ("blue");
            int gvalue = int.Parse ($"{s[(g - 4)..(g - 1)]}".Trim());
         }
         return true;
      }
   }
}
