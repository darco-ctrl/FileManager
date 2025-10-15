using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Controls.Buttons
{
    public class PasteButton : Button
    {
        public PasteButton()
        {


            Background = Brushes.Transparent;

            PointerEntered += (_, __) => Background = ThemeData.HoverBrush;
            PointerExited += (_, __) => Background = Brushes.Transparent;

            DynamicControlManager.OnClipBoardItemChanged += () =>
            {
                CheckForClipBoard();
            };
        }

        private void CheckForClipBoard()
        {
            if (string.IsNullOrWhiteSpace(DynamicControlManager.ClipBoardItem))
            {
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
            }
        }
    }
}
