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

        private string extGroupKey;
        private DataManager.Applications _app;
// LEFT HERE
        public KeyOpenAction(string ext, DataManager.Applications app)
        {
            extGroupKey = ext;
            _app = app;
        }

        public void Trigger()
        {
           
        }
    }
}
