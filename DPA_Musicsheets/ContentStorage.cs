﻿using System.ComponentModel;
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
        private readonly IContentLoader _contentLoader;

        private readonly IDialogService _dialogService;

        public bool Saved
        {
            get { return _applicationContext.Saved; }
            set { _applicationContext.Saved = value; }
        }

        public ContentStorage(IApplicationContext applicationContext, IContentLoader contentLoader, IDialogService dialogService)
        {
            _applicationContext = applicationContext;
            _contentLoader = contentLoader;
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
            _applicationContext.FileLocation = _dialogService.DisplaySave();
            if (_applicationContext.FileLocation.Length <= 0) { return false; }
            try
            {
                File.WriteAllText(_applicationContext.FileLocation, _applicationContext.EditorMemento.Content);
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
            _applicationContext.FileLocation = _dialogService.DisplayOpen();
            if (_applicationContext.FileLocation.Length > 0)
            {
                try
                {
                    LoadFromLocation(_applicationContext.FileLocation);
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
            if (location.EndsWith(".mid"))
            {
                _contentLoader.FromMidi();
            }
            else // if (location.EndsWith(".ly"))
            {
                _contentLoader.FromLilypond();
            }
        }
    }
}
