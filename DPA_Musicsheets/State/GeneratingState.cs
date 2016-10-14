using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.State.Interface;

namespace DPA_Musicsheets.State
{
    /// <summary>
    ///     Generating state
    ///     Editor has changed and the lilypond-generation is in progress.
    /// </summary>
    public class GeneratingState : IState<string>
    {
        private readonly IState<string> _nextState;

        private bool _isGenerating;

        public GeneratingState(IState<string> nextState)
        {
            _nextState = nextState;
            _isGenerating = true; // todo: replace with ILilyPondGeneration
        }

        public void Commit(IStateable<string> stateable)
        {
            if (!_isGenerating) // generation is done
            {
                stateable.SetState(_nextState); // back to waiting state
            }
            // else do nothing
        }
    }
}
