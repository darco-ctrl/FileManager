using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ViewModels
{
    public class SidePanelItemViewModel
    {
        public string Name { get; }
        public ObservableCollection<SidePanelItemViewModel> Children { get; }

        public SidePanelItemViewModel(string name)
        {
            Name = name;
            Children = new ObservableCollection<SidePanelItemViewModel>();
        }
    }
}
