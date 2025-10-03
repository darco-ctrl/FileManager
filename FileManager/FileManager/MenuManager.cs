using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class MenuManager
    {


        public static void CreateFileCreationWindow(bool isCreatingFolder)
        {
            EntryCreationWindow fileCreationWindow = new EntryCreationWindow();

            Console.WriteLine("MenuManager Requested to create entry");
            fileCreationWindow.ShowWindow(isCreatingFolder);
            
        }

    }
}
