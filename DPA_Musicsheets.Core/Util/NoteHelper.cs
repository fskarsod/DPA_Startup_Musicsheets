using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Util
{
    public static class NoteHelper
    {
        public static string ModifierStringified(Modifier modifier)
        {
            switch (modifier)
            {
                case Modifier.Flat:
                    return "es";
                case Modifier.Sharp:
                    return "is";
                default:
                    return string.Empty;
            }
        }
    }
}
