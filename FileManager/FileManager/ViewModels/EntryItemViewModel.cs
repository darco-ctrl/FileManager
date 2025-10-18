using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager.ThemeManager;

namespace FileManager.ViewModels
{
    public class EntryItemViewModel
    {
        public string Name { get; set; } = "";
        public string HoldingPath { get; set; } = "";
        public string IconPath { get; set; } = "{SvgImage /Assets/EntriesIcons/files.svg}";
        public string? Size { get; set; }
    }
}
