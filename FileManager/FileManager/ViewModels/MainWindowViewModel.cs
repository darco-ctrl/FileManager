using System.Collections.ObjectModel;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<SidePanelItemViewModel> SidePanelItems { get; } = new ObservableCollection<SidePanelItemViewModel>();

        public MainWindowViewModel()
        {
            SidePanelItemViewModel ThisPc = new SidePanelItemViewModel("This PC");

            ThisPc.Children.Add(new SidePanelItemViewModel("C:\\"));
            ThisPc.Children.Add(new SidePanelItemViewModel("D:\\"));
            ThisPc.Children.Add(new SidePanelItemViewModel("J:\\"));

            SidePanelItems.Add(ThisPc);
        }
    }
}
