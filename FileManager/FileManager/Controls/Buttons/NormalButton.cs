using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Controls.Buttons
{
    public class NormalButton : Button
    {
        public NormalButton()
        {


            Background = Brushes.Transparent;
            Opacity = 0.35;

            PointerEntered += (_, __) => _PointerEveterEvent();
            PointerExited += (_, __) => _PointerExitedEvent();
        }

        private void _PointerEveterEvent()
        {
            Background = ThemeData.HoverBrush;
            Opacity = 1;
        }

        private void _PointerExitedEvent()
        {
            Background = Brushes.Transparent;
            Opacity = 0.35;
        }
    }
}
