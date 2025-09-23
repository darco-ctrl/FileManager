using System.Collections.ObjectModel;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<FileItemViewModel> SidePanelItems { get; } = FileManager.FetchThisPC();

        public MainWindowViewModel()
        {
           
        }
    }
}
