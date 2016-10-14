using System;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Util;

namespace DPA_Musicsheets.Core.Model
{
    public abstract class BaseNote : IMusicComponent
    {
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

        public double LengthValue => HasDot
            ? 1D / Duration * 1.5D
            : 1D / Duration;

        public virtual void Accept(IMusicComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}