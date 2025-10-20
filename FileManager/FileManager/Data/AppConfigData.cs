using Avalonia;
using FileManager.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Data
{
    public  class AppConfigData
    {

        public enum SpecialFolderIndex : byte
        {
            USER_PROFILE_PATH,
            RECENT,
            DOCUMENTS,
            PICTURES,
            VIDEOS,
            MUSICS
        }

        private List<string> SpacialFolders = new();

        public AppConfigData()
        {
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)); // 0 User location
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.Recent)); // 1 recente
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)); // 2 Documents
            SpacialFolders.Add(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")); // 3 Downloads
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)); // 3 photos
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)); // 4 videos
            SpacialFolders.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)); // 5 musics

        }

        public string GetSpacialFolder(SpecialFolderIndex _index)
        {
            return SpacialFolders[(int)_index];
        }

        public string GetSpacialFolderWithInt(byte _index)
        {
            if (_index < SpacialFolders.Count)
            {
                return SpacialFolders[_index];
            }
            return "";
        }
    }
}