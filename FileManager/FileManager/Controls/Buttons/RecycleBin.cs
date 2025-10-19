using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Styling;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Animation;

namespace FileManager.Controls.Buttons
{
    public class RecycleBin : Button
    {
        public RecycleBin()
        {
            Styles.Add(new Style(x => x.OfType<NormalButton>())
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
                    },
                }
            });

            Background = ThemeData.DarkBackground;
            Opacity = 0.5;

            PointerEntered += (_, __) => _PointerEveterEvent();
            PointerExited += (_, __) => _PointerExitedEvent();
        }


        private void _PointerEveterEvent()
        {
            Background = ThemeData.HoverBrush;
            Opacity = 1;
        }

        private void _PointerExitedEvent()
        {
            Background = ThemeData.DarkBackground;
            Opacity = 0.5;
        }
    }
}