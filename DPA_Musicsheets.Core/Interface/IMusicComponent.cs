using System.Linq.Expressions;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IMusicComponent
    {
        void Accept(IMusicComponentVisitor visitor);
    }
}