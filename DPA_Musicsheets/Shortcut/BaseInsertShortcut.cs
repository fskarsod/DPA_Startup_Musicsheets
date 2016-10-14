using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.Shortcut
{
    public abstract class BaseInsertShortcut : BaseShortcut
    {
        private readonly IMemento<EditorMemento> _editorMemento;

        protected BaseInsertShortcut(IShortcut successor, IMemento<EditorMemento> editorMemento) : base(successor)
        {
            _editorMemento = editorMemento;
        }

        public abstract string Insertion { get; }

        public override bool OnExecute(string key)
        {
            _editorMemento.Context.Content += Insertion;
            return true;
        }
    }
}
