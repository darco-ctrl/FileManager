using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Input;
using FileManager.Core;
using FileManager.Data;
using FileManager.Managers;
using FileManager.Utils;
using Microsoft.VisualBasic;

namespace FileManager.Input.Actions
{
    public class KeyOpenAction
    {
        private string _appPath;

        public KeyOpenAction(string app_path)
        {
            _appPath = app_path;
        }

        public void Trigger()
        {
            string? appPath = FileSystemManager.GetSelectedItemPath();
            if (appPath == null) return;

            ProcessStartInfo process = new ProcessStartInfo();
            process.Arguments = AppState.GetWindowViewModel().CurrentWorkingDir;
            process.FileName = appPath;
            Process.Start(process);
        }   
    }
}
