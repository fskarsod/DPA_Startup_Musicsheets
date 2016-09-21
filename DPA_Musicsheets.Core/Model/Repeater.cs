using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
	public class Repeater : IMusicComponentProvider
	{
		public List<Bar> Bars { get; set; }
	}
}