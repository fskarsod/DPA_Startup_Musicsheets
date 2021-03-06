﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IRepetitionBuilder
    {
        IRepetitionBuilder AddBar(Action<IBarBuilder> builderAction);

        IRepetitionBuilder AddEnding(Action<IEndingBuilder> builderAction);
    }
}
