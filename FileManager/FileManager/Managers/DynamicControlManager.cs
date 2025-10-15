using Avalonia.Controls.Primitives;
using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Managers
{
    public static class DynamicControlManager
    {

        private static string? _stringValue;
        public static event Action? OnClipBoardItemChanged;

        public static string? ClipBoardItem {
            get => _stringValue;
            set
            {
                if (_stringValue != value) 
                {
                    _stringValue = value;
                    OnClipBoardItemChanged?.Invoke();
                }
            }
        }

        public static byte? NoneMoveCopy = 0;
        // 0 = none
        // 1 = Move
        // 2 = Copy 

        public static string? RenameEntry;

        public static EntryItemViewModel CreateEntryItem(string entry)
        {
            EntryItemViewModel? entryItem = new EntryItemViewModel
            {
                Name = Path.GetFileName(entry),
                HoldingPath = entry
            };

            return entryItem;
        }
    }
}
