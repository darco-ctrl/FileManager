using System;
using System.Collections.Generic;
using Avalonia;
using FileManager.Data;
using Avalonia.Input;
using FileManager.Core;
using DatMan = FileManager.Data.DataManager;
using FileManager.Input.Actions;
using FileManager.Groups;

namespace FileManager.Input
{
    public class InputData
    {

        public Dictionary<KeyAction, KeyOpenAction> ActionSet = new();

        public Dictionary<string, ExtGroup> ExtGroups = [];

        public InputData()
        {
            CreateDefaultKeySet();
        }

        private void CreateExtGroup()
        {
            string _name = "text docs";
            ExtGroup _extensionGroup = new ExtGroup(new HashSet<string>
            {
                ".txt",
                ".cs",
                ".py"
            });

            ExtGroups.Add(_name, _extensionGroup);

            _name = "photo";
            _extensionGroup = new ExtGroup(new HashSet<string>
            {
                ".png",
                ".jpg"
            });

            ExtGroups.Add(_name, _extensionGroup);

        }
        
        public void CreateDefaultKeySet()
        {
                        
        }
    }
}
