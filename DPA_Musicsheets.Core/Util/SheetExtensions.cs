using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Util
{
    public static class TrackExtensions
    {
        public static IEnumerable<IMusicComponent> GetMusicComponents(this Track track)
        {
            foreach (var provider in track.MusicComponentProviders)
            {
                foreach (var components in provider.GetMusicComponents())
                {
                    yield return components;
                }
            }
        }
    }
}
