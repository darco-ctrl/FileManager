using System;
using System.Collections.Generic;
using Avalonia;
using FileManager.Data;
using Avalonia.Input;
using FileManager.Core;
using DatMan = FileManager.Data.DataManager;
using FileManager.Input.Actions;


namespace FileManager.Input
{
    public class InputData
    {

        public Dictionary<KeyAction, KeyOpenAction> ActionSet = new();

        public InputData()
        {
            CreateDefaultKeySet();
        }
        
        public void CreateDefaultKeySet()
        {
            KeyAction _keyAction = new KeyAction(new HashSet<Key>
            {
                Key.LeftCtrl,
                Key.V
            });

            KeyOpenAction _keyOpenAction = new KeyOpenAction(DatMan.Current.AppsPath["vs"]);
        }
    }
}
