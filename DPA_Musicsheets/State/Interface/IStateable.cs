using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.State.Interface
{
    public interface IStateable<out TContext>
    {
        TContext Context { get; }

        void SetState(IState<TContext> state);
    }
}
