using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;
using FileManager.Core;
using FileManager.Data;
using FileManager.Groups;
using FileManager.Utils;

namespace FileManager.Input.Actions
{
    public class KeyOpenAction
    {

        private ExtGroup _extGroup;
        private DataManager.Applications _app;

        public KeyOpenAction(ExtGroup ext_group, DataManager.Applications app)
        {
            _extGroup = ext_group;
            _app = app;
        }

        public void Trigger()
        {
           
        }
    }
}
