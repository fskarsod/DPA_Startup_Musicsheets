namespace DPA_Musicsheets.Core.Model
{
    public class Tempo
    {
        public int NoteDuration { get; }

        public int Bpm { get; }

        public string TempoString => NoteDuration + "=" + Bpm;

        public Tempo(int noteDuration, int bpm)
        {
            NoteDuration = noteDuration;
            Bpm = bpm;
        }
    }
}