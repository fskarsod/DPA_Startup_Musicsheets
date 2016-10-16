using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Core.Model
{
    public class TimeSignature : IEquatable<TimeSignature>
    {
        // number above the line (6/8 -> 6)
        public int Numerator { get; set; }

        // number below the line (6/8 -> 8)
        public int Denominator { get; set; }

        private double LengthValue => 1D / Denominator;

        public double TotalLengthValue => Numerator * LengthValue;

        public TimeSignature(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public bool Equals(TimeSignature other)
        {
            return other != null
                && Numerator == other.Numerator
                && Denominator == other.Denominator;
        }

        public string ToLilypond()
        {
            return $"\\relative {Numerator}/{Denominator}";
        }
    }
}