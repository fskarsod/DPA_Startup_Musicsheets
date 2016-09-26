using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IPluginReader<in TSheet, out TSource>
    {
        TSource ReadSheet(TSheet sheet);
    }
}
