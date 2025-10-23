using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;
using FileManager.Core;
using FileManager.Data;
using FileManager.Utils;

namespace FileManager.Input
{
    public class OpenWithKeyAction
    {
        public HashSet<DataManager.FileTypes> FileType;
        public Dictionary<KeyAction, DataManager.Applications> KeySet = new();

        public OpenWithKeyAction
        (
            HashSet<DataManager.FileTypes> _fileType,
            KeyAction[] _keyActions,
            DataManager.Applications[] _values
        )
        {
            FileType = _fileType;
            KeySet.AddRange(_keyActions, _values);
        }

        public void Triggered()
        {
            
        }
    }
}
