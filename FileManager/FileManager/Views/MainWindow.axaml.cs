using Avalonia.Controls;
using FileManager.ViewModels;
using System;

namespace FileManager.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GlobalVariables.SetWindow(this);
            FileManager.StartUpSetup();
        }

        public void UpdatePathTextBox(string path)
        {
            PathTextBlock.Text = path;
        }
    }
}