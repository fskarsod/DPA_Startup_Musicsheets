using DPA_Musicsheets.Core.Interface;

namespace DPA_Musicsheets.Core.Model
{
    public abstract class BaseNote : IMusicComponent
    {
        public int Duration { get; set; }

        public bool HasDot { get; set; }

        public virtual void Accept(IMusicComponentVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}