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
    public static class IconsManager
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

        private static StringBuilder EntriesIcon = new StringBuilder("avares://FileManager/Assets/EntriesIcons/");

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
            EntryIcons.Add(EntryType.FILE, new StringBuilder("file-text.png"));
            EntryIcons.Add(EntryType.FOLDER, new StringBuilder("folder.png"));
            EntryIcons.Add(EntryType.IMAGE, new StringBuilder("image.png"));
            EntryIcons.Add(EntryType.AUDIO , new StringBuilder("audio.png"));
            EntryIcons.Add(EntryType.VIDEO , new StringBuilder("video.png"));
        }

        public static string GetIconPath(string entry_path)
        {
            EntryType _entryType = EntryType.FILE;
            StringBuilder resultString = new StringBuilder(EntriesIcon.ToString());

            if (Directory.Exists(entry_path))
            {
                _entryType = EntryType.FOLDER;
            }
            else
            {
                _entryType = GetFileType(entry_path);
            }

            resultString.Append(EntryIcons[_entryType]);

            //Console.WriteLine($"Result IconPath: {resultString.ToString()}");

            return resultString.ToString();
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