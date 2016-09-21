using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IPluginWriter<out TSheet, in TSource>
    {
        TSheet WriteSheet(TSource source);
    }
}
