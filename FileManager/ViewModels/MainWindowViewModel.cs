using CommunityToolkit.Mvvm.Collections;
using FileManagerAvalonia.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace FileManager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        /*
         * this is basicaly a list which holds 'FileItemViewModel
         * 'List<>' is not used cuz 'ObservableCollection<>' when item is added or removed 
         * avalonia automatically updates which is usefull :D
         * 
         * we dont want only to draw when starting of app :/
         */

        public ObservableCollection<FileItemViewModel> Files { get; set; }

        // KWIT
        public MainWindowViewModel()
        {
            Files = new ObservableCollection<FileItemViewModel>
            {
                new FileItemViewModel { Name = @"C:\", IconPath = null, Size = "-"},
                new FileItemViewModel { Name = @"D:\", IconPath = null, Size = "1.2 MB"},
                new FileItemViewModel { Name = @"E:\", IconPath = null, Size = "10 MB"}
            };


        }
    }
}
