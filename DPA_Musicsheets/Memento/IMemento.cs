using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Util;

namespace DPA_Musicsheets.Memento
{
    public interface IMemento<T> : INotifyPropertyChanged, IClonable<T>, IRestorable<T>
        where T : IMemento<T>
    { }
}
