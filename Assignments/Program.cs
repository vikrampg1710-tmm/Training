using System;
using System.Numerics;

namespace SparkTest;

class Program {
   public static void Main () {
      Random rnd = new ();
      Console.WriteLine ("Generating a random complex number: ");
      var c = new ComplexNumber (rnd.Next(-100, 100), rnd.Next (-100, 100));
      Console.WriteLine ($"x + iy = {c.RealPart} + ({c.ImaginaryPart})");
      Console.WriteLine ($"Real Part: {c.RealPart}");
      Console.WriteLine ($"Imaginary Part: {c.ImaginaryPart}");
      Console.Write ($"Magnitude: {c.Norm (c)}");
   }
}

/// <summary> Complex Number Class </summary>
public class ComplexNumber {
   private int real;
   private int imaginary;
   public ComplexNumber (int real, int imaginary) => (RealPart, ImaginaryPart) = (real, imaginary);

   public int RealPart {
      get => real;
      set => real = value;
   }

   public int ImaginaryPart {
      get => imaginary;
      set => imaginary = value;
   }

   public double Norm (ComplexNumber a) {
      var (r, i) = (a.real, a.imaginary);
      return Math.Sqrt ((r*r) + (i*i));
   }
   public static ComplexNumber operator + (ComplexNumber a, ComplexNumber b) {
      return new ComplexNumber (a.real + b.real, a.imaginary + b.imaginary);
   }

   public static ComplexNumber operator - (ComplexNumber a, ComplexNumber b) {
      return new ComplexNumber (a.real - b.real, a.imaginary - b.imaginary);
   }
}