using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Interface
{
    public interface ISheetReader<in T>
        where T : class
    {
        Sheet ReadSheet(T source);
    }
}
