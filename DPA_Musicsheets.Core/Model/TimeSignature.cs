namespace DPA_Musicsheets.Core.Model
{
    public class TimeSignature
    {
        // number above the line (6/8 -> 6)
        public int Numerator { get; set; }

        // number below the line (6/8 -> 8)
        public int Denominator { get; set; }
    }
}