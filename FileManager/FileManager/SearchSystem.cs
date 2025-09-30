using Avalonia.Threading;
using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileManager
{
    public static class SearchSystem
    {

        public static void RequestForSearching()
        {
            AppState.GetWindow().PathTextBox.Focus();
            AppState.CurrentState = AppState.States.SEARCHING;
            AppState.GetWindowViewModel().CurrentLoadedEntires.Clear();
        }

        public static async Task StartSearchSetup(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
            else
            {

                await Task.Run(() => StartSearching(fileName));

                AppState.CurrentState = AppState.States.NONE;
            }
        }

        private static void StartSearching(string fileName)
        {
            StringBuilder fd_arg = new StringBuilder($"{fileName} {AppState.GetWindowViewModel().CurrentWorkingDir}");

            Console.WriteLine($"Fd args: {fd_arg}");

            

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = @"C:\tools\fd.exe",
                    Arguments = fd_arg.ToString(),
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };


            process.OutputDataReceived += (sender, e) =>
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (!string.IsNullOrWhiteSpace(e.Data))
                    {
                        EntryItemViewModel entryItem = new EntryItemViewModel
                        {
                            HoldingPath = e.Data,
                        };

                        if (File.Exists(e.Data))
                        {
                            entryItem.Name = Path.GetFileName(e.Data);
                            
                            
                        } else if (Directory.Exists(e.Data))
                        {
                            string? dir = e.Data.Remove(e.Data.Count() - 1);
                            if (!string.IsNullOrWhiteSpace(dir))
                            {
                                entryItem.Name = Path.GetFileName(dir);
                            }
                        }

                        Console.WriteLine($"Found: {e.Data}");
                        AppState.GetWindowViewModel().CurrentLoadedEntires.Add(entryItem);

                    }
                });
            };

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();
        }
    }
}
