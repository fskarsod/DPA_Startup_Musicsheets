using System;

namespace DPA_Musicsheets.Core.Model
{
	public class Note : BaseNote
	{
		public Pitch Pitch { get; set; }
		public Accidental Accidental { get; set; }
		public int Octave { get; set; }
	}
}