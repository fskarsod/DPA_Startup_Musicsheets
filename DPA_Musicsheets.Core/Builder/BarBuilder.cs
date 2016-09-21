using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;

namespace DPA_Musicsheets.Core.Builder
{
    public class BarBuilder<TBar>
        : IBarBuilder<TBar>
    {
        private TBar _bar;

        public IBarBuilder<TBar> AddNote<TNote>(Action<INoteBuilder<TNote>> builderAction)
        {
            return AddMusicComponentFromBuilder<INoteBuilder<TNote>, TNote>(builderAction, new NoteBuilder<TNote>());
        }

        public IBarBuilder<TBar> AddRest<TRest>(Action<IRestBuilder<TRest>> builderAction)
        {
            return AddMusicComponentFromBuilder<IRestBuilder<TRest>, TRest>(builderAction, new RestBuilder<TRest>());
        }

        public IBarBuilder<TBar> AddBarBoundary()
        {
            // _bar.MusicComponent.Add(new BarBoundaryBuilder().Build());
            // return this;
            throw new NotImplementedException();
        }

        private IBarBuilder<TBar> AddMusicComponentFromBuilder<TBuilder, TMusicComponent>(Action<TBuilder> builderAction, TBuilder builder)
            where TBuilder : IFluentBuilder<TMusicComponent>
        {
            // builderAction(builder);
            // _bar.MusicComponent.Add(builder.Build());
            // return this;
            throw new NotImplementedException();
        }

        public TBar Build()
        {
            return _bar;
        }
    }
}
