using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IEndingBuilder
    {
        IEndingBuilder AddBar(Action<IBarBuilder> builderAction);

        IEndingBuilder SetRepeats(int repeats);
    }
}
