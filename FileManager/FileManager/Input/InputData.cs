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

               
        }
        
        public void CreateDefaultKeySet()
        {
            
        }
    }
}
