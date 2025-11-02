using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;

namespace FileManager.Data
{
    public static class DataBase
    {
        
        public static List<string> Recents = new();
        public static Dictionary<string, string> AppsPath = new();

        public static Dictionary<string, dynamic> Data = new(); 
    
        public static void Init() {
            Data.Add("max_recent", 10);

            AppsPath.Add("vs", @"C:\Users\nihal\AppData\Local\Programs\Microsoft VS Code\Code.exe");
        }
    }
}
