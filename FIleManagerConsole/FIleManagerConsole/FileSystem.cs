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

        /*
         * as the name says goes to next dir
         * 
         * 'LoadedEntries' stores current dirs' 
         * stuff into 'LoadedEntries' which is a list
         * 
         * then if user want to move to any dir in that
         * they type in number the dir is in  in the output
         * it will be like
         * 
         * 0. folder_one/            (folder/dir)
         * 1. this_is_file_3.txt     (file)
         * 2. folder_two/            (folder/dir)
         * 
         * when function is called it gets the user_input which is converted to
         * 'int' and given to GoToDir arguement as 'num' variable and after 
         * getting num it acces directly from 'LoadedEntries' from by index
         * 
         * this ofc checks if the entry is dir/folder or file cuz you cant
         * go in to a file bro
         */
        public static void GoToDir(int num)
        {
            if (num > 0 && num <= LoadedEntries.Count())
            { 

                if (Directory.Exists(LoadedEntries[num])) {
                    CurrentDirectory = LoadedEntries[num];

                } else
                {
                    Console.WriteLine("That is not a folder");
                }


            } else
            {
                Console.WriteLine("Out of bound.");

            }
        }

        /*
         * this function 'CreateFolder' creates a folder 
         * 
         * -- yay no one expected that, that was a bad joke wasnt it sorry
         * 
         * after this is called it ask to enter a name of folder, process can be
         * canceled using '%c' after getting name of folder it is then combined
         * using 'Path.Combine()' to build full path of that new folder and then using
         * 'Directory.CreateDirectory()' it creates the dir/folder using that full path
         * pretty straight forward ig
         * 
         * btw i build full path by basically adding 'Name' to the 'CurrentDirectory'
         * like this
         * 'new_folder_name = Path.Combine(CurrentDirectory, Name);'
         */
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

        /*
         * same as making file but this creates folder 
         * all process is same you get an input then build full path
         * from the name and instead of 'Directory.CreateDirectory' we 
         * use File.Create :D
         * 
         * btw both checks if a folder by that name exists and make sure it does
         * forget to tell that earlier :P
         */
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

        /*
         * this just deletes the entry ya all function names are self explanatory
         * 
         * -- sorry if i mispelled explanatory, you will see more later
         * 
         * -- after this function is called it askes for name of file dont ask 
         * -- me why i didnt do it asking for num wait i could add both :D
         * 
         * so it askes for either name_of_folder in current dir or index of it 'count'
         * and it builds full path from it if its name using Path.Combine if its an
         * index it directly acces from LoadedEntries
         * 
         * after getting entry_path from either of method the loop breaks 
         * after that it asks for a aconfirmation and then it 
         * goes into if its Directory or File
         * then delete it using 'Directory.Delete' or 'File.Delete' if its file
         * 
         * in 'Dictionary.Delete' there is 'recrusive:true' be defualt its false
         * which means if there is something inside the dir/folder it will give error
         * it cant only delete empty folder so we use recrusive to delete everything
         * inside it as well
         */
        public static void DeleteEntry()
        {

            string? input = "";
            string entry_path;

            while (true)
            {
                Console.WriteLine("Which folder or file would you like to delete in this dir\n you can enter num as well according to above count");
                input = Console.ReadLine();

                if (int.TryParse(input, out int num))
                {
                    entry_path = LoadedEntries[num];
                    break;

                } else if (!string.IsNullOrWhiteSpace(input)) {
                    entry_path = Path.Combine(CurrentDirectory, input);

                    if (Path.Exists(entry_path))
                    {
                        break;
                    }
                    Console.WriteLine("Path doesn not exits try again");
                    entry_path = "";
                } else
                {
                    Console.WriteLine("Type something in bruh");
                } 
                
            }


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
                            Directory.Delete(entry_path, recursive:true);
                            Console.WriteLine($"Deleted dir, {input}");

                        } else if (File.Exists(entry_path))
                        {
                            File.Delete(entry_path);
                            Console.WriteLine($"Deleted file, {input}");
                        } else
                        {
                            Console.WriteLine("Path not found or this is not a file nor folder");
                        }
                        Console.ReadLine();
                        return;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"deleting file failed exited with error: {ex}");
                        Console.ReadLine();
                        return;
                    }
                }
                else if (yes_or_no == "n")
                {
                    Console.WriteLine("Canceling. . .");
                    Console.ReadLine();
                    return;
                }
            }
        }
        /*
         * this the function that makes go back 1 like when you type .. it goes back 1 
         * 
         * i used 'Path.GetDirectoryName()' to get the parent cuz if you have a path
         * lets say your in "C:\Users\you\Documents\folder" (so 'CurrentDirectory = C:\Users\you\Documents\folder')
         * 
         * when you use 'Path.GetDirectoryName()' you get parents name 
         *                             this is the 'CurrentDirectory' the folder we are in right now
         *       -----------------------vvvvvv
         * like "C:\Users\you\Documents\folder"
         *       -------------^^^^^^^^^--------
         *     
         * now when 'Path.GetDirectoryName()' appled it returns "C:\Users\you\Documents" since thats the parent folder
         * i think you got it 
         * basicaly 'Path.GetDirectoryName()' gets the name of dirctory in which the given entry (path) is in
         * 
         * notice how 'parent' is string? not string because 'Path.GetDirectoryName' can return null for example if
         * your in C:\ and tried going back it wont cuz thats the furthest it can get
         */
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


        /*
         * this is the function that prints content of 'CurrentDirecotry' and before printing it prints with a count
         * so user can navigate easielr
         * 
         * -- idk why am thinking this since this is just prototype :/ am the only one who gona use this
         * 
         * so we get a Iterator (i think thats what that called the thing that goes in for loop)
         * like 
         * in this
         * 
         * foreach (var entry in enteries)
         * ----------------------^^^^^^^^
         * that is iterator idk why am showing that everyone one knows that
         * 
         * after that i clear LoadedEntries cuz its probably gona be new dir we are in yea i didnt add a check if path 
         * changed
         * 
         * so yea using 'Directory.EnumerateFileSystemEntries(CurrentDirectory)'
         * i get iterator then iterat through all of them with this for loop that is 'foreach (var entry in enteries)'
         * 
         * then it gets added to 'LoadedEntries'
         * so can be accesesd later
         * the printed into console output in this formate -> $"{counter}. {Path.GetFileName(entry)}"
         * 
         * so it would look like 
         * "1. Folder1"
         * 
         * these are all in an try catch cuz 'EnumerateFileSystemEntries' has expetions so to catch those
         */
        public static void PrintListOfFiles()
        {
            try
            {
                var enteries = Directory.EnumerateFileSystemEntries(CurrentDirectory);
                LoadedEntries = new();

                ushort counter = 0;
                foreach (var entry in enteries)
                {
                    LoadedEntries.Add(entry);
                    Console.WriteLine($"{counter}. {Path.GetFileName(entry)}");
                    counter += 1;
                }

            } catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine($"Path not found,\n request failed with exeption: {ex}");
                
            } catch (Exception ex)
            {
                Console.WriteLine($"Cancel the proecces with exeption: {ex}");
            }
        }

        /*
         * this is fd argument builder 
         * 
         * this asks for stuff and search acccording to that
         * 
         * in mine there is 
         * 
         * 1. Options like what to search (file or all or folders or symlinks)
         * 2. then can be choosed path #@# for current path or enter path
         * 3. ask for if saerch for hidden file it adds  -H there is ig another one --no-ignore something like that
         *    like it doesnt ignore the ignore files like .gitignore 
         * 4. what to search its kinda complecated i just know 4 stuff
         *    ^r - measn it should start with r
         *    *.txt - end with .txt
         *    ^r*.txt - start with r and end with .txt
         *    findme.txt - search for exactly findme.txt
         *    
         * after building arguement it sends the arguemtn and call search 'Search.SearchFor(arguement)'
         * which return output string and prints :D
         *    
         * if you wana know more about fd checkout its git
         * link -> https://github.com/sharkdp/fd/blob/master/README.md
         */
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

            // ASKING FOR OPTIONS
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

            // ASKING FOR WHAT TO SEARCH :/
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

            // ENTER WHERE TO SEARCH
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

            // SEARCH FOR HIDDEN FILE??
            // -- bro seriously? just read above and read this lines below Console.WriteLine T_T
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

            //CASE SENSITIVE SEARCH? this is waste bruh lol
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
            Console.ReadLine(); // a pause to see your arguement
            Console.WriteLine(Search.SearchFor(arguement));
            Console.ReadLine(); // a pause to see your result

            // listning to dimaak kharabh
        }
        
        public static void UpdateLog()
        {
            Console.Clear();
            Console.WriteLine($"Current Directory: {CurrentDirectory}");
            FileSystem.PrintListOfFiles();
        }
    }
}
