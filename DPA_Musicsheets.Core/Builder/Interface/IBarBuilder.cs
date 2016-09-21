using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IBarBuilder<out TBuildable>
        : IFluentBuilder<TBuildable>
    {
        IBarBuilder<TBuildable> AddNote<TNote>(Action<INoteBuilder<TNote>> builderAction);

        IBarBuilder<TBuildable> AddRest<TRest>(Action<IRestBuilder<TRest>> builderAction);

        IBarBuilder<TBuildable> AddBarBoundary();
    }
}