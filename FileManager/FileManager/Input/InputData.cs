using System;
using System.Collections.Generic;
using Avalonia;
using FileManager.Data;
using Avalonia.Input;
using FileManager.Core;
using DatMan = FileManager.Data.DataManager;

namespace FileManager.Input
{
    public class InputData
    {

        public List<OpenWithKeyAction> ActionSet = new();

        public InputData()
        {

        }
        
        // for now i am gona creat invidula function for each action 

        private void CreateTextAction() // TEST 1
        {
            KeyAction[] _actionKey = new[]
            {
                new KeyAction( new[] { Key.LeftShift, Key.T }),
                new KeyAction( new[] { Key.LeftShift, Key.V })
            };

            DataManager.Applications[] _apps = new[]
            {
                DatMan.Applications.NOTEPAD,
                DatMan.Applications.VISUAL_STUDIO_CODE
            };

            HashSet<DatMan.FileTypes> _fileTypes = new();
            _fileTypes.Add(DatMan.FileTypes.TXT);
            _fileTypes.Add(DatMan.FileTypes.CS);

            OpenWithKeyAction _action = new OpenWithKeyAction(
                _fileTypes,
                _actionKey,
                _apps
            );
        }
    }
}
