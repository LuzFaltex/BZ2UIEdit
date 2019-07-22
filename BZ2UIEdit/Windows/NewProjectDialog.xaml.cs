using BZ2UIEdit.Commands;
using BZ2UIEdit.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BZ2UIEdit.Windows
{
    /// <summary>
    /// Interaction logic for NewProjectDialog.xaml
    /// </summary>
    public partial class NewProjectDialog
    {
        private static NewProjectDialogViewModel viewModel;
        private static NewProjectDialog instance;

        public ICommand OpenFileSaveDialogCommand { get; }
            = new RelayCommand(_ => SaveFile(viewModel));

        public ICommand CreateCommand { get; }
            = new RelayCommand(_ => AcceptHandler());

        public NewProjectDialog(NewProjectDialogViewModel vm)
        {            
            DataContext = viewModel = vm;
            instance = this;
            InitializeComponent();
        }

        private static void SaveFile(NewProjectDialogViewModel vm)
        {
            var sfd = new SaveFileDialog()
            {
                FileName = SuggestFileName(vm),
                DefaultExt = ".bzi",
                Filter = "Battlezone UI Project (.bzi)|*.bzi",
                AddExtension = true,
                InitialDirectory = string.IsNullOrEmpty(vm.ProjectLocation) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : vm.ProjectLocation,
                Title = "Project Location",
                OverwritePrompt = true,
                ValidateNames = true
            };

            var result = sfd.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK || result == System.Windows.Forms.DialogResult.Yes)
            {
                // vm.ProjectName = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName);
                // vm.ProjectLocation = System.IO.Path.GetDirectoryName(sfd.FileName);
                vm.ProjectFile = new FileInfo(sfd.FileName);
            }
        }

        private static string SuggestFileName(NewProjectDialogViewModel vm)
        {
            if (string.IsNullOrWhiteSpace(vm.ProjectName))
                return "Project";

            // Filter file name
            return string.Join("_", vm.ProjectName.Split(System.IO.Path.GetInvalidFileNameChars()));
        }

        private static void AcceptHandler()
        {
            instance.DialogResult = true;
            instance.Close();
        }
    }
}
