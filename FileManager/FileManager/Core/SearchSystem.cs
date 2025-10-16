using Avalonia.Threading;
using FileManager.Managers;
using FileManager.Utils;
using FileManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Core
{
    public static class SearchSystem
    {
        /*
         * this makes PathTextBox eligible to recive input for for searching or what to search
         * and set AppState to Searching
         */
        public static void RequestForSearching()
        {
            AppState.GetWindow().PathTextBox.Focus();
            AppState.CurrentState = AppState.States.SEARCHING;
            AppState.GetWindowViewModel().CurrentLoadedEntires.Clear();
        }

        /*
         * After reciving the input it may be null if null do nothing if not null
         * run a task after task is done AppState is set to none
         * i used Task and async becuase i dont want my app to get stuck in one place
         */
        public static async Task StartSearchSetup(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
            else
            {

                await Task.Run(() => StartSearching(fileName));


            }
        }

        /*
         * This starts the search in different thread and get live output from FD
         */
        private static void StartSearching(string fileName)
        {
            StringBuilder fd_arg = new StringBuilder($"{fileName} {AppState.GetWindowViewModel().CurrentWorkingDir}");
            AppState.GetWindowViewModel().CurrentLoadedEntires.Clear();

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

            List<EntryItemViewModel> _tempEntries = [];
            int _index = 0;


            process.OutputDataReceived += (sender, e) =>
            {

                if (!string.IsNullOrWhiteSpace(e.Data))
                {
                    EntryItemViewModel entryItem = DynamicControlManager.CreateEntryItem(e.Data);



                    if (File.Exists(e.Data))
                    {
                        entryItem.Name = Path.GetFileName(e.Data);


                    }
                    else if (Directory.Exists(e.Data))
                    {
                        string? dir = e.Data.Remove(e.Data.Count() - 1);
                        if (!string.IsNullOrWhiteSpace(dir))
                        {
                            entryItem.Name = Path.GetFileName(dir);
                        }
                    }

                    _index += 1;
                    _tempEntries.Add(entryItem);

                    if (_index % 20 == 0)
                    {
                        Dispatcher.UIThread.InvokeAsync(() =>
                        {
                            AppState.GetWindowViewModel().CurrentLoadedEntires.AddRange(_tempEntries);
                            _tempEntries.Clear();
                        });
                    }

                    Console.WriteLine($"Found: {e.Data}");

                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            if (_tempEntries.Count > 0)
            {
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    AppState.GetWindowViewModel().CurrentLoadedEntires.AddRange(_tempEntries);

                });
            }
        }
    }
}
