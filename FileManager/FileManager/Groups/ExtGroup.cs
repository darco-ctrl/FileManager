using System;
using System.Collections.Generic;
using FileManager.Data;

namespace FileManager.Groups
{    
    public class ExtGroup
    {
        public HashSet<string> Extensions = [];

        public ExtGroup(HashSet<string> _extensions)
        {
            Extensions = _extensions;   
        }
    }
}
