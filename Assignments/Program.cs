using System;
using System.IO;

namespace SparkTest;
class Program {
   static void Main () {
      string[] input = File.ReadAllLines ("C:/etc/input.txt");
      int count = 0;
      Console.WriteLine ("The valid game IDs are:");
      for (int i = 0; i < input.Length; i++) {
         bool ok = false;
         var l = input[i];
         var a = l[(l.IndexOf (':') + 1)..].Split (';');
         for (int j = 0; j < a.Length; j++) {
            var b = a[j].TrimStart();
            var c = b.Split (' ');
            var (red, green, blue) = (0, 0, 0);
            for (int k = 0; k < c.Length; k++) {
               var s = c[k].Trim(',');
               if (s == "red") red += int.Parse (c[k - 1]);
               else if (s == "green") green += int.Parse (c[k - 1]);
               else if (s == "blue") blue += int.Parse (c[k - 1]);
            }
            ok = red < 13 && green < 14 && blue < 15;
            if (!ok) break;
         }
         if (ok) { count += i + 1; Console.WriteLine ($"Game ID: {i + 1}"); }
      }
      Console.WriteLine ($"\r\nTotal = {count}");
   }
}
