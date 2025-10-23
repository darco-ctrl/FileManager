using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using FileManager.Core;
using FileManager.Data;
using FileManager.Managers;
using FileManager.Theme;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Controls.Buttons
{
    public class AccentButton : ToggleButton
    {

        public static readonly StyledProperty<byte?> PathProperty = AvaloniaProperty.Register<AccentButton, byte?>(nameof(PathIndex));

        public byte? PathIndex
        {
            get => GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }

        public AccentButton()
        {

            Styles.Add(new Style(x => x.OfType<AccentButton>())
            {
                Setters =
                {
                    new Setter
                    {
                        Property = TransitionsProperty,
                        Value = new Transitions
                        {
                            new BrushTransition
                            {
                                Property = BackgroundProperty,
                                Duration = TimeSpan.FromMilliseconds(200)
                            },
                            new DoubleTransition
                            {
                                Property = OpacityProperty,
                                Duration = TimeSpan.FromMilliseconds(200)
                            }
                        }
                    }
                }
            });

            Background = ThemeManager.Current.Transparent;
            Opacity = 0.5;


            PointerEntered += (_, __) => _PointerEveterEvent();
            PointerExited += (_, __) => _PointerExitedEvent();
            IsCheckedChanged += (_, __) => OnCheckedChanges();
        }

        private void OnCheckedChanges()
        {
            Console.WriteLine("Check changed");
            if (IsChecked == true)
            {
                Console.WriteLine("Check was true");
                Opacity = 1;
                Background = ThemeManager.Current.AccentColor;

                if (ControlsManager.CurrentQuickAccessSeleection == this) return;
                if (ControlsManager.CurrentQuickAccessSeleection != null) ControlsManager.CurrentQuickAccessSeleection.IsChecked = false;

                ControlsManager.CurrentQuickAccessSeleection = this;

                if (PathIndex == null) return;
                AppState.GetWindowViewModel().SetCurrentDir(DataManager.Current.GetSpacialFolder(PathIndex.Value));

            }
            else
            {
                Console.WriteLine("Check was false");
                Opacity = 0.5;
                Background = ThemeManager.Current.Transparent;

            }
        }

        private void _PointerEveterEvent()
        {
            if (IsChecked == true) { return; }

            Background = ThemeManager.Current.HoverBrush;
            Opacity = 1;
        }

        private void _PointerExitedEvent()
        {
            if (IsChecked == true) { return; }

            Background = ThemeManager.Current.Transparent;
            Opacity = 0.5;
        }
    }
}
