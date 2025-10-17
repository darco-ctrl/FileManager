using FileManager.Core;
using System;
using System.IO;

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
            string _text = "";

            if (state == FileOperation.OperationState.CREATE_FOLDER)
            {
                _title = "Folder";
                _waterMark = "Folder name";
            } else if (state == FileOperation.OperationState.CREATE_FILE)
            {
                _title = "File";
                _waterMark = "File name";
            } else if (state == FileOperation.OperationState.RENAME)
            {
                _title = "Rename";
                _waterMark = "New name";



                if (Directory.Exists(ControlsManager.RenameEntry))
                {
                    _text = new DirectoryInfo(ControlsManager.RenameEntry).Name;
                } else
                {
                    _text = $"{Path.GetFileName(ControlsManager.RenameEntry)}";
                }
            }

            NemeReturnWindow.ShowWindow(FileOperation.CreatingDecider, _title, _waterMark, state, _text);

        }
    }
}
