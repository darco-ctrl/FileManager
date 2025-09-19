using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FIleManagerConsole
{
    public static class Program
    {
        private static Dictionary<string, Action> Commands = new Dictionary<string, Action>
        {
            {"..", FileSystem.GoBack},
            {"mkdir", FileSystem.CreatFolder },
            {"mkfile", FileSystem.CreateFile },

        };
        public static void Main()
        {
            Console.WriteLine("-- FILE MANAGER --");

            FileSystem.UpdateLog();

            while (true) {

                string? input = Console.ReadLine();

                if (input == null) { Console.WriteLine("Input is empty"); continue;  }

                
                if (int.TryParse(input, out int n))
                {
                    FileSystem.GoToDir(n);

                } else if (Commands.ContainsKey(input))
                {
                    Commands[input]();

                } else
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                FileSystem.UpdateLog();
            }
        }
    }
}