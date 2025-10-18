using Avalonia.Media;
using FileManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager.ThemeManager
{
    public static class SvgManager
    {

        private static readonly HashSet<string> AudioExtensions = new()
        {
            ".mp3", ".wav", ".flac", ".acc", ".ogg"
        };

        private static readonly HashSet<string> VideoExtensions = new()
        {
            ".mp4", ".mkv", ".avi", ".mov", ".wmv"
        };

        private static readonly HashSet<string> ImageExtension = new()
        {
            ".png", "jpg", ".jpeg", ".bmp", ".gif", ".svg"
        };

        private static StringBuilder EntriesIcon = new StringBuilder("Assets/EntriesIcons/");

        private enum EntryType : byte
        {
            FILE,
            FOLDER,
            IMAGE,
            VIDEO,
            AUDIO
        }

        private static Dictionary<EntryType, StringBuilder> EntryIcons = new Dictionary<EntryType, StringBuilder>();

        public static void Init()
        {
            EntryIcons.Add(EntryType.FILE, new StringBuilder("file-text.svg"));
            EntryIcons.Add(EntryType.FOLDER, new StringBuilder("folder.svg"));
            EntryIcons.Add(EntryType.IMAGE, new StringBuilder("image.svg"));
            EntryIcons.Add(EntryType.AUDIO , new StringBuilder("audio.svg"));
            EntryIcons.Add(EntryType.VIDEO , new StringBuilder("video.svg"));
        }

        public static string GetSvgPath(string entry_path)
        {
            EntryType _entryType;
            StringBuilder result = EntriesIcon;

            if (Directory.Exists(entry_path))
            {
                _entryType = EntryType.FOLDER;
            }
            else
            {
                _entryType = GetFileType(entry_path);
            }

            result.Append(EntryIcons[_entryType]);
            return result.ToString();
        }

        private static EntryType GetFileType(string path)
        {
            string ext = Path.GetExtension(path);

            if (ImageExtension.Contains(ext)) return EntryType.IMAGE;
            if (VideoExtensions.Contains(ext)) return EntryType.VIDEO;
            if (AudioExtensions.Contains(ext)) return EntryType.AUDIO;

            return EntryType.FILE;
        }
    }
}