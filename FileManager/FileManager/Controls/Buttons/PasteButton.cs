using Avalonia.Controls;
using Avalonia.Media;
using FileManager.Managers;
using FileManager.ThemeManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Styling;
using Avalonia.Animation;

namespace FileManager.Controls.Buttons
{
    public class PasteButton : Button
    {
        public PasteButton()
        {

            Styles.Add(new Style(x => x.OfType<PasteButton>())
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

            PointerEntered += (_, __) => Background = ThemeData.HoverBrush;
            PointerExited += (_, __) => Background = ThemeData.Transparent;

            ControlsManager.OnClipBoardItemChanged += () =>
            {
                CheckForClipBoard();
            };
            CheckForClipBoard();
        }

        private void CheckForClipBoard()
        {
            if (string.IsNullOrWhiteSpace(ControlsManager.ClipBoardItem))
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
