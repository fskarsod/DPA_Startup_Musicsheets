using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MidiPlugin.Util
{
    public class FixedSizeQueue<T> : Queue<T>
    {
        public int Size { get; private set; }

        public FixedSizeQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            while (Count > Size)
            {
                Dequeue();
            }
        }
    }
}
