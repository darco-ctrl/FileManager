    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using Tmds.DBus.Protocol;

namespace FileManager.Utils
{
    public static class UtilFunc
    {
        public static void AddRange<T>(this ObservableCollection<T> list, IEnumerable<T> items, bool reverse = false)
        {
            T[] arr = items.ToArray();

            //Console.WriteLine("\n-------------------------\n");
            //Console.WriteLine("AddRange Called\n");

            if (reverse)
            {
                //Console.WriteLine("Mode: reverse");
               // Console.WriteLine($"arrLenght: {arr.Length}\n");
                for (int i = arr.Length-1; i > 0; i--)
                {
                    //Console.WriteLine($"{i}: {arr[i]}");
                    list.Add(arr[i]);
                }

                //Console.WriteLine("\n----------- finished process ---------\n");

                return;
            }

            foreach (var item in arr)
            {
                list.Add(item);
            }
        }

        public static void AddRange<TKey, TValue>(
            this Dictionary<TKey, TValue> _dict,
            IEnumerable<TKey> _keys,
            IEnumerable<TValue> _values) where TKey : notnull
        {
            if (_dict == null)
            {
                throw new ArgumentNullException(nameof(_dict));
            }

            if (_keys == null)
            {
                throw new ArgumentNullException(nameof(_keys));
            }

            if (_values == null)
            {
                throw new ArgumentNullException(nameof(_values));
            }

            var keyList = _keys.ToList();
            var valueList = _values.ToList();

            if (keyList.Count != valueList.Count)
            {
                throw new ArgumentException($"Size of '{nameof(_keys)}' is not equal to size of '{nameof(_values)}'.");
            }

            for (int i = 0; i < keyList.Count; i++)
            {
                _dict.Add(keyList[i], valueList[i]);
            }
        }
    }
}
