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

        public static ToggleButton? SelectedEntry;
        private static HashSet<ToggleButton> SelectedEntries = new HashSet<ToggleButton>();

        public static string? PathToCopy;

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
                if (SelectedEntry != null)
                {
                    SelectedEntry.IsChecked = false;
                    SelectedEntry = button;
                } 
                else {
                    SelectedEntry = button;
                }
            } else if (button.IsChecked == false) // i used litral IsChecked == false bceause its not bool its bool? it can be null
            {
                if (SelectedEntry == button)
                {
                    SelectedEntry.IsChecked = true;
                }
            }
        }

        public static void ButtonSelected(ToggleButton? button)
        {
            if (button == null) { return; }
            SelectedEntry = button;
            SelectedEntries.Add(button);
        }

        public static void ResetButtonSelection()
        {
            if (SelectedEntry != null)
            {
                SelectedEntry.IsChecked = false;
            }
            SelectedEntry = null;
            SelectedEntries.Clear();
        }

        public static void CanRemoveButtonSelection(ToggleButton? button)
        {

        }
    }
}
