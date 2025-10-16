using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.Theme;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Controls.Buttons
{
    public class EntryModifierButton : Button
    {
        public EntryModifierButton()
        {
            Console.WriteLine("Loading Entry Modifier");
            Background = Brushes.Transparent;

            PointerEntered += (_, __) => Background = ThemeData.HoverBrush;
            PointerExited += (_, __) => Background = Brushes.Transparent;

            AppState.GetWindow().Opened += (_, __) =>
            {
                var listBox = AppState.GetWindow().MainEntryList;
                if (listBox != null)
                {
                    AppState.GetWindow().MainEntryList.SelectionChanged += (_, __) => ManageIsEnabled();
                    ManageIsEnabled();
                }
            };


        }

        private void ManageIsEnabled()
        {
            //Console.WriteLine("Managing IsEnabled");
            if (AppState.GetWindow().MainEntryList.SelectedItem == null)
            {
                Opacity = 0.5;
                IsEnabled = false;
            }
            else
            {
                Opacity = 1;
                IsEnabled = true;
            }
        }
    }
}
