using System;
using System.Collections.Generic;
using Avalonia.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace FileManager.Input
{
    public class KeyAction
    {
        public Key[] ActionKey;

        public KeyAction(Key[] keys)
        {
            ActionKey = keys;                        
        }   
    }
}
