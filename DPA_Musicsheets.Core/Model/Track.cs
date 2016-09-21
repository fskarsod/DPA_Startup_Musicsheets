using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
	public class Track
	{
		public string Name { get; set; }
		public List<IMusicComponentProvider> MusicComponentProviders { get; set; }
	}
}