using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;

namespace FileManager.Utils
{
    public static class UtilFunc
    {
        public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }
    }
}