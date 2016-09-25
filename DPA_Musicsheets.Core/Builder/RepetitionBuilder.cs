using System;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class RepetitionBuilder : IRepetitionBuilder, IFluentBuilder<Repetition>
    {
        private readonly Repetition _repetition;

        public RepetitionBuilder()
        {
            _repetition = new Repetition();
        }

        public IRepetitionBuilder AddBar(Action<IBarBuilder> builderAction)
        {
            var builder = new BarBuilder();
            builderAction(builder);
            _repetition.Bars.Add(builder.Build());
            return this;
        }

        public IRepetitionBuilder AddEnding(Action<IEndingBuilder> builderAction)
        {
            var builder = new EndingBuilder();
            builderAction(builder);
            _repetition.Alternatives.Add(builder.Build());
            return this;
        }

        public Repetition Build()
        {
            return _repetition;
        }
    }
}
