using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Theme
{
    /*
     *      Background color : #141414
        Primary color : #1a1a1a
        Secondary Color : #1e1e1e
        Accent color : #155dfc # Blue
        PS border color : #2a2a2a

        1 TextColor : White
        2 TextColor : #959caa
        3 TextColor : #353E47

        292929  hover color
     */

    public class ThemeData : INotifyPropertyChanged
    {

        private IBrush _transparent = new SolidColorBrush(Color.FromArgb(0, 41, 41, 41));
        public IBrush Transparent
        {
            get => _transparent;
            set
            {
                if (_transparent != value)
                {
                    _transparent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Transparent)));
                }
            }
        }

        //  used for main background
        private IBrush _backgroundBrush = new SolidColorBrush(Color.Parse("#141414"));
        public IBrush BackgroundBrush
        {
            get => _backgroundBrush;
            set
            {
                if (_backgroundBrush != value)
                {
                    _backgroundBrush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundBrush)));
                }
            }
        }

        // light background used for titlebar side bar and more
        private IBrush _primaryColor = new SolidColorBrush(Color.Parse("#1a1a1a"));
        public IBrush PrimaryColor
        {
            get => _primaryColor;
            set
            {
                if (_primaryColor != value)
                {
                    _primaryColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondaryColor)));
                }
            }
        }

        private IBrush _secondaryColor = new SolidColorBrush(Color.Parse("#1e1e1e"));
        public IBrush SecondaryColor
        {
            get => _secondaryColor;
            set
            {
                if (_secondaryColor != value)
                {
                    _secondaryColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondaryColor)));
                }
            }
        }

        private IBrush _accentColor = new SolidColorBrush(Color.Parse("#155dfc")); //
        public IBrush AccentColor
        {
            get => _accentColor;
            set
            {
                if (_accentColor != value)
                {
                    _accentColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccentColor)));
                }
            }
        }

        private IBrush _darkAccentColor = new SolidColorBrush(Color.Parse("#0041cd")); //
        public IBrush DarkAccentColor
        {
            get => _darkAccentColor;
            set
            {
                if (_darkAccentColor != value)
                {
                    _darkAccentColor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DarkAccentColor)));
                }
            }
        }

        //Primary outline
        private IBrush _primaryBorderBrush = new SolidColorBrush(Color.Parse("#2a2a2a"));
        public IBrush PrimaryBorderBrush
        {
            get => _primaryBorderBrush;
            set
            {
                if (_primaryBorderBrush != value)
                {
                    _primaryBorderBrush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrimaryBorderBrush)));
                }
            }
        }

        private IBrush _accentWhite = new SolidColorBrush(Color.Parse("#e7e6e6"));
        public IBrush AccentWhite
        {
            get => _accentWhite;
            set
            {
                if (_accentWhite != value)
                {
                    _accentWhite = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AccentWhite)));
                }
            }
        }

        private IBrush _textBrush_1 = new SolidColorBrush(Color.Parse("#d1d5db"));
        public IBrush TextBrush_1
        {
            get => _textBrush_1;
            set
            {
                if (_textBrush_1 != value)
                {
                    _textBrush_1 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBrush_1)));
                }
            }
        }

        private IBrush _textBrush_2 = new SolidColorBrush(Color.Parse("#959caa"));
        public IBrush TextBrush_2
        {
            get => _textBrush_2;
            set
            {
                if (_textBrush_2 != value)
                {
                    _textBrush_2 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBrush_2)));
                }
            }
        }

        private IBrush _textBrush_3 = new SolidColorBrush(Color.Parse("#353e47"));
        public IBrush TextBrush_3
        {
            get => _textBrush_3;
            set
            {
                if (_textBrush_3 != value)
                {
                    _textBrush_3 = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TextBrush_3)));
                }
            }
        }

        private IBrush _hoverBrush = new SolidColorBrush(Color.Parse("#292929"));
        public IBrush HoverBrush
        {
            get => _hoverBrush;
            set
            {
                if (_hoverBrush != value)
                {
                    _hoverBrush = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HoverBrush)));
                }
            }
        }

        private CornerRadius _primaryCornerRadius = new CornerRadius(10);
        public CornerRadius PrimaryCornerRadius
        {
            get => _primaryCornerRadius;
            set
            {
                if (_primaryCornerRadius != value)
                {
                    _primaryCornerRadius = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrimaryCornerRadius)));
                }
            }
        }

        private CornerRadius _secondaryCornerRadius = new CornerRadius(6);
        public CornerRadius SecondaryCornerRadius
        {
            get => _secondaryCornerRadius;
            set
            {
                if (_secondaryCornerRadius != value)
                {
                    _secondaryCornerRadius = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SecondaryCornerRadius)));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
