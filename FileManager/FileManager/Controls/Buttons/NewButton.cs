using System;
using Avalonia;
using Avalonia.Controls;
using FileManager.ThemeManager;

namespace FileManager.Controls.Buttons
{
    public class NewButton : DropDownButton
    {
        public NewButton() {
            Background = ThemeData.AccentColor;

            PointerEntered += (_, __) => Background = ThemeData.DarkAccentColor;
            PointerExited += (_, __) => Background = ThemeData.AccentColor;

        }
    }
}