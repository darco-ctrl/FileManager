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
        private Dictionary<int, string> KeyMap = new();

        public KeyOpenAction(Dictionary<int, string> _keyMap)
        {
            KeyMap = _keyMap;
        }

        public void TryTrigger(int _keyID, string arg)
        {
            if (KeyMap.TryGetValue(_keyID, out string? app))
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = arg;
                psi.FileName = app;
                psi.UseShellExecute = true;

                Process.Start(psi);
                
            }
        }   
    }
}
