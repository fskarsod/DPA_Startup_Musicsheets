using System.IO;

namespace DPA_Musicsheets
{
    public interface IContentStorage
    {
        int SaveHashCode { get; }

        bool Saved { get; }

        bool Save();

        void Load();
        
        void LoadFromLocation(string location);
    }
    
    public class ContentStorage : IContentStorage
    {
        private readonly IMemento<EditorMemento> _editorMemento;

        private readonly IDialogService _dialogService;

        public int SaveHashCode { get; private set; }

        public bool Saved { get; private set; }

        public ContentStorage(EditorMemento editorMemento, IDialogService dialogService)
        {
            _editorMemento = editorMemento;
            _dialogService = dialogService;
        }

        public bool Save()
        {
            var fileName = _dialogService.DisplaySave();
            if (fileName.Length <= 0) { return false; }
            try
            {
                File.WriteAllText(fileName, _editorMemento.Context.Content);
            }
            catch (IOException)
            {
                _dialogService.DisplayError("Something has gone wrong when saving the file.");
                return false;
            }
            SaveHashCode = _editorMemento.Context.Content.GetHashCode();
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
                }
                catch (IOException)
                { /* Explicit swallow */ }
            }
            _dialogService.DisplayError("Something has gone wrong when loading the file.");
            // Leave the content as is.
        }

        public void LoadFromLocation(string location)
        {
            _editorMemento.Context.Content = File.ReadAllText(location);
        }
    }
}
