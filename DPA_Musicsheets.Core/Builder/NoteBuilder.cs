using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Core.Builder.Interface;
using DPA_Musicsheets.Core.Model;
using DPA_Musicsheets.Core.Model.Enum;

namespace DPA_Musicsheets.Core.Builder
{
    public class NoteBuilder : INoteBuilder, IFluentBuilder<Note>
    {
        private readonly Note _note;

        public NoteBuilder()
        {
            _note = new Note();
        }

        public INoteBuilder SetDuration(int duration)
        {
            _note.Duration = duration;
            return this;
        }

        public INoteBuilder HasDot(bool hasDot = true)
        {
            _note.HasDot = hasDot;
            return this;
        }

        public INoteBuilder SetPitch(Pitch pitch)
        {
            _note.Pitch = pitch;
            return this;
        }

        public INoteBuilder SetAccidental(Accidental accidental)
        {
            _note.Accidental = accidental;
            return this;
        }

        public Note Build()
        {
            return _note;
        }
    }
}
