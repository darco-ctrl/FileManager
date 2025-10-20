using Avalonia.Media;
using FileManager.Managers;
using FileManager.Utils;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<EntryItemViewModel> CurrentLoadedEntires { get; set; } = new();

        public string CurrentWorkingDir { get; set; }

        public MainWindowViewModel()
        {
            CurrentWorkingDir = "";
        }

        /*
         * this try to set current path if it did set bath it returns true if not it returns a false
         */
        public bool SetCurrentDir(string? newPath, bool AddToRecent = true)
        {
            if (Directory.Exists(newPath))
            {
                CurrentWorkingDir = newPath;
                FileSystemManager.RefreshDir();
                AppState.GetWindow().UpdatePathBlockText();

                if (AddToRecent)  ControlsManager.AddToRecentDir(newPath);

                return true;
            }
            return false;
        }
    }
}
