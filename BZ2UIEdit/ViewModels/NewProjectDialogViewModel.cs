using BZ2UIEdit.Commands;
using BZ2UIEdit.Common;
using BZ2UIEdit.Services.DataValidationService;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BZ2UIEdit.ViewModels
{
    public class NewProjectDialogViewModel : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, ICollection<string>> _validationErrors
            = new Dictionary<string, ICollection<string>>();

        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { SetProperty(ref _projectName, value); }
        }

        private string _projectLocation;
        public string ProjectLocation
        {
            get { return _projectLocation; }
            set
            {
                SetProperty(ref _projectLocation, value);
                OnPropertyChanged(nameof(ContinueButtonEnabled));
            }
        }

        private int _age;

        [Required(ErrorMessage = "You must specify an age")]
        [Range(minimum:10, maximum: 100, ErrorMessage = "Age must be between 10 and 100")]
        public int Age
        {
            get { return _age; }
            set
            {
                SetProperty(ref _age, value);
                ValidateModelProperty(value);
            }
        }

        private GameType _gameType = GameType.BZCC;
        public GameType GameType
        {
            get { return _gameType; }
            set { SetProperty(ref _gameType, value); }
        }

        private ProjectType _projectType = ProjectType.EmptyFallback;
        public ProjectType ProjectType
        {
            get { return _projectType; }
            set { SetProperty(ref _projectType, value); }
        }

        public void ValidateModelProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (_validationErrors.ContainsKey(propertyName))
                _validationErrors.Remove(propertyName);

            PropertyInfo propertyInfo = GetType().GetProperty(propertyName);
            IList<string> validationErrors =
                (from validationAttribute in propertyInfo.GetCustomAttributes(true).OfType<ValidationAttribute>()
                    where !validationAttribute.IsValid(value)
                    select validationAttribute.FormatErrorMessage(string.Empty))
                    .ToList();

            _validationErrors.Add(propertyName, validationErrors);
            RaiseErrorsChanged(propertyName);
            OnPropertyChanged(nameof(ContinueButtonEnabled));
        }

        public bool ContinueButtonEnabled
            => CanContinue(this);

        public static bool CanContinue(NewProjectDialogViewModel vm)
            => !vm.HasErrors & !string.IsNullOrWhiteSpace(vm.ProjectLocation);

        #region INotifyDataErrorInfo members

        public bool HasErrors => _validationErrors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName = "")
            => _validationErrors.TryGetValue(propertyName, out var value)
                ? value
                : Array.Empty<string>();

        public void RaiseErrorsChanged(string propertyName)
            => ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        #endregion


        public ICommand OpenFileSaveDialogCommand { get; }
            = new RelayCommand<NewProjectDialogViewModel>(SaveFile);

        public ICommand CreateCommand { get; }
            = new RelayCommand<NewProjectDialogViewModel>(CreateProject);

        public static void SaveFile(NewProjectDialogViewModel vm)
        {
            var sfd = new SaveFileDialog()
            {
                FileName = "Project",
                DefaultExt = ".bzi",
                Filter = "Battlezone UI Project (.bzi)|*.bzi",
                AddExtension = true,
                InitialDirectory = string.IsNullOrEmpty(vm.ProjectLocation) ? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) : vm.ProjectLocation,
                Title = "Project Location",
                OverwritePrompt = true,
            };

            sfd.FileOk += FileOk;

            bool? result = sfd.ShowDialog();

            if (result == true)
            {
                vm.ProjectName = System.IO.Path.GetFileNameWithoutExtension(sfd.FileName);
                vm.ProjectLocation = System.IO.Path.GetDirectoryName(sfd.FileName);
            }

            void FileOk(object sender, CancelEventArgs e)
            {
                var sv = sender as SaveFileDialog;
                if (System.IO.Path.GetExtension(sv.FileName).ToLower() != ".bzi")
                {
                    e.Cancel = true;
                    MessageBox.Show("Please omit the extension or use 'BZI'");
                    return;
                }
            }
        }

        public static void CreateProject(NewProjectDialogViewModel vm)
        {
            MessageBox.Show($"Project Name: {vm.ProjectName}{Environment.NewLine}Project Location: {vm.ProjectLocation}{Environment.NewLine}Game Type: {vm.GameType}{Environment.NewLine}Project Type: {vm.ProjectType}");
        }
    }
}
