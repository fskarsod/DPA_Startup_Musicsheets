using System;
using System.Collections.Generic;
using System.Linq;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model;

namespace DPA_Musicsheets.Core.Builder
{
    public class RestBuilder : IRestBuilder, IFluentBuilder<Rest>
    {
        private readonly Rest _rest;

        public RestBuilder()
        {
            _rest = new Rest();
        }

        public IRestBuilder SetDuration(int duration)
        {
            _rest.Duration = duration;
            return this;
        }

        public IRestBuilder HasDot(bool hasDot = true)
        {
            _rest.HasDot = hasDot;
            return this;
        }

        public Rest Build()
        {
            return _rest;
        }
    }
}
