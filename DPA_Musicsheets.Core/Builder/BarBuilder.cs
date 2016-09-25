using System;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class BarBuilder : IBarBuilder, IFluentBuilder<Bar>
    {
        private readonly Bar _bar;

        public BarBuilder()
        {
            _bar = new Bar();
            _bar.MusicComponents.Add(new BarBoundary()); // todo: move to Bar-class? maybe no because is responsiblity of builder
        }

        public IBarBuilder SetTimeSignature(TimeSignature timeSignature)
        {
            _bar.TimeSignature = timeSignature;
            return this;
        }

        public IBarBuilder AddNote(Action<INoteBuilder> builderAction)
        {
            return AddMusicComponentFromBuilder<NoteBuilder>(new NoteBuilder(), builderAction);
        }

        public IBarBuilder AddRest(Action<IRestBuilder> builderAction)
        {
            return AddMusicComponentFromBuilder<RestBuilder>(new RestBuilder(), builderAction);
        }

        private IBarBuilder AddMusicComponentFromBuilder<TBuilder>(TBuilder builder, Action<TBuilder> builderAction)
            where TBuilder : IFluentBuilder<IMusicComponent>
        {
            builderAction(builder);
            _bar.MusicComponents.Add(builder.Build());
            return this;
        }

        public Bar Build()
        {
            return _bar;
        }
    }
}
