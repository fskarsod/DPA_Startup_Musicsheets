using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.State.Interface;

namespace DPA_Musicsheets.State
{
    /// <summary>
    ///     Default state
    ///     Editor has not been used and lilypond-generation is actual.
    /// </summary>
    public class WaitingState : IState<string>
    {
        private readonly IState<string> _nextState;

        public WaitingState(IState<string> nextState)
        {
            _nextState = nextState;
        }

        public void Commit(IStateable<string> stateable)
        {
            // todo: start generating lilypond
            stateable.SetState(_nextState);
        }
    }
}
