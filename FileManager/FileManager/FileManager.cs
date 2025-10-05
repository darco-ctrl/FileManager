using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;
using Avalonia.Threading;
using FileManager.Views;
using Avalonia.Controls;
using Avalonia.Input;


namespace FileManager
{
    public static class FileManager
    {
        /*
         * this is the function called at the starting inside App.axaml.cs
         * and this is called to display and update UI on first 
         */
        public static void StartUpSetup()
        {

            AppState.GetWindowViewModel().SetCurrentDir(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
        }

        /*
         * when ever CurrentDir inside MainWindowViewModel is changed this function is called
         * to display new dir list of CurrentDir 
         * 
         * IF YOU NEED FURTHER EXPLANATION FOR THIS CHECK CONSOLE VERSION OF THIS
         */
        public static void RefreshDir()
        {
            DynamicControlManager.ResetButtonSelection();

            Console.WriteLine("Clearing and refresing current dir");
            AppState.GetWindowViewModel().CurrentLoadedEntires.Clear();
            var entries = Directory.EnumerateFileSystemEntries(AppState.GetWindowViewModel().CurrentWorkingDir).ToArray();

            foreach (var entry in entries)
            {   
                if (!FileManagerHelper.IsEntryInBlackList(entry)) { continue; }

                EntryItemViewModel entryItem = DynamicControlManager.CreateEntryItem(entry);
                AppState.GetWindowViewModel().CurrentLoadedEntires.Add(entryItem);
                //Console.WriteLine($"{entry}");
            }
        }
        /*
         * When pressed Backspace or asgined key for GoBackOne in 'InputManager.KeyActionSet'
         * this makes go back one step if user is in
         * C:\user\user_name
         * when function is called on this path it goes into 
         * C:\user
         * and set this as CurrentWorkingDir
         * 
         */
        public static void GoBackOne()
        {
            string? parent = Path.GetDirectoryName(AppState.GetWindowViewModel().CurrentWorkingDir);

            if (parent != null)
            {
                AppState.GetWindowViewModel().SetCurrentDir(parent);
            }
        }

        /*
         * This is manual way to make PathTextBox TwoWay i didnt use avalonia
         * Mode=TwoWay becuase the user input could be a preall path or not 
         * so i made manual
         * 
         * after PathTextBox enters TextChanging and Enter key is pressed tis Function is called
         * this function checks if Dir exists (not path itself) if its file it wont work either 
         * it must be a Directory
         */
        public static void PathBoxTryingToSetNewPath(string? path)
        {
            if (Directory.Exists(path))
            {
                AppState.GetWindowViewModel().SetCurrentDir(path);
            } else
            {
                AppState.GetWindow().UpdatePathBlockText();
            }
            AppState.GetWindow().FocusWindow();
        }


        public static void CreateFile(string fileName)
        {
            string FilePath = Path.Combine(AppState.GetWindowViewModel().CurrentWorkingDir, fileName);

            File.Create(FilePath);
            RefreshDir();
        }

        public static void DeleteEntry(string entryPath)
        {
            FileOperation.DeleteItem(entryPath);
        }

        

        public static void CreateDir(string dirName)
        {
            string DirPath = Path.Combine(AppState.GetWindowViewModel().CurrentWorkingDir, dirName);

            Directory.CreateDirectory(DirPath);
            RefreshDir();

            Console.WriteLine("CreatedFile");
        }
         
        /*
         * this runs at the start of program
         * fetching all current drives inside pc
         * for example any USB's or drives like HDD's or SSD's
         */
        public static ObservableCollection<DriveItemViewModel> FetchThisPC()
        {
            Console.WriteLine("--------  Recived to make  ------------");
            // Drives to return into 'SidePanelItems' in 'MainWindowViewModel.cs'
            ObservableCollection<DriveItemViewModel> drivesList = new();

            // Gets all drives and saves in drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            
            foreach (var drive in drives)
            {
                // Checks if drive is Ready or not dont complain me thats littrally the description says too -_-
                if (drive.IsReady)
                {
                    // Add into 'drivesList' straight by creating 'DriveViewModel'
                    drivesList.Add(
                        new DriveItemViewModel
                        {
                            Name = $"{drive.Name}",
                            VolumeLabel = drive.VolumeLabel,
                        });
                }
            }
            return drivesList; // well who knows what this line does :D ig you have to figure this out yourself
        }

        // Idk why i named that i didnt get any better name so its that 
        // so this code checks if anything is added like any other device for example
        // your USB stick or something similar to it this is running in different thread
        public static void StartExternalDrivesWatcher(MainWindowViewModel mainWindowVM)
        {
#pragma warning disable CA1416 // Validate platform compatibility


            // This basically listen for system events
            // this watcher which is 'ManagementEventWatcher' this is a class from System.Management
            // that listens for Windows Management Instrumentation (WMI) events and
            // for us we are looking for specific event which is is a driver added or removed or configured that is next line
            ManagementEventWatcher watcher = new ManagementEventWatcher();

            // like i said above we only need Change in drive info like drive added or removed or configed
            // so we use 'Win32_VolumeChangeEvent' to get those all like drive added or removed  stuf like that
            // and 'SELECT *' selects everything in that like all type of event in 'Win32_VolumeChangeEvent'
            watcher.Query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent");


            // so this activates when a WMI event is occured
            // if so we subscribe (or lambda) to it 
            // where 'sender' is our 'watcher'
            // and e is the arguement which contains event data that we need

            // -- I HOPE THIS IS UNDERSTANDABLE T_T
            watcher.EventArrived += (sender, e) =>
            {
                // so using 'e.NewEvent' that contains WMI event data
                // we get 'EventType' property
                // EventType Code:
                //     1 = config change of the drive/volum
                //     2 = Drive added
                //     3 = drive removed
                // and using 'Convert.ToInt32' we convert it into int
                int eventType = Convert.ToInt32(e.NewEvent["EventType"]);


                /*
                 * string? cuz it may be null here :/
                 * we are getting Drive name as name says :D
                 * we get it using 'e.NewEvent["DriveName"] turn it into string and store in 'driveName'
                 */
                string? driveName = e.NewEvent["DriveName"]?.ToString();

                if (driveName != null)
                {
                    /*
                     * This is to change stuff in UI or update UI 
                     * since Avalonia UI updates in UIthread 
                     * we cant do in different thread which watcher is runnning so we doing this 
                     * i will try to explain as i can
                     * 
                     * 'Dispatcher' its an class in 'Avalonia.Threading' which manages threading for the UI
                     * it just make sure all UI stuff update in 'UIThread'
                     * 
                     * 'UIThread' this is main Ui thread where all ui is updated in avalonia
                     * 
                     * 'InvokeAsync()' this schedule a code to run on 'UIThread' asynchronously
                     * here it does not block the thread it just queues it and run it later so its scheduling like i said
                     * 
                     * and Lambda experssion a mini Function i would say
                     * () => {}
                     * ^^
                     * parameters for the functions inside or code block inside '{}' so quick way to make a function without making a function?
                     * you can imagin as you want basically it just makes a mini function yes ofc you can make a function outside and call that but
                     * you would have to pass everything needed like 'eventType' and stuff
                     * 
                     * here we paremters empty as i know they are not empty most of the time when subscribed like
                     * += (sender, e) like we did above
                     */
                    Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        

                        if (eventType == 2) // if drive is added
                        {
                            DriveItemViewModel driveItem = new DriveItemViewModel
                            {
                                Name = driveName,
                                //VolumeLabel = e.NewEvent[""] will do tmr
                            };

                            if (!mainWindowVM.SidePanelItems.Contains(driveItem))
                            {
                                mainWindowVM.SidePanelItems.Add(driveItem);
                            }
                        }
                        else if (eventType == 3) // if drive is removed
                        {
                            DriveItemViewModel? driveToRemove = mainWindowVM.SidePanelItems.FirstOrDefault(drive => drive.Name == driveName);

                            if (driveToRemove != null)
                            {
                                mainWindowVM.SidePanelItems.Remove(driveToRemove);
                            }
                        }
                    });
                }
            };

            // as it says watcher starts so it starts listening for events (WMI events ) from here on
            watcher.Start();

#pragma warning restore CA1416
        }

        
    }
}
