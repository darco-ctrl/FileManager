using Avalonia;
using FileManager.Core;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager.Data
{
    public  class AppConfigData
    {

        public Dictionary<string, string> AppsPath = new();
        private Dictionary<byte, string> SpacialFolders = new();

        public AppConfigData()
        {
            AppSetup();
            SpecialFoldersSetup();
        }

        private void AppSetup()
        {
            AppsPath.Add("vs", @"C:\Users\nihal\AppData\Local\Programs\Microsoft VS Code\Code.exe");
        }

        private void SpecialFoldersSetup()
        {
             SpacialFolders.Add(0, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)); // 0 User location
            SpacialFolders.Add(1, Environment.GetFolderPath(Environment.SpecialFolder.Recent)); // 1 recente
            SpacialFolders.Add(2, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)); // 2 Documents
            SpacialFolders.Add(3, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads")); // 3 Downloads
            SpacialFolders.Add(4, Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)); // 3 photos
            SpacialFolders.Add(5, Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)); // 4 videos
            SpacialFolders.Add(6, Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)); // 5 musics

           
        }

        public string GetSpacialFolder(byte _index)
        {
            return SpacialFolders[_index];
        }
    }
}
