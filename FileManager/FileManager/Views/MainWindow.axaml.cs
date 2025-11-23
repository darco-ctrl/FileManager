using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using FileManager.Core;
using FileManager.Managers;
using FileManager.Utils;
using FileManager.ViewModels;
using FileManager.Input;
using HarfBuzzSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Avalonia.Platform;
using Avalonia;
using FileManager.Data;
using Microsoft.VisualBasic;


namespace FileManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AppState.SetWindow(this);

            InitializeComponent();

            Position = new Avalonia.PixelPoint(0, 0);

            this.KeyUp += (Object? s, Avalonia.Input.KeyEventArgs e) => InputManager.OnKeyUp(e);
            this.KeyDown += (Object? s, Avalonia.Input.KeyEventArgs e) => InputManager.OnKeyDown(e);

            /*
            try
            {
                var uri = new Uri("avares://FileManager/Assets/EntriesIcons/folder.svg");
                using var stream = AssetLoader.Open(uri);
                var svg = new Svg();
                svg.Load(stream);
                Console.WriteLine("SVG loaded successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load SVG: {ex.Message}");
            }
            */
        }

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

        private void MoveMenuItemClicked(Object sender, RoutedEventArgs args)
        {

            string? path = FileSystemManager.GetSelectedItemPath();
            if (path != null)
            {
                ControlsManager.ClipBoardItem = path;
                ControlsManager.NoneMoveCopy = 1;
            }
        }

        private void CopyMenuItemClicked(Object sender, RoutedEventArgs args)
        {

            string? path = FileSystemManager.GetSelectedItemPath();
            if (path != null)
            {
                ControlsManager.ClipBoardItem = path;
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

        private void PasteMenuItemClicked(Object sender, RoutedEventArgs args)
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

                //FileSystemManager.RefreshDir();
                ControlsManager.ClipBoardItem = null;
            }
        }

        private void RenameSelectedEntry(Object sender, RoutedEventArgs args)
        {

            string? path = FileSystemManager.GetSelectedItemPath();            
            if (path != null)
            {
                ControlsManager.RenameEntry = path;
                MenuManager.OpenGetNameWindow(FileOperation.OperationState.RENAME);
            }
        }

        private void DeleteSelectedEntry(Object sender, RoutedEventArgs args)
        {

            string? path = FileSystemManager.GetSelectedItemPath();

            if (path != null)
            {
                Console.WriteLine($"Passed tests\n Holding path: {path}");
                FileOperation.DeleteEntry(path);
            }
        }

        private void HomeButtonClicked(Object sender, RoutedEventArgs e)
        {
            AppState.GetWindowViewModel().SetCurrentDir(DataManager.Current.GetSpacialFolder(0));
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

        private void GoBackDirButtonClicked(Object sender, RoutedEventArgs e)
        {
            FileSystemManager.GoBackOne();
        }

        private void RefreshButtonClicked(Object sender, RoutedEventArgs e)
        {
            FileSystemManager.RefreshDir();
        }

        private void SettingsButtonClicked(Object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Creating settings window");
            MenuManager.OpenSettingsWindow();
        }

        #endregion

        #region Navigation Events

        /*
         * What this do is when an Entry like file or folder is clicked it try to set it as current Dir
         * by try i mean it checks if given entry is file or folder if folder then set it if not dont set
         */
        private void OnEntryDoubleTapped(Object? sender, Avalonia.Input.TappedEventArgs e)
        {
            ControlsManager.SetQuickAccesToNull();

            string? path = FileSystemManager.GetSelectedItemPath();

            if (path != null)
            {
                AppState.GetWindowViewModel().SetCurrentDir(path);
            }
        }


        /*
         * after CurrentWorkingDir is updated this is called to update PathTextBox i made it into a function becuase
         * i can add stuff what to do before or after it is upated like now i added if its searching if so dont update
         * just return
         * it helpfull cuz i dont have to add this if statement everywhere
         */
        public void UpdatePathBlockText()
        {
            PathTextBox.Text = AppState.GetWindowViewModel().CurrentWorkingDir;
        }

        #endregion

        #region PointerPressed

        private void TitleBarPointerPressed(Object sender, PointerPressedEventArgs args)
        {
            if (args.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                BeginMoveDrag(args);
            }
        }

        private void EntryScrollViewerPointerPressed(Object sender, PointerPressedEventArgs args)
        {

            if (args.Source is ToggleButton)
            {
                return;
            }
        }

        /*
         * this is to regain focus back to window when pressed somewhere else like lets say if your changing
         * text in PathTextBox and clicked somewhere else inside the window if so window will automatically
         * foucs into itself and this is the function that does that
         *
         * PointerPressed is something from avalonia you can read its doc here > 'https://docs.avaloniaui.net/docs/concepts/input/pointer'
         * it explains it well
         */
        private void WindowPointerPressed(Object sender, PointerPressedEventArgs e)
        {
            Focus();
        }

        #endregion

        #region ContextMenu

        private void RightClickContextMenuOpened(Object sender, RoutedEventArgs args)
        {
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
        }


        private void NewFileMenuItemClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuManager.OpenGetNameWindow(FileOperation.OperationState.CREATE_FILE);
        }

        private void NewFolderMenuItemClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            MenuManager.OpenGetNameWindow(FileOperation.OperationState.CREATE_FOLDER);
        }


        #endregion

        #region Utility

        /*
         * i made this function isntead of using window.Focus if i had to do somethng when i focus in future i
         * can just add it here instead of going everywhere and changing it
         */
        public void FocusWindow()
        {
            Focus();
        }
        #endregion
    }
}
