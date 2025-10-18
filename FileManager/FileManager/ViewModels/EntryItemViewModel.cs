using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.IO;
using FileManager.ThemeManager;

namespace FileManager.ViewModels
{
    public class EntryItemViewModel
    {
        public string Name { get; set; } = "";
        public string HoldingPath { get; set; } = "";
        public Bitmap? Icon { get; set; }
        public string? Size { get; set; }

        public void LoadIcon()
        {
            var uri = new Uri(IconsManager.GetIconPath(HoldingPath));
            using var stream = AssetLoader.Open(uri);
            Icon = new Bitmap(stream);
        }

    }
}
