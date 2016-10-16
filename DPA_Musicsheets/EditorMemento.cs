using System;
using System.ComponentModel;
using DPA_Musicsheets.Util;

namespace DPA_Musicsheets
{
    public interface IMemento<T> : INotifyPropertyChanged, IClonable<T>, IRestorable<T>
        where T : IMemento<T>
    {
        T Context { get; }
    }

    public class EditorMemento : IMemento<EditorMemento>
    {
        #region public string Content { get; set; } // RaisePropertyChanged
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; RaisePropertyChanged(nameof(Content)); }
        }
        #endregion

        #region public string Timestamp { get; set; } // RaisePropertyChanged
        private long _timestamp;
        public long Timestamp
        {
            get { return _timestamp; }
            private set { _timestamp = value; RaisePropertyChanged(nameof(Timestamp)); }
        }
        #endregion

        public EditorMemento Context => this;
        
        public EditorMemento(string content = "")
        {
            Content = content;
            Timestamp = DateTime.UtcNow.Ticks;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public EditorMemento Clone() => new EditorMemento(Content);

        public bool Restore(EditorMemento restorable)
        {
            Timestamp = restorable.Timestamp;
            Content = restorable.Content;
            return true;
        }
    }
}
