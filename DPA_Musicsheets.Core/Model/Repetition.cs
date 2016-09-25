using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
    public class Repetition : IMusicComponentProvider
    {
        // Main repetition
        public IList<Bar> Bars { get; set; }

        // Endings including alternative endings
        public IList<Alternative> Alternatives { get; set; }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            foreach (var alternative in Alternatives) // each alternative
            {
                for (var i = 0; i < alternative.Repeats; i++) // alternative needs to be repeated x times
                {
                    foreach (var musicComponent in Bars.SelectMany(bar => bar.GetMusicComponents())) // yield the main body
                    {
                        yield return musicComponent;
                    }
                }
                foreach (var bar in alternative.Ending.SelectMany(ending => ending.GetMusicComponents())) // yield the ending
                {
                    yield return bar;
                }
            }
        }
    }
}