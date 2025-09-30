using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Input;

namespace FileManager
{
    public class InputManager
    {

        private readonly HashSet<Key> _KeyDown = new();
        private Dictionary<Key, Action> KeyActionSet = new(); // All keys and functions that tell what it should do
        private readonly HashSet<Key> IsPressed = new(); // this is to prevent held action



        public InputManager()
        {
            KeyActionSet.Add(Key.Back, FileManager.GoBackOne);
            KeyActionSet.Add(Key.Enter, EnterKeyFunction);
        }

        public void OnKeyDown(KeyEventArgs e)
        {
            _KeyDown.Add(e.Key);

            if (KeyActionSet.ContainsKey(e.Key) && !IsPressed.Contains(e.Key))
            {
                KeyActionSet[e.Key]();
                IsPressed.Add(e.Key);
            }
        }

        private void EnterKeyFunction()
        {
            if (AppState.GetWindow().PathTextBox.IsFocused)
            {
               if (AppState.CurrentState == AppState.States.SEARCHING)
               {
                    SearchSystem.StartSearchSetup(AppState.GetWindow().PathTextBox.Text);
               } else
               {
                    FileManager.PathBoxTryingToSetNewPath(AppState.GetWindow().PathTextBox.Text);
               }
            }
        }

        public void OnKeyUp(KeyEventArgs e)
        {
            _KeyDown.Remove(e.Key);

            if (IsPressed.Contains(e.Key))
            {
                IsPressed.Remove(e.Key);
            }
        }

        public bool IsKeyDown(Key key) => _KeyDown.Contains(key);
        public IEnumerable<Key> KeysDown => _KeyDown;
    }
}
