using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class FileManagerHelper
    {

        /*
         * Black list stuff i can add something that shouldnt appear in the Display
         * or just ignore it
         * i didnt use HashSet for this cuz i have to loop through this anyways so
         * 
         */
        private readonly static List<FileAttributes> BlackListAttr = new List<FileAttributes>
        {
            FileAttributes.System,
            FileAttributes.Hidden,
        };


        /*
         * this function checks if the Entry (could be file or folder) is in black list 
         * right now the defualt black list has System files attribute
         * and hidden attrubute (this hidden attrubute is system one not . one you usee in 
         * .config or .gitignore this is different)
         */
        public static bool IsEntryInBlackList(string entry)
        {
            var attr = File.GetAttributes(entry);

            foreach (var blacklistatr in BlackListAttr)
            {
                if (attr.HasFlag(blacklistatr))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool DoesEntryExists(string entry)
        {
            return Path.Exists(AppState.GetWindowViewModel().CurrentWorkingDir + entry);
        }
    }
}
