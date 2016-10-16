using System.ComponentModel;
using System.IO;

namespace DPA_Musicsheets
{
    public interface IContentStorage
    {
        bool Saved { get; }

        bool Save();

        void Load();
        
        void LoadFromLocation(string location);
    }
    
    // todo: state
    public class ContentStorage : IContentStorage
    {
        private readonly IApplicationContext _applicationContext;

        private readonly IDialogService _dialogService;

        public bool Saved
        {
            get { return _applicationContext.Saved; }
            set { _applicationContext.Saved = value; }
        }

        public ContentStorage(IApplicationContext applicationContext, IDialogService dialogService)
        {
            _applicationContext = applicationContext;
            _dialogService = dialogService;
            _applicationContext.EditorMemento.PropertyChanged += OnPropertyChanged;
        }

        public void OnPropertyChanged(object sender, PropertyChangedEventArgs evt)
        {
            if (evt.PropertyName.Equals(nameof(_applicationContext.EditorMemento.Content)))
                OnEditorChanged();
        }

        private void OnEditorChanged()
        {
            Saved = false;
        }

        public bool Save()
        {
            var fileName = _dialogService.DisplaySave();
            if (fileName.Length <= 0) { return false; }
            try
            {
                File.WriteAllText(fileName, _applicationContext.EditorMemento.Content);
            }
            catch (IOException)
            {
                _dialogService.DisplayError("Something has gone wrong when saving the file.");
                return false;
            }
            return Saved = true;
        }

        public void Load()
        {
            var fileName = _dialogService.DisplayOpen();
            if (fileName.Length > 0)
            {
                try
                {
                    LoadFromLocation(fileName);
                    return;
                }
                catch (IOException)
                { /* Explicit swallow */ }
            }
            _dialogService.DisplayError("Something has gone wrong when loading the file.");
            // Leave the content as is.
        }

        public void LoadFromLocation(string location)
        {
            // todo: .ly, .mid
            // if .ly -> File.ReadAllText
            // if .mid -> to Sequence -> to Domain -> to Lilypond

            // to editor only <--
            _applicationContext.EditorMemento.Content = File.ReadAllText(location);
        }
    }
}
