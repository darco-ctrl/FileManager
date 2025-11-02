﻿using FileManager.Data;
using FileManager.Managers;
using FileManager.Utils;
using FileManager.ViewModels;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Versioning;
using System.Threading.Tasks;

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
                    //Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
                }
            });

            FileSystemManager.RefreshDir();
        }

        public static void CopyItem(string src, string destination)
        {
            //Console.WriteLine("Reviced Request to copy");
            if (Directory.Exists(src))
            {
                //Console.WriteLine("Path is detected as a Directory Excuting method");

                _ = CopyDirector(src, destination);

            }
            else if (File.Exists(src))
            {
                //Console.WriteLine("Path is detected as a File Excuting method");
                _ = Copy(src, destination);
            } else
            {
                //Console.WriteLine("It says this file/folder doesnt exists\nidk how this is possible it shouldnt happen");
            }

            FileSystemManager.RefreshDir();
        }

        private static async Task CopyDirector(string sourcePath, string dest)
        {
            //Console.WriteLine($"Source Path : {sourcePath}");

            await Task.Run(() =>
            {
                //Console.WriteLine("Creating Folder");
                string _folderName = GetItemName(sourcePath);
                string targetPath = Path.Combine(dest, _folderName);

                //Console.WriteLine($"Target Path : {targetPath}");

                FileOperation.CreateDir(targetPath);

                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", System.IO.SearchOption.AllDirectories))
                {
                    try
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                    } catch (Exception ex)
                    {
                        //Console.WriteLine($"Copying dir existed with error: {ex}");
                        return;
                    }
               }

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                    } catch (Exception ex)
                    {
                        //Console.WriteLine($"Copying file existed at replacing files with error: {ex}");   
                        return; 
                    }
               }
            });


            FileSystemManager.RefreshDir();
        }

        public static void MoveEntry(string src, string dest)
        {
            string _dest = Path.Combine(dest, GetItemName(src));

            Console.WriteLine($"Moving entry \nFrom: {src}\nTo: {dest}");
            
            if (Directory.Exists(src))
            {
                try
                {
                    Directory.Move(src, _dest);   
                } catch (Exception ex)
                {
                    Console.WriteLine($"Moving dir existed with error: {ex}.");
                    return;
                }
            } else if (File.Exists(src)) {
                try
                {
                    File.Move(src, _dest);
                } catch (Exception ex)
                {
                    Console.WriteLine($"moving file existed with error: {ex}");
                    return;    
                }
            }

            FileSystemManager.RefreshDir();
        }

        public static string GetItemName(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo FolderInfo = new DirectoryInfo(path);
                return FolderInfo.Name;
            }
            else if (File.Exists(path))
            {
                return Path.GetFileName(path);
            }

            throw new FileNotFoundException("Source Item not found: " + path);
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
            } else if (state == OperationState.RENAME && ControlsManager.RenameEntry != null)
            {
                MoveEntry(ControlsManager.RenameEntry, Path.Combine(AppState.GetWindowViewModel().CurrentWorkingDir, entry_name));
                ControlsManager.RenameEntry = null;
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

            //Console.WriteLine($"Creating {dest}");

            Directory.CreateDirectory(dest);
            FileSystemManager.RefreshDir();

            //Console.WriteLine("CreatedFile");
        }
        
        #endregion
    }
}
