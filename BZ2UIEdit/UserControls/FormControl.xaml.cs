using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace BZ2UIEdit.UserControls
{
    /// <summary>
    /// Interaction logic for FormControl.xaml
    /// </summary>
    public partial class FormControl : UserControl
    {
        public static readonly DependencyProperty HeaderDependencyProperty
            = DependencyProperty.Register("Header", typeof(string), typeof(FormControl));

        public string Header
        {
            get { return (string) GetValue(HeaderDependencyProperty); }
            set { SetValue(HeaderDependencyProperty, value); }
        }

        public static readonly DependencyProperty ToolTipDependencyProperty
            = DependencyProperty.Register("Hint", typeof(ToolTip), typeof(FormControl));

        public ToolTip Hint
        {
            get { return (ToolTip)GetValue(ToolTipDependencyProperty); }
            set { SetValue(ToolTipDependencyProperty, value); }
        }

        public FormControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
