using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Windows;
using System.IO;

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

        if (string.IsNullOrWhiteSpace(entryName))
        {
            ShowError("Cannot be empty");
        } else
        {
            if (FileManagerHelper.DoesEntryExists(entryName))
            {
                ShowError("That entry already exists in current scope");
                return;
            }

            if (IsCreatingDir)
            {
                FileManager.CreateDir(entryName);
            } else
            {
                FileManager.CreateFile(entryName);
            }
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