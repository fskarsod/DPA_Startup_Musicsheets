using System;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class EndingBuilder : IEndingBuilder, IFluentBuilder<Ending>
    {
        private readonly Ending _ending;

        public EndingBuilder()
        {
            _ending = new Ending();
            _ending.Repeats = 1;
        }

        public IEndingBuilder AddBar(Action<IBarBuilder> builderAction)
        {
            var builder = new BarBuilder();
            builderAction(builder);
            _ending.Bars.Add(builder.Build());
            return this;
        }

        public IEndingBuilder SetRepeats(int repeats)
        {
            _ending.Repeats = repeats;
            return this;
        }

        public Ending Build()
        {
            return _ending;
        }
    }
}
