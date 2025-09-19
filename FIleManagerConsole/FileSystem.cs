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
                CurrentDirectory = LoadedEntries[num - 1];
                UpdateLog();
                
            } else
            {
                Console.WriteLine("Out of bound.");
                UpdateLog();
            }
        }

        public static void CreatFolder()
        {

        }

        public static void GoBack()
        {
            string? parent = Path.GetDirectoryName(CurrentDirectory);

            if (parent != null)
            {
                CurrentDirectory = parent;
                UpdateLog();

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
