using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace FIleManagerConsole
{
    public class  Program
    {
        public static void Main()
        {
            

            while (true) {

                Console.WriteLine("""
                    What would you like to do?
                    1. print all file in current dir
                    """);

                string? input = Console.ReadLine();

                if (input == null) { continue; }

                try
                {
                    int input_num = int.Parse(input);

                    switch (input_num)
                    {
                        case 1:
                            Console.WriteLine("Printing Dirs\n");
                            FileSystem.PrintListOfFiles();
                            break;
                    }

                } 
                catch (FormatException ex)
                {
                    Console.WriteLine($"Given input is not an integer,\n please enter input from above,\n exited with: {ex}");
                    continue;

                } catch (Exception ex)
                {
                    Console.WriteLine($"Exited with exeption: {ex}");
                    continue;

                }
            }
        }
    }
}