using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerAvalonia.ViewModels
{
    public class FileItemViewModel
    {
        /*
         * this is the data a file/folder olds
         */
        public string Name { get; set; }
        public string? IconPath { get; set; }
        public string? Size { get; set; }
    }
}
