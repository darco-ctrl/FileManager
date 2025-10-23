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
            MUSICS,
            DOWNLOADS
        }



        private Dictionary<SpecialFolderIndex, string> SpacialFolders = new();

        public AppConfigData()
        {
            SpacialFolders.Add(SpecialFolderIndex.USER_PROFILE_PATH ,Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)); // 0 User location
            SpacialFolders.Add(SpecialFolderIndex.RECENT, Environment.GetFolderPath(Environment.SpecialFolder.Recent)); // 1 recente
            SpacialFolders.Add(SpecialFolderIndex.DOCUMENTS, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)); // 2 Documents
            SpacialFolders.Add(SpecialFolderIndex.DOWNLOADS, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")); // 3 Downloads
            SpacialFolders.Add(SpecialFolderIndex.PICTURES, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)); // 3 photos
            SpacialFolders.Add(SpecialFolderIndex.VIDEOS, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)); // 4 videos
            SpacialFolders.Add(SpecialFolderIndex.MUSICS, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)); // 5 musics

        }

        public string GetSpacialFolder(SpecialFolderIndex _index)
        {
            return SpacialFolders[_index];
        }
    }
}
