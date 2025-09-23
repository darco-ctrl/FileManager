using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Management;
using System.Threading.Tasks;


namespace FileManager
{
    public static class FileManager
    {
        public static ObservableCollection<FileItemViewModel> FetchThisPC()
        {
            // Drives to return into 'SidePanelItems' in 'MainWindowViewModel.cs'
            ObservableCollection<FileItemViewModel> drivesList = new();

            // Gets all drives and saves in drives
            DriveInfo[] drives = DriveInfo.GetDrives();
            
            foreach (var drive in drives)
            {
                // Checks if drive is Ready or not dont complain me thats littrally the description says too -_-
                if (drive.IsReady)
                {
                    // Add into 'drivesList' straight by creating 'FileItemViewModel'
                    drivesList.Add(
                        new FileItemViewModel
                        {
                            Name = $"{drive.VolumeLabel} ({drive.Name})",
                            HoldingPath = drive.VolumeLabel,
                        });
                }
            }
            return drivesList; // well who knows what this line does :D ig you have to figure this out yourself
        }

        // Idk why i named that i didnt get any better name so its that 
        // so this code checks if anything is added like any other device for example
        // your USB stick or something similar to it this is running in different thread
        public static void StartExternalDrivesWatcher()
        {
#pragma warning disable CA1416 // Validate platform compatibility


            // This basically listen for system events
            ManagementEventWatcher watcher = new ManagementEventWatcher();

            watcher.Query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent");

            ///////// STOPEDD HEREE //////////////////////

#pragma warning restore CA1416
        }
    }
}
