using System;
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

        public int Repeats { get; set; }

        public Repetition()
        {
            Bars = new List<Bar>();
            Endings = new List<Ending>();
            Repeats = 2;
        }

        private IEnumerable<IMusicComponent> YieldFromBars()
        {
            return Bars.SelectMany(bar => bar.GetMusicComponents());
        }

        public IEnumerable<IMusicComponent> GetMusicComponents()
        {
            if (!Endings.Any())
            {
                for (var i = 0; i < Repeats; i++)
                {
                    foreach (var musicComponent in Bars.SelectMany(bar => bar.GetMusicComponents())) // yield the main body
                    {
                        yield return musicComponent;
                    }
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

        public string ToLilypond()
        {
            var repString = $"\\repeat volta {Math.Max(Repeats, Endings.Sum(e => e.Repeats))} {{{Environment.NewLine}";

            repString = Bars.Aggregate(repString, (current, bar) => current + bar.ToLilypond());

            if (Endings.Count > 0)
            {
                repString += $"\\alternative {{{Environment.NewLine}";
                repString = Endings.Aggregate(repString, (current, ending) => current + ending.ToLilypond());
            }

            return $"{repString}}}{Environment.NewLine}";
        }
    }
}