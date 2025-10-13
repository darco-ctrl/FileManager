using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using FileManager.Core;
using FileManager.Managers;
using FileManager.Utils;
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
            if (DynamicControlManager.SelectedEntry == null) { return; }

            if (DynamicControlManager.SelectedEntry.DataContext is EntryItemViewModel entryData)
            {
                DynamicControlManager.ClipBoardItem = entryData.HoldingPath;
                DynamicControlManager.PasteFormMove = 1;
            }
        }

        private void CopyMenuItemClicked(Object sender, RoutedEventArgs args)
        {
            if (DynamicControlManager.SelectedEntry == null) { return; }

            if (DynamicControlManager.SelectedEntry.DataContext is EntryItemViewModel entryData)
            {
                DynamicControlManager.ClipBoardItem = entryData.HoldingPath;
                DynamicControlManager.PasteFormMove = 2;



                Console.WriteLine(DynamicControlManager.ClipBoardItem);
            }
        }

        private void PasteMenuItemClicked(Object sender, RoutedEventArgs args)
        {
            if (DynamicControlManager.ClipBoardItem == null)
            {
                Console.WriteLine("Nothing to paste");
                return;
            }
            else
            {
                Console.WriteLine($"------------------------------\nCopying Item . . .\n From : {DynamicControlManager.ClipBoardItem}\n To : {AppState.GetWindowViewModel().CurrentWorkingDir}");

                if (DynamicControlManager.PasteFormMove == 2)
                {
                    FileOperation.CopyItem(DynamicControlManager.ClipBoardItem, AppState.GetWindowViewModel().CurrentWorkingDir);
                }
                else if (DynamicControlManager.PasteFormMove == 1)
                {
                    FileOperation.MoveEntry(DynamicControlManager.ClipBoardItem, AppState.GetWindowViewModel().CurrentWorkingDir);
                }


                DynamicControlManager.ClipBoardItem = null;
            }
        }

        private void RenameSelectedEntry(Object sender, RoutedEventArgs args)
        {
            if (DynamicControlManager.SelectedEntry == null) { return; }

            if (DynamicControlManager.SelectedEntry.DataContext is EntryItemViewModel entryData)
            {
                DynamicControlManager.RenameEntry = entryData.HoldingPath;
                MenuManager.OpenGetNameWindow(FileOperation.OperationState.RENAME);
            }
        }

        private void DeleteSelectedEntry(Object sender, RoutedEventArgs args)
        {
            if (DynamicControlManager.SelectedEntry != null)
            {
                if (DynamicControlManager.SelectedEntry.DataContext is EntryItemViewModel entry)
                {
                    Console.WriteLine($"Passed tests\n Holding path: {entry.HoldingPath}");
                    FileOperation.DeleteEntry(entry.HoldingPath);
                }
            }
        }

        private void RefreshMenuItemClicked(Object? sender, RoutedEventArgs e)
        {
            FileSystemManager.RefreshDir();
        }

        #endregion

        #region Navigation Events 

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

        private void EntryClicked(Object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ToggleButton? entryButton = sender as ToggleButton;
            if (entryButton == null) { return; }
            DynamicControlManager.SelectionManager(entryButton);
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

        #endregion

        #region ContextMenu

        private void RightClickContextMenuOpened(Object sender, RoutedEventArgs args)
        {
            /*
            if (sender is ContextMenu rightClickMenu)
            {
                List<MenuItem> Items = rightClickMenu.Items.Cast<MenuItem>().ToList();
                Console.WriteLine($"Selected Entry = {DynamicControlManager.SelectedEntry}");
                if (DynamicControlManager.SelectedEntry == null)
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

                if (DynamicControlManager.ClipBoardItem == null)
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

        public InputManager GetInputManager() => IM;


        #endregion
    }
}