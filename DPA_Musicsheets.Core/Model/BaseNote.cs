using System;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Util;

namespace DPA_Musicsheets.Core.Model
{
    public abstract class BaseNote : IMusicComponent
    {
        // Technically should only be 1, 2, 4, 8, 16, 32, 64, 128, 256 etc..
        private int _duration;

        public int Duration
        {
            get { return _duration; }
            set
            {
                if (!MathClass.IsPowerOfTwo(value))
                    throw new ArgumentException("Duration is not a power of two.");
                _duration = value;
            }
        }

        public bool HasDot { get; set; }

        public double LengthValue
        {
            get
            {
                return HasDot
                    ? 1D / Duration * 1.5D
                    : 1D / Duration;
            }
        }

        public virtual void Accept(IMusicComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}