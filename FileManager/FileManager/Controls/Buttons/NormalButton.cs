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

            PointerEntered += (_, __) => Background = ThemeData.HoverBrush;
            PointerExited += (_, __) => Background = Brushes.Transparent;
        }
    }
}
