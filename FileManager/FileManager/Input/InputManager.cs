using System;
using System.Collections.Generic;
using Avalonia.Input;
using FileManager.Core;
using FileManager.Utils;
using FileManager.ViewModels;
using Avalonia.VisualTree;
using Avalonia;
using Avalonia.Controls;
using FileManager.Managers;
using System.Linq;
using FileManager.Input.Actions;
using System.Transactions;
using System.IO;

namespace FileManager.Input
{
    public static class InputManager
    {
        public static InputData Current = new(); 

        // KeyDown stores all current key pressed the key is removed from KeyDown when OnKeyUp function is triggered
        private static readonly HashSet<Key> _KeyDown = new();
        private static Dictionary<Key, Action> KeyActionSet = new(); // All keys and functions that tell what it should do
        private static readonly HashSet<Key> IsPressed = new(); // this is to prevent held action

        // I made key's HashSet and Dictionary becuase its faster than checking each key with if statement
        // with this i can just check if KeyActionSet and get the value all in O(1)

        // INPUT MANAGE CONSTRUCTOR
        public static void Init()
        {
            KeyActionSet.Add(Key.Back, FileSystemManager.GoBackOne); // Adding Backspace key so user can go back from a dir
            KeyActionSet.Add(Key.Enter, EnterKeyFunction); // Adding Enter key

        }

        // When any KeyisDown its added to KeyDown and check if the Key Pressed is in 'KeyActionSet' if so
        // call the action connected to that
        public static void OnKeyDown(KeyEventArgs e)
        {
            if (_KeyDown.Contains(e.Key)) return;
            _KeyDown.Add(e.Key);

            CheckForAction();

            if (KeyActionSet.ContainsKey(e.Key) && !IsPressed.Contains(e.Key))
            {
                KeyActionSet[e.Key]();
                IsPressed.Add(e.Key);
            }
        }

        private static void CheckForAction()
        {
            ListBox _listBox = AppState.GetWindow().MainEntryList;
            if (_listBox.SelectedItem == null) return;

            EntryItemViewModel? _entryItem = _listBox.SelectedItem as EntryItemViewModel;
            if (_entryItem == null) return;

            string _extension = Path.GetExtension(_entryItem.HoldingPath);

            KeyOpenAction _keyOpenAction = Current.FileTypeIDSet[_extension];
            _keyOpenAction.TryTrigger(GetKeyDownID());
        }
        
        public static int GetKeyDownID()
        {
            return _KeyDown.GetIntID(4);
        }

        public static void PrintKeys(HashSet<Key> keys)
        {
            Console.WriteLine("\nPrinting Key\n");
            Key[] _keys = keys.ToArray<Key>();
            foreach (Key _key in _keys)
            {
                Console.WriteLine($" - {_key}");
            }
            Console.WriteLine("\nFinsihed printing\n");
        }

        // I made a EnterKeyFunction becuase Enter key has multiple uses cases
        private static void EnterKeyFunction()
        {
            if (AppState.GetWindow().PathTextBox.IsFocused)
            {
                FileSystemManager.PathBoxTryingToSetNewPath(AppState.GetWindow().PathTextBox.Text);
            }
            else if (AppState.GetWindow().SearchTextBox.IsFocused)
            {
                if (!string.IsNullOrWhiteSpace(AppState.GetWindow().SearchTextBox.Text))
                {
                    AppState.CurrentState = AppState.States.SEARCHING;
                    _ = SearchSystem.StartSearchSetup(AppState.GetWindow().SearchTextBox.Text);
                }
            }
        }

        // This removes key from _KeyDown
        public static void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (AppState.GetWindow().MainEntryList.SelectedItems != null)
                {
                    AppState.GetWindow().MainEntryList.SelectedItems?.Clear();
                }
            }

            _KeyDown.Remove(e.Key);

            if (IsPressed.Contains(e.Key))
            {
                IsPressed.Remove(e.Key);
            }
        }

        private static void CheckIfCanOpenFile()
        {
            if (AppState.GetWindow().MainEntryList.SelectedItem == null) return;

            //KeyAction _keyAction = new KeyAction(_KeyDown.ToArray());


        }

        public static bool IsKeyDown(Key key) => _KeyDown.Contains(key); // check if a key is Down from anywhere
    }
}
