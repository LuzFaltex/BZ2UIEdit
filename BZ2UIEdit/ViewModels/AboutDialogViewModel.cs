using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BZ2UIEdit.Commands;
using BZ2UIEdit.UserControls;
using FontAwesome.WPF;

namespace BZ2UIEdit.ViewModels
{
    public class AboutDialogViewModel : ViewModelBase
    {
        public string Title { get; }
            = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyTitleAttribute>().Title;

        public string Description { get; }
            = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyDescriptionAttribute>().Description;

        public string Product { get; }
            = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyProductAttribute>().Product;

        // This property is not added to the assembly, but is treated in a special way.
        public string Version { get; }
            = Assembly.GetExecutingAssembly()
                .GetName().Version.ToString(3);

        public string Copyright { get; }
            = Assembly.GetExecutingAssembly()
                .GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;

        public ObservableCollection<ThirdPartyCopyrightListing> ThirdPartyCopyrightListing { get; }
            = new ObservableCollection<ThirdPartyCopyrightListing>()
            {
                new ThirdPartyCopyrightListing(FontAwesomeIcon.FileCodeOutline, "Windows Presentation Foundation - Microsoft", "https://github.com/dotnet/wpf/blob/master/LICENSE.TXT"),
                new ThirdPartyCopyrightListing("https://raw.githubusercontent.com/MahApps/MahApps.Metro/develop/mahapps.metro.logo2.png", "MahApps.Metro (Theme) - MahApps", "https://github.com/MahApps/MahApps.Metro/blob/develop/LICENSE"),
                new ThirdPartyCopyrightListing(FontAwesomeIcon.FontAwesome, "Font Awesome - Fonticons, Inc.", "https://github.com/charri/Font-Awesome-WPF/blob/master/LICENSE"),
                new ThirdPartyCopyrightListing(FontAwesomeIcon.Terminal, "Json.NET - Newtonsoft", "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md")
            };

        public ICommand OpenHyperlinkCommand { get; }
            = new RelayCommand<string>(
                x => System.Diagnostics.Process.Start(x),
                x => Uri.TryCreate(x, UriKind.Absolute, out Uri uriResult)
                                  && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));

        public ICommand OpenMessageBoxCommand { get; }
            = new RelayCommand<string>(
                x => MessageBox.Show(x));
    }
}
