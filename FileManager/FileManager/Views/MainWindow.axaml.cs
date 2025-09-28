using Avalonia.Controls;
using FileManager.ViewModels;
using System;

namespace FileManager.Views
{
    public partial class MainWindow : Window
    {
        private InputManager IM { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            GlobalVariables.SetWindow(this);

            this.KeyUp += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyUp(e);
            this.KeyDown += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyDown(e);
        }

        /*
         * What this do is when an Entry like file or folder is clicked it try to set it as current Dir
         * by try i mean it checks if given entry is file or folder if folder then set it if not dont set
         */
        public void EntryClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button but && but.DataContext is EntryItemViewModel entry)
            {
                GlobalVariables.GetWindowViewModel().SetCurrentDir(entry.HoldingPath);
            }
        }

        /*
         * if you have read Console Versio nyou would know how this works
         */
        public void GoBack(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FileManager.GoBackOne();
        }

        public InputManager GetInputManager() => IM;
    }
}