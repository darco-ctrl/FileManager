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
using FileManager.ViewModels;
using Microsoft.VisualBasic;

namespace FileManager.Input.Actions
{
    public class KeyOpenAction
    {
        public Dictionary<int, string> KeyToAppMap = new();

        public HashSet<string> FileTypes = new();

        public KeyOpenAction()
        {
            
        }

        public void TryTrigger(int _keyID)
        {
            if (!KeyToAppMap.ContainsKey(_keyID)) return;



            string _applicationPath = KeyToAppMap[_keyID];
            string? _arg = FileSystemManager.GetSelectedItemPath();

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _applicationPath,
                Arguments = _arg,
                UseShellExecute = true
            };

            Process.Start(psi);

        }   
    }
}
