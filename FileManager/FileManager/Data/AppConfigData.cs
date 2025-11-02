using Avalonia;
using FileManager.Core;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace FileManager.Data
{
    public  class AppConfigData
    {
        private Dictionary<byte, string> SpacialFolders = new();

        public AppConfigData()
        {
            AppSetup();
            SpecialFoldersSetup();
        }

        private void AppSetup()
        {
        }

        private void SpecialFoldersSetup()
        {
            SpacialFolders.Add(0, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)); // 0 User location
            SpacialFolders.Add(1, "%recent"); // 1 recente
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

        public void AddToRecent(string _path)
        {
            if (!Directory.Exists(_path)) return;

            sbyte _pathIndex = (sbyte)DataBase.Recents.IndexOf(_path);
            if (_pathIndex != -1 && _pathIndex < DataBase.Recents.Count - 1)
            {
                DataBase.Recents.RemoveAt(_pathIndex);
                DataBase.Recents.Add(_path);

                return;
            }


            if (DataBase.Recents.Count < DataBase.Data["max_recent"])
            {
                DataBase.Recents.Add(_path);
            } else
            {
                DataBase.Recents.RemoveAt(0);
                DataBase.Recents.Add(_path);
            }
        }
    }
}
