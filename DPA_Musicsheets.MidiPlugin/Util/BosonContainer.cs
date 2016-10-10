using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public class BosonContainer<T>
    {
        private readonly T[] _data;

        private readonly int _capacity;

        public T this[int index] => _data[index];

        public BosonContainer(int capacity)
        {
            _capacity = capacity;
            _data = new T[_capacity];
        }

        public void Queue(T elem)
        {
            PushArray();
            _data[_capacity - 1] = elem;
        }

        private void PushArray()
        {
            for(var i = 1; i < _data.Length; i++)
                _data[i - 1] = _data[i];
        }
    }
}
