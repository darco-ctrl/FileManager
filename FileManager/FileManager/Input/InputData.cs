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

        public List<KeyOpenAction> KeyOpenActions = new();

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

            KeyOpenAction _keyOpenAction = new KeyOpenAction();

            _keyOpenAction.FileTypes.Add(".txt");
            _keyOpenAction.KeyToAppMap.Add(_keys.GetIntID(4), @"C:\Users\nihal\AppData\Local\Programs\Microsoft VS Code\Code.exe");

            KeyOpenActions.Add(_keyOpenAction);
        }
    }
}
