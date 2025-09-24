using System;
using System.Collections.ObjectModel;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<DriveItemViewModel> SidePanelItems { get; } = FileManager.FetchThisPC();
       

        public MainWindowViewModel()
        {
            FileManager.StartExternalDrivesWatcher(this);
        }
    }
}
