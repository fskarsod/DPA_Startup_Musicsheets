using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.State.Interface
{
    public interface IState<in TContext>
    {
        void Commit(IStateable<TContext> stateable);
    }
}
