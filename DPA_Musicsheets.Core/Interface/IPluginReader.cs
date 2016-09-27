using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IPluginReader<out TSource>
    {
        TSource ReadSheet(Sheet sheet);
    }
}
