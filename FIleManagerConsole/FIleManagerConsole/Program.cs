using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FIleManagerConsole
{
    internal static class Program
    {
        /*
         * Dictionary to store commands function so
         * i can access it according to user's command
         * in instant
         *
         * and this is easier than switch case or if
         * you dont have to add if or case each time you
         * just come to this dictionary and add 
         * key "string" and value as a defined "function"
         */
        private static Dictionary<string, Action> Commands = new Dictionary<string, Action>
        {
            {"..", FileSystem.GoBack},
            {"mkdir", FileSystem.CreatFolder },
            {"mkfile", FileSystem.CreateFile },
            {"delE", FileSystem.DeleteEntry},
            {"s", FileSystem.SearchM }

        };

        public static bool CanRun = true;
        public static void Main()
        {
            Console.WriteLine("-- FILE MANAGER --");

            FileSystem.UpdateLog();

            while (true) {
                if (!CanRun) { continue; }

                string? input = Console.ReadLine();

                if (input == null) { Console.WriteLine("Input is empty"); continue;  }

                // this is the place that decide what command has
                // user sent and do stuff acording to that
                if (int.TryParse(input, out int n))
                {
                    FileSystem.GoToDir(n);

                } else if (Commands.ContainsKey(input))
                {
                    // see nothing other than this just 1 line
                    // it checks if key exists in dictionary 
                    // and straight call it without adding new line here
                    
                    Commands[input]();

                } else
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                //Console.ReadLine();
                FileSystem.UpdateLog();
            }
        }
    }
}