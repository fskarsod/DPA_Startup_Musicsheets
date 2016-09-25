﻿using System.Collections.Generic;
using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
    public class Track
    {
        public string Name { get; set; }

        public IList<IMusicComponentProvider> MusicComponentProviders { get; set; }

        public Track()
        {
            MusicComponentProviders = new List<IMusicComponentProvider>();
        }
    }
}