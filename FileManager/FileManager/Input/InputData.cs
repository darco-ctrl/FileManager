using System;
using System.Collections.Generic;
using Avalonia;
using FileManager.Data;
using Avalonia.Input;
using FileManager.Core;
using DatMan = FileManager.Data.DataManager;
using FileManager.Input.Actions;
using System.Collections.Specialized;
using FileManager.Utils;


namespace FileManager.Input
{
    public class InputData
    {

        
        public Dictionary<string, KeyOpenAction> FileTypeIDSet = new();

        public InputData()
        {
            CreateDefaultKeySet();
            
        }
        
        private void CreateDefaultKeySet()
        {
            HashSet<Key> _keys = new HashSet<Key>
            {
                Key.LeftCtrl,
                Key.V
            };

            Dictionary<int, string> _keyMap = new();
            _keyMap.Add(_keys.GetIntID(4), @"C:\Users\nihal\AppData\Local\Programs\Microsoft VS Code\Code.exe");

            KeyOpenAction _keyOpenAction = new KeyOpenAction(_keyMap);

            InputManager.PrintKeys(_keys);

            FileTypeIDSet.Add(".txt", _keyOpenAction);
        }
    }
}
