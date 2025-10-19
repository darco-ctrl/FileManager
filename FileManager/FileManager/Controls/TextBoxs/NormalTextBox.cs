using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using FileManager.Theme;
using System;


namespace FileManager.Controls.TextBoxs
{
    public class NormalTextBox : TextBox
    {
        public NormalTextBox()
        {
            Console.WriteLine($"Constructed name : {Watermark}");

            Background = Brushes.Gray;
            Foreground = Brushes.White;
            BorderBrush = ThemeManager.Current.Transparent;

        }

        private void Hovered()
        {
            Background = ThemeManager.Current.Transparent;
        }
    }
}