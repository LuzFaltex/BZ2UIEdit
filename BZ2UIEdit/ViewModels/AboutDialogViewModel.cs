using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BZ2UIEdit.Commands;

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

        public ICommand OpenHyperlinkCommand
            = new RelayCommand<string>(
                uri => System.Diagnostics.Process.Start(uri),
                uri =>
                {
                    return Uri.TryCreate(uri, UriKind.Absolute, out Uri uriResult)
                                  && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                });
    }
}
