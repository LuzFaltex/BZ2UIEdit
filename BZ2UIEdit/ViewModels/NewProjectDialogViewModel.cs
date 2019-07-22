using BZ2UIEdit.Commands;
using BZ2UIEdit.Common;
using BZ2UIEdit.Services;
using BZ2UIEdit.Services.FileService;
using BZ2UIEdit.Services.NewProjectService;
using Microsoft.Win32;
using Serilog;
using Serilog.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        private string _projectName = "Project";
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                SetProperty(ref _projectName, value);
                OnPropertyChanged(nameof(ContinueButtonEnabled));
            }
        }

        public string ProjectLocation
        {
            get { return _projectFile?.DirectoryName ?? ""; }
        }

        private FileInfo _projectFile;
        public FileInfo ProjectFile
        {
            get { return _projectFile; }
            set
            {
                SetProperty(ref _projectFile, value);
                OnPropertyChanged(nameof(ContinueButtonEnabled));
                OnPropertyChanged(nameof(ProjectLocation));
            }
        }

        private GameType _gameType = GameType.BZCC;
        public GameType GameType
        {
            get { return _gameType; }
            set { SetProperty(ref _gameType, value); }
        }

        private bool _cloneStock = false;
        public bool CloneStock
        {
            get { return _cloneStock; }
            set { SetProperty(ref _cloneStock, value); }
        }

        private bool _fallback = false;
        public bool Fallback
        {
            get { return _fallback; }
            set { SetProperty(ref _fallback, value); }
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
            => !vm.HasErrors & !string.IsNullOrWhiteSpace(vm.ProjectLocation) & !string.IsNullOrWhiteSpace(vm.ProjectName);

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
    }
}
