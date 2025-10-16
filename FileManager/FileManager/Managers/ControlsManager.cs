using Avalonia.Controls.Primitives;
using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Managers
{
    public static class ControlsManager
    {
        private static string? _stringValue;
        public static event Action? OnClipBoardItemChanged;

        private static List<string> RecentDirs = new List<string>();
        private static int RecentDirIndex = -1;

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

        public static string? GetNextDir()
        {
            Console.WriteLine($"RecentDirIndex: {RecentDirIndex}, RecentDirs.Count: {RecentDirs.Count}");
            if (RecentDirIndex < RecentDirs.Count - 1)
            {
                RecentDirIndex++;
                return RecentDirs[RecentDirIndex];
            }
            return null;
        }

        public static string? GetPreviousDir()
        {
            Console.WriteLine($"RecentDirIndex: {RecentDirIndex}, RecentDirs.Count: {RecentDirs.Count}");
            if (RecentDirIndex > 0)
            {
                RecentDirIndex--;
                return RecentDirs[RecentDirIndex];
            }
            return null;
        }

        public static void AddRecentDir(string dir)
        {
            if (RecentDirs.Count > 10)
            {
                 RecentDirs.RemoveAt(0);
            }
            if (RecentDirs.Count == 0)
            {
                RecentDirs.Add(dir);
                RecentDirIndex = 0;
                return;
            }
            if (RecentDirs[RecentDirs.Count - 1] != dir)
            {
                RecentDirs.Add(dir);
                RecentDirIndex = RecentDirs.Count - 1;
            }
        }
    }
}
