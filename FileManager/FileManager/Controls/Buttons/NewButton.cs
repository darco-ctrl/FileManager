using System;
using Avalonia;
using Avalonia.Controls;
using FileManager.Theme;
using Avalonia.Styling;
using Avalonia.Animation;

namespace FileManager.Controls.Buttons
{
    public class NewButton : DropDownButton
    {
        public NewButton() {

            Styles.Add(new Style(x => x.OfType<NewButton>())
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
                            }
                        }
                    }
                }
            });

            Background = ThemeManager.Current.AccentColor;

            PointerEntered += (_, __) => Background = ThemeManager.Current.DarkAccentColor;
            PointerExited += (_, __) => Background = ThemeManager.Current.AccentColor;

        }
    }
}