using System;
using System.Collections.Generic;

namespace FileManager.Data
{
    public static class DataManager
    {
        public static AppConfigData Current = new AppConfigData();

        public enum FileTypes : byte
        {
            ANY,
            TXT,
            FOLDER,
            CS,
            MP4
        }

        public static Dictionary<string, FileTypes> FileTypesTable = new Dictionary<string, FileTypes>();

        public static HashSet<string> SpecialPathCode = new(); 
    
        public static void Init()
        {
            FileTypesTable.Add(".txt", FileTypes.TXT);
            FileTypesTable.Add(".cs", FileTypes.CS);
            FileTypesTable.Add(".mp4", FileTypes.MP4);

            SpecialPathCode.Add("%recent");
            SpecialPathCode.Add("%recycleBin");
        }

    }
}



