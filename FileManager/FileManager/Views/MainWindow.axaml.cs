using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using FileManager.ViewModels;
using HarfBuzzSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private void OnEntryDoubleTapped(Object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is ToggleButton but && but.DataContext is EntryItemViewModel entry)
            {
                AppState.GetWindowViewModel().SetCurrentDir(entry.HoldingPath);
            }
        }

        private void EntryScrollViewerPointerPressed(Object sender, PointerPressedEventArgs args)
        {

            if (args.Source is ToggleButton)
            {
                return;
            }

            DynamicControlManager.ResetButtonSelection();
        }

        private void EntryClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ToggleButton? entryButton = sender as ToggleButton;
            if (entryButton == null) { return; }
            DynamicControlManager.SelectionManager(entryButton);
        }

        private void EntryPointerPressed(Object sender, PointerPressedEventArgs args)
        {
            ToggleButton? entryButton = sender as ToggleButton;
            if (entryButton == null) { return; }

            if (args.GetCurrentPoint(this).Properties.IsRightButtonPressed)
            {
                Console.WriteLine("ToggleButtonPressed");
                entryButton.IsChecked = !entryButton.IsChecked;
                DynamicControlManager.SelectionManager(entryButton);
            }
        }

        private void DeleteSelectedEntry(Object sender, Avalonia.Interactivity.RoutedEventArgs args)
        {
            Console.WriteLine($"SelectedItem: {DynamicControlManager.SelectedEntry}");
            if (sender is ToggleButton toggleButton && toggleButton.DataContext is EntryItemViewModel entryData)
            {
                FileManager.DeleteEntry(entryData.HoldingPath);
            }
        }


        private void RightClickContextMenuOpened(Object sender, RoutedEventArgs args)
        {
            if (sender is ContextMenu rightClickMenu)
            {
                List<MenuItem> Items = rightClickMenu.Items.Cast<MenuItem>().ToList();
                if (DynamicControlManager.SelectedEntry == null)
                {
                    Items[3].IsEnabled = false;
                } else
                {
                    Items[3].IsEnabled = true;
                }
            }
        }

        /*
         * checking for mouse right click
         */

        /*
         * i made this function isntead of using window.Focus if i had to do somethng when i focus in future i 
         * can just add it here instead of going everywhere and changing it
         */
        public void FocusWindow()
        {
            Focus();
        }

        /*
         * this is to regain focus back to window when pressed somewhere else like lets say if your changing
         * text in PathTextBox and clicked somewhere else inside the window if so window will automatically
         * foucs into itself and this is the function that does that
         * 
         * PointerPressed is something from avalonia you can read its doc here > 'https://docs.avaloniaui.net/docs/concepts/input/pointer'
         * it explains it well
         */
        private void PointerPresed(Object sender, PointerPressedEventArgs e)
        {
            Focus();
        }

        private void RefreshMenuItemClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            FileManager.RefreshDir();
        }

        private void NewFileMenuItemClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuManager.CreateFileCreationWindow(false);
        }

        private void NewFolderMenuItemClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuManager.CreateFileCreationWindow(true);
        }

        /*
         * after CurrentWorkingDir is updated this is called to update PathTextBox i made it into a function becuase
         * i can add stuff what to do before or after it is upated like now i added if its searching if so dont update
         * just return 
         * it helpfull cuz i dont have to add this if statement everywhere
         */
        public void UpdatePathBlockText()
        {
            if (AppState.IsSearching) { return; }
            PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
        }



        /*
         * the sets up env for searching after SearchButton is clicked
         */
        public void SearchButtonClicked(Object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (AppState.CurrentState == AppState.States.NONE)
            {
                SearchSystem.RequestForSearching();
                PathTextBox.Text = "";
            } else if (AppState.CurrentState == AppState.States.SEARCHING)
            {
                UpdatePathBlockText();
                FileManager.RefreshDir();
            }

        }

        public InputManager GetInputManager() => IM;
    }
}