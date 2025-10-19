using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using FileManager.ThemeManager;
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
            BorderBrush = ThemeData.Transparent;

        }

        private void Hovered()
        {
            Background = ThemeData.Transparent;
        }
    }
}