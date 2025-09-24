using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ViewModels
{
    public class DriveItemViewModel
    {
        public string? VolumeLabel { get; set; }
        public string? IconPath { get; set; }
        public string? Size { get; set; }
        public string? Name { get; set; }
        public string? DeviceID { get; set; }
    }
}
