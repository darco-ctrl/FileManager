
using Avalonia.Threading;
using FileManager.Managers;
using FileManager.Utils;
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
using APath = Avalonia.Controls.Shapes.Path;
using AShapes = Avalonia.Controls.Shapes;
using MSearchOption = Microsoft.VisualBasic.FileIO.SearchOption;

namespace FileManager.Core
{
    [SupportedOSPlatform("windows")]
    public static class FileOperation // File Operation
    {

        public enum OperationState 
        {
            NONE,
            RENAME,
            CREATE_FILE,
            CREATE_FOLDER
        }

        public static async Task DeleteItem(string path)
        {
            await Task.Run(() =>
            {
                if (Directory.Exists(path))
                {
                    FileSystem.DeleteDirectory(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                }
                else if (File.Exists(path))
                {
                    FileSystem.DeleteFile(path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
                }
                else
                {
                    Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
                }
            });

            FileSystemManager.RefreshDir();
        }

        public static void CopyItem(string src, string destination)
        {
            Console.WriteLine("Reviced Request to copy");
            if (Directory.Exists(src))
            {
                Console.WriteLine("Path is detected as a Directory Excuting method");

                _ = CopyDirector(src, destination);

            }
            else if (File.Exists(src)) 
            {
                Console.WriteLine("Path is detected as a File Excuting method");
                _ = Copy(src, destination);
            } else
            {
                Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
            }

            //FileManager.RefreshDir();
        }

        private static async Task CopyDirector(string sourcePath, string dest)
        {
            Console.WriteLine($"Source Path : {sourcePath}");

            await Task.Run(() =>
            {
                Console.WriteLine("Creating Folder");
                string _folderName = GetFolderName(sourcePath);
                string targetPath = Path.Combine(dest, _folderName);

                Console.WriteLine($"Target Path : {targetPath}");

                FileOperation.CreateDir(targetPath);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", System.IO.SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            });


            FileSystemManager.RefreshDir();
        }

        public static void MoveEntry(string src, string dest)
        {
            if (Directory.Exists(src)) 
            {
                string _dest = Path.Combine(dest, GetFolderName(src));

                Directory.Move(src, _dest);
            } else if (File.Exists(src)) {
                File.Move(src, dest);
            }
        }

        public static string GetFolderName(string path)
        {
            DirectoryInfo FolderInfo = new DirectoryInfo(path);
            return FolderInfo.Name;
        }

        private static async Task Copy(string src, string destinationFolder)
        {

            await Task.Run(() =>
            {
                string dest = Path.Combine(destinationFolder, Path.GetFileName(src));

                File.Copy(src, dest);
            });
        }

        public static void CreatingDecider(string entry_name, OperationState state)
        {
            if (state == OperationState.CREATE_FILE)
            {
                CreateFile(entry_name);
            } else if (state == OperationState.CREATE_FOLDER)
            {
                CreateDir(Path.Combine(AppState.GetWindowViewModel().CurrentWorkingDir, entry_name));
            }
        }

        #region Create File/Folder

        public static void CreateFile(string fileName)
        {
            string FilePath = Path.Combine(AppState.GetWindowViewModel().CurrentWorkingDir, fileName);

            File.Create(FilePath);
            FileSystemManager.RefreshDir();
        }

        public static void DeleteEntry(string entryPath)
        {
            _ = FileOperation.DeleteItem(entryPath);
        }

        public static void CreateDir(string dest)
        {

            Console.WriteLine($"Creating {dest}");

            Directory.CreateDirectory(dest);
            FileSystemManager.RefreshDir();

            Console.WriteLine("CreatedFile");
        }

        #endregion
    }
}
