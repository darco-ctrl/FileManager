using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIleManagerConsole
{
    internal static class Search
    {
        public static string SearchFor(string argument)
        {
            var sw = Stopwatch.StartNew();

            Program.CanRun = false;

            ProcessStartInfo psi = new ProcessStartInfo 
            {
                FileName = @"C:\tools\fd.exe",
                Arguments = argument,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
                
            };

            using (Process? process = Process.Start(psi))
            {
                if (process == null) { return "Couldnt start process"; }

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                {
                    return $"Error: {error}"; 
                }

                sw.Stop();
                Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalSeconds} seconds");
                Console.ReadLine();

                Program.CanRun = true;
                return output;
            }
        }
    }
}
