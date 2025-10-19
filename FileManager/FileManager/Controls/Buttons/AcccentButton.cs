using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Styling;
using FileManager.Managers;
using FileManager.ThemeManager;
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

            Background = ThemeData.Transparent;
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
                Background = ThemeData.AccentColor;

            } else
            {
                Console.WriteLine("Check was false");
                Opacity = 0.5;
                Background = ThemeData.Transparent;

            }
        }

        private void _PointerEveterEvent()
        {
            if (IsChecked == true) { return; }

            Background = ThemeData.HoverBrush;
            Opacity = 1;
        }

        private void _PointerExitedEvent()
        {
            if (IsChecked == true) { return; }

            Background = ThemeData.Transparent;
            Opacity = 0.5;
        }
    }
}