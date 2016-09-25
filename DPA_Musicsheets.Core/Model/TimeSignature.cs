namespace DPA_Musicsheets.Core.Model
{
    public class TimeSignature
    {
        // number above the line (6/8 -> 6)
        public int Numerator { get; set; }

        // number below the line (6/8 -> 8)
        public int Denominator { get; set; }

        private double LengthValue { get { return 1D / Denominator; } }

        public double TotalLengthValue { get { return Numerator * LengthValue; } }

        public TimeSignature(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
    }
}