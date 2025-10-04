using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Windows;
using System.IO;
using Avalonia.Platform;

namespace FileManager;

public partial class EntryCreationWindow : Window
{
    private bool IsCreatingDir;

    public EntryCreationWindow()
    {
        InitializeComponent();
    }

    private void CancelButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Console.WriteLine("CancelButtonClicked");
        this.Close();
    }

    private void CreateButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        string? entryName = EntryNameTextBox.Text;
        Console.WriteLine($"Entry Name: {entryName}");

        //Console.WriteLine($"Checking if its null or empty space");
        if (string.IsNullOrWhiteSpace(entryName))
        {
            ShowError("Cannot be empty");
        } else
        {
            //Console.WriteLine($"Passed null|empty test");
            //Console.WriteLine($"Testing if path already exists");
            if (FileManagerHelper.DoesEntryExists(entryName))
            {
                ShowError("That entry already exists in current scope");
                return;
            }
            //Console.WriteLine($"Passed path test");
            //Console.WriteLine($"Checking what to create");
            if (IsCreatingDir)
            {
                //Console.WriteLine($"Requsting to create Dir");
                FileManager.CreateDir(entryName);
            } else
            {
                //Console.WriteLine($"Requsting to create File");
                FileManager.CreateFile(entryName);
            }

            this.Close();
        }
    }

    public void ShowWindow(bool is_creating_dir)
    {
        IsCreatingDir = is_creating_dir;

        if (IsCreatingDir)
        {
            EntryNameTextBox.Watermark = "Folder name";
        } else
        {
            EntryNameTextBox.Watermark = "File name";
        }

        this.ShowDialog(AppState.GetWindow());
    }

    private void ShowError(string errorText)
    {
        EntryNameTextBox.Text = "";
        EntryNameTextBox.Watermark = errorText;
    }
}