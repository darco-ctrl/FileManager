using System;
using System.Collections.Generic;
using Avalonia.Input;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace FileManager.Input.Actions
{
    public class KeyAction
    {
        public HashSet<Key> ActionKey;

        public KeyAction(HashSet<Key> keys)
        {
            ActionKey = keys;                        
        }   
    }
}
