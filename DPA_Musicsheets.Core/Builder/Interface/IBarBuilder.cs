using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;


namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IBarBuilder
    {
        IBarBuilder SetTimeSignature(TimeSignature timeSignature);

        IBarBuilder AddNote(Action<INoteBuilder> builderAction);

        IBarBuilder AddRest(Action<IRestBuilder> builderAction);
    }
}