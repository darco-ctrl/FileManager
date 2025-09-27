using System;
using System.Collections.ObjectModel;
using System.IO;

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
            GlobalVariables.SetMainWindowViewModel(this);
            FileManager.StartUpSetup();
            
        }

        public void SetCurrentDir(string newPath)
        {
            if (Directory.Exists(newPath))
            {
                CurrentWorkingDir = newPath;
                FileManager.updateDirItems();
            }
        }
    }
}
