using System;
using System.Collections.Generic;
using FileManager.Data;

namespace FileManager.Groups
{    
    public class ExtGroup
    {
        public HashSet<DataManager.FileTypes> Extensions = [];

        public ExtGroup(HashSet<DataManager.FileTypes> _extensions)
        {
            Extensions = _extensions;   
        }
    }
}
