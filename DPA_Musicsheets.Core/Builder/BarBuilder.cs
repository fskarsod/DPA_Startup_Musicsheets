using System;
using System.Collections.Generic;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class BarBuilder : IBarBuilder, IFluentBuilder<Bar>
    {
        private readonly Bar _bar;

        private double _totalLengthValue;

        private double _currentLengthValue;

        public BarBuilder()
        {
            _bar = new Bar();
            _bar.MusicComponents.Add(new BarBoundary()); // todo: move to Bar-class? maybe no because is responsiblity of builder
        }

        public IBarBuilder SetTimeSignature(TimeSignature timeSignature)
        {
            if (_bar.TimeSignature != null) { throw new InvalidOperationException("TimeSignature cannot be changed within a single bar."); }
            _bar.TimeSignature = timeSignature;
            _totalLengthValue = _bar.TimeSignature.TotalLengthValue;
            _currentLengthValue = 0;
            // _bar.MusicComponents = new List<IMusicComponent>(); // todo: decide whether to reset the bar
            return this;
        }

        public IBarBuilder SetTempo(Tempo tempo)
        {
            if (_bar.Tempo != null)
                throw new InvalidOperationException("Tempo cannot be changed within a single bar.");

            _bar.Tempo = tempo;
            return this;
        }

        public IBarBuilder AddNote(Action<INoteBuilder> builderAction)
        {
            return AddBaseNoteFromBuilder<NoteBuilder>(new NoteBuilder(), builderAction);
        }

        public IBarBuilder AddComponent<TComponent>(TComponent component)
            where TComponent : BaseNote
        {
            return AppendBaseNote(component);
        }

        public IBarBuilder AddRest(Action<IRestBuilder> builderAction)
        {
            return AddBaseNoteFromBuilder<RestBuilder>(new RestBuilder(), builderAction);
        }

        private IBarBuilder AddBaseNoteFromBuilder<TBuilder>(TBuilder builder, Action<TBuilder> builderAction)
            where TBuilder : IFluentBuilder<BaseNote>
        {
            builderAction(builder);
            return AppendBaseNote(builder.Build());
        }

        private IBarBuilder AppendBaseNote(BaseNote baseNote)
        {
            _currentLengthValue += baseNote.LengthValue;
            _bar.MusicComponents.Add(baseNote);
            return this;
        }

        public Bar Build()
        {
            if (_currentLengthValue > _totalLengthValue) { throw new InvalidOperationException("Bar has too many notes."); }
            if (_currentLengthValue < _totalLengthValue) { throw new InvalidOperationException("Bar has not enough notes."); }
            return _bar;
        }
    }
}
