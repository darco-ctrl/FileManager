using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using FileManager.ViewModels;
using HarfBuzzSharp;
using System;
using System.Collections.Generic;
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
            this.Position = new Avalonia.PixelPoint(0, 0);

            this.KeyUp += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyUp(e);
            this.KeyDown += (Object? s, Avalonia.Input.KeyEventArgs e) => IM.OnKeyDown(e);
        }

<<<<<<< Updated upstream
=======
        #region Button Pressed

        private void CloseButtonPressed(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButtonPressed(Object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonPressed(Object sender, RoutedEventArgs e)
        {
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            } else
            {
                this.WindowState = WindowState.Normal;
            }

        }

        private void MoveButtonClicked(Object sender, RoutedEventArgs args)
        {
            EntryItemViewModel? selectedItem = MainEntryList.SelectedItem as EntryItemViewModel;
            if (selectedItem != null)
            {
                ControlsManager.ClipBoardItem = selectedItem.HoldingPath;
                ControlsManager.NoneMoveCopy = 1;
            }

        }

        private void CopyButtonClicked(Object sender, RoutedEventArgs args)
        {

            EntryItemViewModel? selectedItem = MainEntryList.SelectedItem as EntryItemViewModel;
            if (selectedItem != null)
            {
                ControlsManager.ClipBoardItem = selectedItem.HoldingPath;
                ControlsManager.NoneMoveCopy = 2;
            }

            /*
            if (ControlsManager.SelectedEntry == null) { return; }

            if (ControlsManager.SelectedEntry.DataContext is EntryItemViewModel entryData)
            {
                ControlsManager.ClipBoardItem = entryData.HoldingPath;
                ControlsManager.PasteFormMove = 2;



                Console.WriteLine(ControlsManager.ClipBoardItem);
            }*/
        }

        private void PasteButtonClicked(Object sender, RoutedEventArgs args)
        {
            if (ControlsManager.ClipBoardItem == null)
            {
                Console.WriteLine("Nothing to paste");
                return;
            }
            else
            {
                Console.WriteLine($"------------------------------\nCopying Item . . .\n From : {ControlsManager.ClipBoardItem}\n To : {AppState.GetWindowViewModel().CurrentWorkingDir}");

                if (ControlsManager.NoneMoveCopy == 2)
                {
                    FileOperation.CopyItem(ControlsManager.ClipBoardItem, AppState.GetWindowViewModel().CurrentWorkingDir);
                }
                else if (ControlsManager.NoneMoveCopy == 1)
                {
                    FileOperation.MoveEntry(ControlsManager.ClipBoardItem, AppState.GetWindowViewModel().CurrentWorkingDir);
                }


                ControlsManager.ClipBoardItem = null;
            }
        }

        private void RenameSelectedEntry(Object sender, RoutedEventArgs args)
        {
            EntryItemViewModel? selectedItem = MainEntryList.SelectedItem as EntryItemViewModel;
            if (selectedItem != null)
            {
                ControlsManager.RenameEntry = selectedItem.HoldingPath;
                MenuManager.OpenGetNameWindow(FileOperation.OperationState.RENAME);
            }
        }

        private void DeleteSelectedEntry(Object sender, RoutedEventArgs args)
        {
            EntryItemViewModel? selectedItem = MainEntryList.SelectedItem as EntryItemViewModel;
            if (selectedItem != null)
            {
                Console.WriteLine($"Passed tests\n Holding path: {selectedItem.HoldingPath}");
                FileOperation.DeleteEntry(selectedItem.HoldingPath);
            }
        }

        private void HomeButtonClicked(Object sender, RoutedEventArgs e)
        {
            AppState.GetWindowViewModel().SetCurrentDir(FileOperation.GetUserLocation());
        }

        private void PreviousDirButtonClicked(Object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Trying to go forward");
            string? dir = ControlsManager.GetPreviousDir();
            if (dir != null)
            {
                AppState.GetWindowViewModel().SetCurrentDir(dir, false);

                //Console.WriteLine("Moved to previous dir");
            }

            //Console.WriteLine("No previous dir");
        }

        private void ForwardDirButtonClicked(Object sender, RoutedEventArgs e)
        {
            //Console.WriteLine("Trying to go forward");
            string? dir = ControlsManager.GetNextDir();
            if (dir != null)
            {
                AppState.GetWindowViewModel().SetCurrentDir(dir, false);

                //Console.WriteLine("Moved to next dir");
            }

            //Console.WriteLine("No next dir");
        }

        private void RefreshButtonClicked(Object sender, RoutedEventArgs e)
        {
            FileSystemManager.RefreshDir();
        }

        #endregion

        #region Navigation Events

>>>>>>> Stashed changes
        /*
         * What this do is when an Entry like file or folder is clicked it try to set it as current Dir
         * by try i mean it checks if given entry is file or folder if folder then set it if not dont set
         */
        private void OnEntryDoubleTapped(Object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button but && but.DataContext is EntryItemViewModel entry)
            {
                AppState.GetWindowViewModel().SetCurrentDir(entry.HoldingPath);
            }
        }

        private void EntryClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ToggleButton? toggleButton = sender as ToggleButton;
            DynamicControlManager.SelectionManager(toggleButton);

        }


        /*
         * checking for mouse right click
         */
        private void OnEntryButtonPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (sender == null) { return; }
            var properties = e.GetCurrentPoint(sender as Button).Properties;

            if (properties.IsRightButtonPressed)
            {
                DynamicControlManager.MakeRightClickMenu(sender);
            }
        }

        /*
         * i made this function isntead of using window.Focus if i had to do somethng when i focus in future i 
         * can just add it here instead of going everywhere and changing it
         */
        public void FocusWindow()
        {
            DynamicControlManager.ResetButtonSelection();
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

        /*
         * after CurrentWorkingDir is updated this is called to update PathTextBox i made it into a function becuase
         * i can add stuff what to do before or after it is upated like now i added if its searching if so dont update
         * just return 
         * it helpfull cuz i dont have to add this if statement everywhere
         */
        public void UpdatePathBlockText()
        {
<<<<<<< Updated upstream
            if (AppState.IsSearching) { return; }
            PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
=======
            /*
            if (sender is ContextMenu rightClickMenu)
            {
                List<MenuItem> Items = rightClickMenu.Items.Cast<MenuItem>().ToList();
                Console.WriteLine($"Selected Entry = {ControlsManager.SelectedEntry}");
                if (ControlsManager.SelectedEntry == null)
                {
                    Items[1].IsEnabled = false;
                    Items[2].IsEnabled = false;
                    Items[4].IsEnabled = false;
                    Items[7].IsEnabled = false;
                }
                else
                {
                    Items[1].IsEnabled = true;
                    Items[2].IsEnabled = true;
                    Items[4].IsEnabled = true;
                    Items[7].IsEnabled = true;
                }

                if (ControlsManager.ClipBoardItem == null)
                {

                    Items[3].IsEnabled = false;
                }
                else
                {
                    Items[3].IsEnabled = true;
                }
            }
            */
>>>>>>> Stashed changes
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
                FileManager.updateDirItems();
            }

        }

        public InputManager GetInputManager() => IM;
    }
}