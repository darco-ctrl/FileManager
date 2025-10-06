using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using FileManager.Managers;
using FileManager.Utils;
using FileManager.ViewModels;
using FileManager.Views;
using System;
using System.Linq;
using System.Runtime.InteropServices;



namespace FileManager
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();



        public override void OnFrameworkInitializationCompleted()
        {
#if DEBUG
            AllocConsole(); // opens a console window for the whole app
#endif
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Console.WriteLine("STARTING APP");

                AppState.SetMainWindowViewModel(new MainWindowViewModel());
                MainWindow? mainWindow = new MainWindow
                {
                    DataContext = AppState.GetWindowViewModel()
                };
                AppState.SetWindow(mainWindow);
                FileSystemManager.StartUpSetup();
                
                mainWindow = null;

                // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
                // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = AppState.GetWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}