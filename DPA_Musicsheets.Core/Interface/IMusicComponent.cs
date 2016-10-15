using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace DPA_Musicsheets.Core.Interface
{
    public interface IMusicComponent
    {
        void Accept(IMusicComponentVisitor visitor);

        string AsString();
    }
}