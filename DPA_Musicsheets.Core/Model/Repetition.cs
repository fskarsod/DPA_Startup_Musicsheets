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
        public IList<Ending> Endings { get; set; }

        public Repetition()
        {
            Bars = new List<Bar>();
            Endings = new List<Ending>();
        }

        private IEnumerable<IMusicComponent> YieldFromBars()
        {
            return Bars.SelectMany(bar => bar.GetMusicComponents());
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            if (!Endings.Any())
            {
                foreach (var musicComponent in Bars.SelectMany(bar => bar.GetMusicComponents())) // yield the main body
                {
                    yield return musicComponent;
                }
            }
            else
            {
                foreach (var ending in Endings) // each alternative
                {
                    for (var i = 0; i < ending.Repeats; i++) // alternative needs to be repeated x times
                    {
                        foreach (var musicComponent in Bars.SelectMany(bar => bar.GetMusicComponents())) // yield the main body
                        {
                            yield return musicComponent;
                        }
                        foreach (var bar in ending.Bars.SelectMany(bar => bar.GetMusicComponents())) // yield the ending
                        {
                            yield return bar;
                        }
                    }
                }
            }
        }
    }
}