using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Text.Json.Serialization;
using System.Diagnostics;

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

        public static void CreateFile() 
        {
            while (true)
            {
                Console.WriteLine("'%c' to cancel\n Enter folder name: ");
                string? new_folder_name = Console.ReadLine();

                if (new_folder_name == null) { continue; }

                if (new_folder_name == "%c")
                {
                    Console.WriteLine("Folder creation was canceled");
                    return;

                }
                else
                {
                    new_folder_name = Path.Combine(CurrentDirectory, new_folder_name);

                    if (File.Exists(new_folder_name))
                    {
                        Console.WriteLine("a file with that name already exists");
                    }
                    else
                    {
                        File.Create(new_folder_name);

                        Console.WriteLine($"Created new file: {Path.GetDirectoryName(new_folder_name)}");
                        return;
                    }
                }
            }
        }

        public static void DeleteEntry()
        {

            string? input = "";

            do
            {
                Console.WriteLine("Which folder or file would you like to delete in this dir");
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
      

            string entry_path = Path.Combine(CurrentDirectory, input);

            Console.WriteLine($"Are you sure you want to delete y/n\n ->: {entry_path}");
            string? yes_or_no;

            while (true)
            {
                yes_or_no = Console.ReadLine();

                if (yes_or_no == null)
                {
                    Console.WriteLine("No input recived");
                }
                else if (yes_or_no == "y")
                {
                    try
                    {

                        if (Directory.Exists(entry_path))
                        {
                            Directory.Delete(entry_path);
                            Console.WriteLine($"Deleted dir, {input}");

                        } else if (File.Exists(entry_path))
                        {
                            File.Delete(entry_path);
                            Console.WriteLine($"Deleted file, {input}");
                        } else
                        {
                            Console.WriteLine("Path not found");
                        }
                        return;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"deleting file failed exited with error: {ex}");
                        return;
                    }
                }
                else if (yes_or_no == "n")
                {
                    Console.WriteLine("Canceling. . .");
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

        public static void SearchM()
        {
            Console.WriteLine("fd [PATTERN] [PATH] [OPTIONS]\n");
            Console.WriteLine(@"
What pattern would you like to search for:\n
 '.' for all entries (eg: files, folder, symlinks)
 'f' for all files
 'd' for all dir
 'l' for all symlinks
");

            string? arguement;

            while (true) {
                arguement = Console.ReadLine();

                if (arguement == null)
                { continue; }
                if (arguement == "e")
                { return; }

                if (arguement != "f" && arguement != "." && arguement != "d" && arguement != "l")
                {
                    Console.WriteLine("Invlaid input try again");
                    continue;
                }

                if (arguement == ".")
                {
                    arguement = "";
                    break;
                }

                arguement = "-t " + arguement;
                break;
            }

            Console.WriteLine("waht do you wana search for");

            while (true)
            {
                string? look_for = Console.ReadLine();
                
                if (!string.IsNullOrWhiteSpace(look_for))
                {
                    arguement += " " + look_for;
                    break;
                }
            }

            Console.WriteLine("where would you wana search in #@# to search current dir");

            while (true)
            {
                string? path_to_search = Console.ReadLine();

                if (path_to_search == "#@#")
                {
                    arguement += " " + CurrentDirectory;
                    break;
                }
                
                if (!string.IsNullOrWhiteSpace(path_to_search) && Path.Exists(path_to_search))
                {
                    arguement += " " + path_to_search;
                    break;
                }
            }

            Console.WriteLine("search for hidden file? y/n");

            while (true)
            {
                string? search_for_hidden_file = Console.ReadLine();

                if (search_for_hidden_file == "y")
                {
                    arguement += " -H";
                    break;
                }
                else if (search_for_hidden_file == "n")
                {
                    break;
                }

            }

            Console.WriteLine("run case sensitive search y/n");

            while (true)
            {
                string? case_sensitive = Console.ReadLine();

                if (case_sensitive == "y")
                {
                    arguement += " -s";
                    break;

                } else if (case_sensitive == "n") {
                    arguement += " -i";
                    break;
                }
            }

            arguement += " -p";

            Console.WriteLine($"arguement: {arguement}");
            Console.ReadLine();
            Console.WriteLine(Search.SearchFor(arguement));
            Console.ReadLine();
        }

        /*
        public static void Search()
        {
            Console.WriteLine(@"
            What would you like to search
            *.txt to match this to right side
            *abc* to check if it exists in it
            file* to see left side of file
            ");

            string? searchPattern;

            do
            {
                searchPattern = Console.ReadLine();

            } while (string.IsNullOrWhiteSpace(searchPattern));

            var files = SafeEnumerateFiles(CurrentDirectory, searchPattern, SearchOption.AllDirectories);

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };

            var sw = Stopwatch.StartNew();
            Console.WriteLine("Search started you will not able to enter input");
            Program.CanRun = false;

            Parallel.ForEach(files, options, (file) =>
            {
                try
                {
                    Console.WriteLine(file);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"[Access Denied] {file}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {file} - {ex.Message}");
                }

            });

            sw.Stop();
            Console.WriteLine($"Search completed\n Elapsed Time: {sw.ElapsedMilliseconds} ms \n {sw.Elapsed.TotalSeconds:F2} seconds");
            var i = Console.ReadLine();
            Program.CanRun = true;

            static IEnumerable<string> SafeEnumerateFiles(string path, string searchPattern, SearchOption option)
            {
                Queue<string> dirs = new Queue<string>();
                dirs.Enqueue(path);

                while (dirs.Count > 0)
                {
                    string currentDir = dirs.Dequeue();
                    string[] subDirs = Array.Empty<string>();
                    string[] files = Array.Empty<string>();

                    try
                    {
                        files = Directory.GetFiles(currentDir, searchPattern);
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (Exception) { }

                    foreach (var file in files)
                        yield return file;

                    if (option == SearchOption.AllDirectories)
                    {
                        try
                        {
                            subDirs = Directory.GetDirectories(currentDir);
                            foreach (var subDir in subDirs)
                                dirs.Enqueue(subDir);
                        }
                        catch (UnauthorizedAccessException) { }
                        catch (Exception) { }
                    }
                }
            }
        }
        */
        
        public static void UpdateLog()
        {
            Console.Clear();
            Console.WriteLine($"Current Directory: {CurrentDirectory}");
            FileSystem.PrintListOfFiles();
        }
    }
}
