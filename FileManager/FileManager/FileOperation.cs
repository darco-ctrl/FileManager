using HarfBuzzSharp;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    [SupportedOSPlatform("windows")]
    public static class FileOperation // File Operation
    {
        public static void DeleteItem(string path)
        {
           if (Directory.Exists(path))
           {
                FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
           } else if (File.Exists(path))
            {
                FileSystem.DeleteFile(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
            } else
            {
                Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
            }

            FileManager.RefreshDir();
        }

        public static void CopyItem(string src, string destination)
        {
            Console.WriteLine("Reviced Request to copy");
            if (Directory.Exists(src))
            {
                Console.WriteLine("Path is detected as a Directory Excuting method");

                //CopyDirectory(path, destination);

            }
            else if (File.Exists(src)) 
            {
                Console.WriteLine("Path is detected as a File Excuting method");
                Copy(src, destination);
            } else
            {
                Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
            }

            FileManager.RefreshDir();
        }

        private static void CopyDirector(string src, string dest)
        {
            
        }

        private static void Copy(string src, string destinationFolder)
        {
            string dest = Path.Combine(destinationFolder, Path.GetFileName(src));

            File.Copy(src, dest);
        }
    }
}
