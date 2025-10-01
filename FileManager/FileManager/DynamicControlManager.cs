using Avalonia.Controls;
using Avalonia.Media;
using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    public static class DynamicControlManager
    {
        private static List<Control> Menus = new List<Control>();


        public static EntryItemViewModel CreateEntryItem(string entry)
        {
            EntryItemViewModel? entryItem = new EntryItemViewModel
            {
                Name = Path.GetFileName(entry),
                HoldingPath = entry
            };

            return entryItem;
        }

        public static void MakeRightClickMenu(Object sender)
        {
            Button? EntryButton = sender as Button;
            if (EntryButton == null) { return; }

            ContextMenu rightClickContextMenu = new ContextMenu();

            MenuItem test1 = new MenuItem { Header = "Test 1" };
            test1.Click += (s, e) => Console.WriteLine("Test1 BeenClicked");

            MenuItem test2 = new MenuItem { Header = "Test 2" };
            test2.Click += (s, e) => Console.WriteLine("Test2 Been clicked");

            rightClickContextMenu.Items.Add(test1);
            rightClickContextMenu.Items.Add(test2);

            EntryButton.ContextMenu = rightClickContextMenu;

            Menus.Add(EntryButton);
        }
    }
}
