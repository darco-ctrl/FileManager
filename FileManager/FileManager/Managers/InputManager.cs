using System;
using System.Collections.Generic;
using Avalonia.Input;
using FileManager.Core;
using FileManager.Utils;
using FileManager.ViewModels;
using Avalonia.VisualTree;
using Avalonia;
using Avalonia.Controls;

namespace FileManager.Managers
{
    public class InputManager
    {
        // KeyDown stores all current key pressed the key is removed from KeyDown when OnKeyUp function is triggered
        private readonly HashSet<Key> _KeyDown = new();
        private Dictionary<Key, Action> KeyActionSet = new(); // All keys and functions that tell what it should do
        private readonly HashSet<Key> IsPressed = new(); // this is to prevent held action

        // I made key's HashSet and Dictionary becuase its faster than checking each key with if statement
        // with this i can just check if KeyActionSet and get the value all in O(1)

        // INPUT MANAGE CONSTRUCTOR
        public InputManager()
        {
            KeyActionSet.Add(Key.Back, FileSystemManager.GoBackOne); // Adding Backspace key so user can go back from a dir
            KeyActionSet.Add(Key.Enter, EnterKeyFunction); // Adding Enter key
            KeyActionSet.Add(Key.H, HKeyFunction);

        }

        // When any KeyisDown its added to KeyDown and check if the Key Pressed is in 'KeyActionSet' if so
        // call the action connected to that
        public void OnKeyDown(KeyEventArgs e)
        {
            _KeyDown.Add(e.Key);

            if (KeyActionSet.ContainsKey(e.Key) && !IsPressed.Contains(e.Key))
            {
                KeyActionSet[e.Key]();
                IsPressed.Add(e.Key);
            }
        }

    public static T? FindChild<T>(Visual parent) where T : class
    {
        foreach (var child in parent.GetVisualChildren())
        {
            if (child is T t)
                return t;

            if (child is Visual visualChild)
            {
                var result = FindChild<T>(visualChild);
                if (result != null)
                    return result;
            }
        }
        return null;
    }

    public static IEnumerable<T> FindChildren<T>(Visual parent) where T : class
    {
        foreach (var child in parent.GetVisualChildren())
        {
            if (child is T t)
                yield return t;

            if (child is Visual visualChild)
            {
                foreach (var grandChild in FindChildren<T>(visualChild))
                    yield return grandChild;
            }
        }
    }

        private void HKeyFunction()
        {
            Console.WriteLine("pritng image status");
            EntryItemViewModel? selectedItem_entry = AppState.GetWindow().MainEntryList.SelectedItem as EntryItemViewModel;
            ListBoxItem? selectedItem = AppState.GetWindow().MainEntryList.SelectedItem as ListBoxItem;
            if (selectedItem != null)
            {

                Image? _image = FindChild<Image>(selectedItem);
                if (_image == null) return;

                Console.WriteLine($"Source : {_image.Source}");
                if (selectedItem_entry == null) return;
                Console.WriteLine($"Icon path : {selectedItem_entry.IconPath}");

            }
        }

        // I made a EnterKeyFunction becuase Enter key has multiple uses cases
        private void EnterKeyFunction()
        {
            if (AppState.GetWindow().PathTextBox.IsFocused)
            {
                FileSystemManager.PathBoxTryingToSetNewPath(AppState.GetWindow().PathTextBox.Text);
            } else if (AppState.GetWindow().SearchTextBox.IsFocused)
            {
                if (!string.IsNullOrWhiteSpace(AppState.GetWindow().SearchTextBox.Text))
                {
                    AppState.CurrentState = AppState.States.SEARCHING;
                    _ = SearchSystem.StartSearchSetup(AppState.GetWindow().SearchTextBox.Text);
                }
            }
        }

        // This removes key from _KeyDown
        public void OnKeyUp(KeyEventArgs e)
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

        public bool IsKeyDown(Key key) => _KeyDown.Contains(key); // check if a key is Down from anywhere
    }
}
