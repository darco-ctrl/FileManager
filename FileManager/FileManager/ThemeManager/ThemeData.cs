using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.ThemeManager
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

    public static class ThemeData
    {
        public static IBrush BackgroundBrush = new SolidColorBrush(Color.Parse("#141414"));
        public static IBrush PrimaryColor = new SolidColorBrush(Color.Parse("#1a1a1a"));
        public static IBrush SecondayColor = new SolidColorBrush(Color.Parse("#1e1e1e"));
        public static IBrush SelectionColor = new SolidColorBrush(Color.Parse("#155dfc"));
        public static IBrush MainBorderBrush = new SolidColorBrush(Color.Parse("#2a2a2a"));
        public static IBrush TextBrush_1 = new SolidColorBrush(Color.Parse("#ffffff"));
        public static IBrush TextBrush_2 = new SolidColorBrush(Color.Parse("#959caa"));
        public static IBrush TextBrush_3 = new SolidColorBrush(Color.Parse("#353e47"));
        public static IBrush HoverBrush = new SolidColorBrush(Color.Parse("#292929"));
    }
}
