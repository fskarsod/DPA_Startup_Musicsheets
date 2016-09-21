using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;

namespace DPA_Musicsheets.Core.Builder
{
    public class NoteBuilder<TNote> : INoteBuilder<TNote>
    {
        private TNote _note;

        public NoteBuilder()
        {
            // _note = new TNote();
        }

        public INoteBuilder<TNote> SetDuration(int duration)
        {
            // _note.Duration = duration;
            // return this;
            throw new NotImplementedException();
        }

        public INoteBuilder<TNote> HasDot(bool hasDot = true)
        {
            // _note.HasDot = hasDot;
            // return this;
            throw new NotImplementedException();
        }

        public INoteBuilder<TNote> SetPitch(int pitch)
        {
            // _note.Pitch = pitch;
            // return this;
            throw new NotImplementedException();
        }

        public INoteBuilder<TNote> SetAccidental(int accidental)
        {
            // _note.Accidental = accidental;
            // return this;
            throw new NotImplementedException();
        }

        public TNote Build()
        {
            return _note;
        }
    }
}
