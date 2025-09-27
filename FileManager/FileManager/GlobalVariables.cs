using FileManager.ViewModels;
using FileManager.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class GlobalVariables
    {
        public static MainWindow? window { get; private set; }
        public static MainWindowViewModel? windowViewmodel { get; private set; }

        public static void SetWindow(MainWindow mw)
        {
            window = mw;
        }

        public static MainWindow GetWindow()
        {
            if (window != null)
            {
                return window;
            } else
            {
                throw new InvalidOperationException("'MainWindow' has not been set yet!!");
            }
        }

        public static void SetMainWindowViewModel(MainWindowViewModel mwvm)
        {
            windowViewmodel = mwvm;
        }

        public static MainWindowViewModel GetWindowViewModel()
        {
            if (windowViewmodel != null)
            {
                return windowViewmodel;
            } else
            {
                throw new InvalidOperationException("'MainWindowViewModel' has not been set yet!!");
            }
        }
    }
}
