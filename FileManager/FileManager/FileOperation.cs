using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using HarfBuzzSharp;

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

        public static void CopyItem(string path, string destination)
        {
            Console.WriteLine("Reviced Request to copy");
            if (Directory.Exists(path))
            {
                Console.WriteLine("Path is detected as a Directory Excuting method");
                try
                {
                    string _folderName = Path.GetFileName(path.TrimEnd(Path.DirectorySeparatorChar));
                    string _desitination = Path.Combine(_folderName, destination);

                    FileSystem.CopyDirectory(path, _desitination, UIOption.AllDialogs);
                } catch (Exception ex)
                {
                    Console.WriteLine($"Failed to excute error: {ex}");
                }

            }
            else if (File.Exists(path)) 
            {
                Console.WriteLine("Path is detected as a File Excuting method");
                FileSystem.CopyFile(path, destination, UIOption.AllDialogs);
            } else
            {
                Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
            }

            FileManager.RefreshDir();
        }
    }
}
