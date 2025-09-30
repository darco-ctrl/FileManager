using Avalonia.Controls;
using Avalonia.Input;
using FileManager.ViewModels;
using HarfBuzzSharp;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;


namespace FileManager.Views
{
    public partial class MainWindow : Window
    {
        private InputManager IM { get; set; } = new();

        public MainWindow()
        {

            InitializeComponent();

            this.KeyUp += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyUp(e);
            this.KeyDown += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyDown(e);
        }

        /*
         * What this do is when an Entry like file or folder is clicked it try to set it as current Dir
         * by try i mean it checks if given entry is file or folder if folder then set it if not dont set
         */
        private void EntryClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button but && but.DataContext is EntryItemViewModel entry)
            {
                AppState.GetWindowViewModel().SetCurrentDir(entry.HoldingPath);
            }
        }

        private void PointerPresed(Object sender, PointerCaptureLostEventArgs e)
        {
            Focus();
        }

        public void UpdatePathBlockText()
        {
            PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
        }

        

        public InputManager GetInputManager() => IM;
    }
}