using Avalonia.Controls;
using FileManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Managers
{
    public static class MenuManager
    {


        public static void OpenGetNameWindow(FileOperation.OperationState state)
        {
            EntryCreationWindow NemeReturnWindow = new EntryCreationWindow();

            Console.WriteLine("MenuManager Requested to create entry");

            string _title = "";
            string _waterMark = "";

            if (state == FileOperation.OperationState.CREATE_FOLDER)
            {
                _title = "Folder";
                _waterMark = "Folder name";
            } else if (state == FileOperation.OperationState.CREATE_FILE)
            {
                _title = "File";
                _waterMark = "File name";
            }

            NemeReturnWindow.ShowWindow(FileOperation.CreatingDecider, _title, _waterMark, state);

        }
    }
}
