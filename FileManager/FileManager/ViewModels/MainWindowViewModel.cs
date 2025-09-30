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
            AppState.SetMainWindowViewModel(this);
            FileManager.StartUpSetup();
            
        }

        public bool SetCurrentDir(string? newPath)
        {
            if (Directory.Exists(newPath))
            {
                CurrentWorkingDir = newPath;
                FileManager.updateDirItems();
                AppState.GetWindow().UpdatePathBlockText();
                return true;
            }
            return false;
        }
    }
}
