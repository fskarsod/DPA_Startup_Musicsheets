﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder.Interface
{
    public interface IAggregateBuilder<out TBuildable, in TAddable> : IFluentBuilder<TBuildable>
    {
        IAggregateBuilder<TBuildable, TAddable> Add(TimeSignature timeSignature, TAddable element);
    }
}
