using Avalonia.Controls;
using FileManager.ViewModels;
using HarfBuzzSharp;
using System;
using System.IO;

namespace FileManager.Views
{
    public partial class MainWindow : Window
    {
        private InputManager IM { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            AppState.SetWindow(this);

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
                AppState.GetWindowViewModel().SetCurrentDir(entry.HoldingPath);
            }
        }

        /*
         * if you have read Console Versio nyou would know how this works
         */
        public void GoBack(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FileManager.GoBackOne();
        }

        public void PathTextBoxChanged(Object sender, TextChangedEventArgs e)
        {
            TextBox pathTextBox = (TextBox)sender;
            string? path = PathTextBox.Text;

            if (Directory.Exists(path))
            {
                PathTextBox.Text = path;
            } else
            {
                PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
            }

        }

        public void UpdatePathBlockText()
        {
            PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
        }

        

        public InputManager GetInputManager() => IM;
    }
}