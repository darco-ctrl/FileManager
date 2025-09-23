using System.Collections.ObjectModel;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<FileItemViewModel> SidePanelItems { get; } = new ObservableCollection<FileItemViewModel>();

        public MainWindowViewModel()
        {
            SidePanelItems.Add(new FileItemViewModel { Name = "THIS PC", IconPath = "", Size = "40 GB" });
        }
    }
}
