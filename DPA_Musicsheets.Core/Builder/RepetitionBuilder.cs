using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;

namespace DPA_Musicsheets.Core.Builder
{
    public class RepetitionBuilder<TRepetition> : IRepetitionBuilder<TRepetition>
    {
        private TRepetition _repetition;

        public RepetitionBuilder()
        {
            // _repetition = new TRepetition();
        }

        public IRepetitionBuilder<TRepetition> Repeat(int times)
        {
            // _repetition.Times = times;
            // return this;
            throw new NotImplementedException();
        }

        public IRepetitionBuilder<TRepetition> AddBar<TBar>(Action<IBarBuilder<TBar>> builderAction)
        {
            // var builder = new BarBuilder();
            // builderAction(builder);
            // _repetition.MusicComponents.Add(builder.Build());
            // return this;
            throw new NotImplementedException();
        }

        // todo: stuff for alternative endings and stuff;

        public TRepetition Build()
        {
            return _repetition;
        }
    }
}
