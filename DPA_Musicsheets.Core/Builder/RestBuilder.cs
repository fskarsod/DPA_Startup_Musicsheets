using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;

namespace DPA_Musicsheets.Core.Builder
{
    public class RestBuilder<TRest> : IRestBuilder<TRest>
    {
        private TRest _rest;

        public RestBuilder()
        {
            // _rest = new TRest();
        }

        public IRestBuilder<TRest> SetDuration(int duration)
        {
            // _rest.Duration = duration;
            // return this;
            throw new NotImplementedException();
        }

        public IRestBuilder<TRest> HasDot(bool hasDot = true)
        {
            // _rest.HasDot = hasDot;
            // return this;
            throw new NotImplementedException();
        }

        public TRest Build()
        {
            return _rest;
        }
    }
}
