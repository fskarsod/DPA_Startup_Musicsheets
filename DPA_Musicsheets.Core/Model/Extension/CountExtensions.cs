using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model.Extension
{
    public static class CountExtensions
    {
        public static int MusicComponentCount(this Track track)
        {
            return track.MusicComponentProviders.SelectMany(provider => provider.GetMusicComponents()).Count();
        }

        public static int MusicComponentCount(this IMusicComponentProvider provider)
        {
            return provider.GetMusicComponents().Count();
        }

        public static int MusicComponentOfTypeCount<T>(this Track track)
            where T : IMusicComponent
        {
            return track.MusicComponentProviders.SelectMany(provider => provider.GetMusicComponents()).OfType<T>().Count();
        }

        public static int MusicComponentOfTypeCount<T>(this IMusicComponentProvider provider)
            where T : IMusicComponent
        {
            return provider.GetMusicComponents().OfType<T>().Count();
        }
    }
}
