using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
	public class Bar : IMusicComponentProvider
	{
		public TimeSignature TimeSignature { get; set; }
		public List<IMusicComponent> MusicComponents { get; set; }
	}
}