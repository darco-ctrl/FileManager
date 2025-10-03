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

        private static ToggleButton? MainSelectedEntry;
        private static HashSet<ToggleButton> SelectedEntries = new HashSet<ToggleButton>();

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
    }
}
