using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FontAwesome.WPF;

namespace BZ2UIEdit.UserControls
{
    /// <summary>
    /// Interaction logic for ThirdPartyCopyrightListing.xaml
    /// </summary>
    public partial class ThirdPartyCopyrightListing : UserControl
    {
        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(string), typeof(ThirdPartyCopyrightListing), new PropertyMetadata(string.Empty));

        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(ThirdPartyCopyrightListing), new PropertyMetadata(FontAwesomeIcon.None));

        public string LicenseUrl
        {
            get { return (string)GetValue(LicenseUrlProperty); }
            set { SetValue(LicenseUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LicenseUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LicenseUrlProperty =
            DependencyProperty.Register("LicenseUrl", typeof(string), typeof(ThirdPartyCopyrightListing), new PropertyMetadata(string.Empty));



        public Visibility ImageVisibility
            => Image is null ? Visibility.Collapsed : Visibility.Visible;

        public Visibility IconVisibility
            => Icon == FontAwesomeIcon.None ? Visibility.Collapsed : Visibility.Visible;


        public ThirdPartyCopyrightListing(string imageSource, string content, string licenseUrl)
        {
            Image = imageSource;
            Content = content;
            LicenseUrl = licenseUrl;
            InitializeComponent();
        }

        public ThirdPartyCopyrightListing(FontAwesomeIcon icon, string content, string licenseUrl)
        {
            Icon = icon;
            Content = content;
            LicenseUrl = licenseUrl;
            InitializeComponent();
        }
    }
}
