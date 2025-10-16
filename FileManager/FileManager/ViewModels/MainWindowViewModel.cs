using Avalonia.Media;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<DriveItemViewModel> SidePanelItems { get; } = FileManager.FetchThisPC();
        public ObservableCollection<EntryItemViewModel> CurrentLoadedEntires { get; set; } = new();

        public string CurrentWorkingDir { get; set; }

        public MainWindowViewModel()
        {
            CurrentWorkingDir = "";
            FileManager.StartExternalDrivesWatcher(this);
        }

        /*
         * this try to set current path if it did set bath it returns true if not it returns a false
         */
        public bool SetCurrentDir(string? newPath, bool addToRecent = true)
        {
            if (Directory.Exists(newPath))
            {
                CurrentWorkingDir = newPath;
                FileManager.updateDirItems();
                AppState.GetWindow().UpdatePathBlockText();
                if (addToRecent) ControlsManager.AddRecentDir(newPath);
                return true;
            }
            return false;
        }
    }
}
