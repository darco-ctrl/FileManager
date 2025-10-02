using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using FileManager.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class DynamicControlManager
    {
        private static List<Control> Menus = new List<Control>();

        private static ToggleButton? MainSelectedEntry;
        private static HashSet<ToggleButton> SelectedEntries = new HashSet<ToggleButton>();

        private static SolidColorBrush MainSelectedEntryColor = new SolidColorBrush(Color.Parse("#FF7B7B7B"));
        private static SolidColorBrush SelectedEntryColor = new SolidColorBrush(Color.Parse("#FF4C4C4C"));


        public static EntryItemViewModel CreateEntryItem(string entry)
        {
            EntryItemViewModel? entryItem = new EntryItemViewModel
            {
                Name = Path.GetFileName(entry),
                HoldingPath = entry
            };

            return entryItem;
        }

        public static void SelectionManager(ToggleButton? button)
        {

            if (button == null) { return; }

            if (button.IsChecked == true) // i used litral IsChecked == false bceause its not bool its bool? it can be null
            {
                if (MainSelectedEntry != null)
                {
                    MainSelectedEntry.IsChecked = false;
                    MainSelectedEntry = button;
                } 
                else {
                    MainSelectedEntry = button;
                }
            } else if (button.IsChecked == false) // i used litral IsChecked == false bceause its not bool its bool? it can be null
            {
                if (MainSelectedEntry == button)
                {
                    MainSelectedEntry.IsChecked = true;
                }
            }
        }

        public static void ButtonSelected(ToggleButton? button)
        {
            if (button == null) { return; }
            MainSelectedEntry = button;
            SelectedEntries.Add(button);
        }

        public static void ResetButtonSelection()
        {
            MainSelectedEntry = null;
            SelectedEntries.Clear();
        }

        public static void CanRemoveButtonSelection(ToggleButton? button)
        {
           
        }

        public static void MakeRightClickMenu(Object sender)
        {
            Button? EntryButton = sender as Button;
            if (EntryButton == null) { return; }

            ContextMenu rightClickContextMenu = new ContextMenu();

            MenuItem test1 = new MenuItem { Header = "Test 1" };
            test1.Click += (s, e) => Console.WriteLine("Test1 BeenClicked");

            MenuItem test2 = new MenuItem { Header = "Test 2" };
            test2.Click += (s, e) => Console.WriteLine("Test2 Been clicked");

            rightClickContextMenu.Items.Add(test1);
            rightClickContextMenu.Items.Add(test2);

            EntryButton.ContextMenu = rightClickContextMenu;

            Menus.Add(EntryButton);
        }
    }
}
