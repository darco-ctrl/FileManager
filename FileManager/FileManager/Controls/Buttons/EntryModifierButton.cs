using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.Theme;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Styling;
using Avalonia.Animation;

namespace FileManager.Controls.Buttons
{
    public class EntryModifierButton : Button
    {
        public EntryModifierButton()
        {

            Styles.Add(new Style(x => x.OfType<EntryModifierButton>())
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
            CornerRadius = new Avalonia.CornerRadius(6);
            Opacity = 0.5;

            PointerEntered += (_, __) => Background = ThemeManager.Current.HoverBrush;
            PointerExited += (_, __) => Background = ThemeManager.Current.Transparent;

            AppState.GetWindow().Opened += (_, __) =>
            {
                var listBox = AppState.GetWindow().MainEntryList;
                if (listBox != null)
                {
                    AppState.GetWindow().MainEntryList.SelectionChanged += (_, __) => ManageIsEnabled();
                    ManageIsEnabled();
                }
            };
        }

        private void ManageIsEnabled()
        {
            if (AppState.GetWindow().MainEntryList.SelectedItem == null)
            {
                Opacity = 0.2;
                IsEnabled = false;
            }
            else
            {
                Opacity = 1;
                IsEnabled = true;
            }
        }
    }
}
