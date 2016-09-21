using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DPA_Musicsheets.Core.Model
{
    public class Fraction
    {
        // Top half
        public int Numerator { get; set; }

        // Bottom half
        public int Denominator { get; set; }

        public Fraction(int numberator, int denominator)
        {
            Numerator = numberator;
            Denominator = denominator;
        }

        public Fraction Simplification()
        {
            var gcd = GreatestCommonDivisor(Numerator, Denominator);
            return new Fraction(Numerator / gcd, Denominator / gcd);
        }

        private static int GreatestCommonDivisor(int a, int b)
        {
            while (b > 0)
            {
                var rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }

        public bool Equals(Fraction obj)
        {
            return Numerator == obj.Numerator
                && Denominator == obj.Denominator;
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            // if f1 is null -> return f2
            throw new NotImplementedException();
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            // if f1 is null -> return f2
            throw new NotImplementedException();
        }
    }
}
