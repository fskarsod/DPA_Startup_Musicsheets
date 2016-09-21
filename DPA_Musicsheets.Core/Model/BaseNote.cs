using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
	public abstract class BaseNote : IMusicComponent
	{
		public bool HasDot { get; set; }
		public int Duration { get; set; }
	}
}