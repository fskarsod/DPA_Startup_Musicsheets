using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA_Musicsheets.Util;

namespace DPA_Musicsheets.Memento
{
    public class EditorMemento : IMemento<EditorMemento>
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set {  _content  = value; RaisePropertyChanged(nameof(Content)); }
        }

        private long _timestamp;
        public long Timestamp
        {
            get { return _timestamp; }
            private set { _timestamp = value; RaisePropertyChanged(nameof(Timestamp)); }
        }

        public EditorMemento(string content)
        {
            Timestamp = DateTime.UtcNow.Ticks;
            Content = content;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EditorMemento Clone()
        {
            return new EditorMemento(Content);
        }

        public bool Restore(EditorMemento restorable)
        {
            Timestamp = restorable.Timestamp;
            Content = restorable.Content;
            return true;
        }
    }
}
