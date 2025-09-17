using System;
using System.IO;
using System.Reflection.PortableExecutable;

namespace FIleManagerConsole
{
    internal static class FileSystem
    {
        public static string current_dir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        public static void PrintListOfFiles()
        {
            try
            {
                var entry = Directory.EnumerateFileSystemEntries(current_dir);

                int counter = 0;
                foreach (var dir in entry)
                {
                    counter += 1;

                    Console.WriteLine($"{counter}. {dir}");
                }

            } catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Path not found,\n request failed with exeption: {ex}");
                
            } catch (Exception ex)
            {
                Console.WriteLine($"Cancel the proecces with exeption: {ex}");
            }
        }
    }
}
