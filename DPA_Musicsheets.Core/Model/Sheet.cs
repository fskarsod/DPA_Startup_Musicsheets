using System;
using System.Collections.Generic;

namespace DPA_Musicsheets.Core.Model
{
	public class Sheet
	{
		public string Name { get; set; }
		public List<Track> Tracks { get; set; }
	}
}