using System;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;

namespace FIleManagerConsole
{
    internal static class FileSystem
    {
        public static string CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private static List<String> LoadedEntries = new();
        public static void GoToDir(int num)
        {
            if (num > 0 && num <= LoadedEntries.Count())
            {
                string new_dir = LoadedEntries[num - 1];

                if (Directory.Exists(new_dir)) {
                    CurrentDirectory = LoadedEntries[num - 1];

                } else
                {
                    Console.WriteLine("That is not a folder");

                }


            } else
            {
                Console.WriteLine("Out of bound.");

            }
        }

        public static void CreatFolder()
        {
            while (true) {
                Console.WriteLine("'%c' to cancel\n Enter folder name: ");
                string? new_folder_name = Console.ReadLine();

                if (new_folder_name == null) { continue; }

                if (new_folder_name == "%c")
                {
                    Console.WriteLine("Folder creation was canceled");
                    return;

                } else
                {
                    new_folder_name = Path.Combine(CurrentDirectory, new_folder_name);

                    Directory.CreateDirectory(new_folder_name);
                    CurrentDirectory = new_folder_name;

                    Console.WriteLine($"Created new folder: {Path.GetDirectoryName(CurrentDirectory)}");
                    return;
                }
            }
        }

        public static void GoBack()
        {
            string? parent = Path.GetDirectoryName(CurrentDirectory);

            if (parent != null)
            {
                CurrentDirectory = parent;

            } else
            {
                Console.WriteLine("Cannot go back further than this. :(");
            }
        }

        public static void PrintListOfFiles()
        {
            try
            {
                var enteries = Directory.EnumerateFileSystemEntries(CurrentDirectory);
                LoadedEntries = new();

                int counter = 0;
                foreach (var entry in enteries)
                {
                    LoadedEntries.Add(entry);
                    counter += 1;
                    Console.WriteLine($"{counter}. {Path.GetFileName(entry)}");
                }

            } catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Path not found,\n request failed with exeption: {ex}");
                
            } catch (Exception ex)
            {
                Console.WriteLine($"Cancel the proecces with exeption: {ex}");
            }
        }

        public static void UpdateLog()
        {
            Console.Clear();
            Console.WriteLine($"Current Directory: {CurrentDirectory}");
            FileSystem.PrintListOfFiles();
        }
    }
}
