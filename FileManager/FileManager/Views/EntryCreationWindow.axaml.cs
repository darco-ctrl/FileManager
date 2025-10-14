using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using FileManager.Core;
using FileManager.Managers;
using FileManager.Utils;
using System;
using System.IO;
using System.Windows;

namespace FileManager;

public partial class EntryCreationWindow : Window
{
    private Action<string, FileOperation.OperationState>? ReturnAction;
    private FileOperation.OperationState ReturnWith = FileOperation.OperationState.NONE;

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
        string? entryName = InputBox.Text;
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

            if (ReturnAction != null)
            {
                ReturnAction(entryName, ReturnWith);
            }

            this.Close();
        }
    }

    public void ShowWindow(Action<string, FileOperation.OperationState> action,
        string _title, string waterMark, FileOperation.OperationState return_with,
        string text="")
    {
        ReturnAction = action;
        ReturnWith = return_with;

        Title.Text = _title;
        InputBox.Watermark = waterMark;

        InputBox.Text = text;

        int i = text.LastIndexOf('.');
        if (i == -1)
        {
            i = text.Length;
            InputBox.CaretIndex = i;
            InputBox.SelectionStart = 0;
            InputBox.SelectionEnd = i;
        } else
        {
            InputBox.CaretIndex = i;
            InputBox.SelectionStart = 0;
            InputBox.SelectionEnd = i;
        }

        this.ShowDialog(AppState.GetWindow());
        this.Focus();
        InputBox.Focus();
    }

    private void ShowError(string errorText)
    {
        InputBox.Text = "";
        InputBox.Watermark = errorText;
    }
}