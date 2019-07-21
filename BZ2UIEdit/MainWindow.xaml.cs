using BZ2UIEdit.Commands;
using BZ2UIEdit.Common;
using BZ2UIEdit.Common.Models;
using BZ2UIEdit.Extensions;
using BZ2UIEdit.Services;
using BZ2UIEdit.Services.FileService;
using BZ2UIEdit.Services.NewProjectService;
using BZ2UIEdit.UserControls;
using BZ2UIEdit.ViewModels;
using BZ2UIEdit.Views;
using BZ2UIEdit.Windows;
using MahApps.Metro.Controls.Dialogs;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace BZ2UIEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static LoggingLevelSwitch LevelSwitch { get; }
            = new LoggingLevelSwitch(Properties.Settings.Default.Debug ? Serilog.Events.LogEventLevel.Debug : Serilog.Events.LogEventLevel.Information);

        public ICommand NewProjectCommand { get; }
            = new RelayCommand(async _ => await CreateProject());

        public ICommand CloseCommand { get; }
            = new RelayCommand(
                _ => Application.Current.MainWindow.Close(),
                _ => (Application.Current != null && Application.Current.MainWindow != null));

        public ICommand ShowHelpCommand { get; }
            = new RelayCommand(
                _ => Process.Start("https://github.com/LuzFaltex/BZ2UIEdit/wiki"));

        public ICommand ShowAboutCommand { get; }
            = new RelayCommand(
                _ => new AboutDialog().ShowDialog());

        private readonly MainWindowViewModel viewModel = new MainWindowViewModel();

        public MainWindow()
        {
            string applicationBase = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LuzFaltex", "BZ2UIEdit");
            DataContext = this;
            InitializeComponent();
            MainPage mainPage = new MainPage(this, new MainPageViewModel());
            MainFrame.Navigate(mainPage);

            StatusLabel.DataContext = viewModel;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(LevelSwitch)
                .WriteTo.RollingFile($@"{applicationBase}\Logs\{{Date}}.log")
                .WriteTo.GuiLogger((LogViewer)mainPage.FindName("LogControl"))
                .CreateLogger();

            Log.Information("Startup complete");
        }

        private static async Task CreateProject()
        {
            var vm = new NewProjectDialogViewModel();

            var result = new NewProjectDialog(vm).ShowDialog();

            // MessageBox.Show($"Project Name: {vm.ProjectName}{Environment.NewLine}Project Location: {vm.ProjectLocation}{Environment.NewLine}Game Type: {vm.GameType}{Environment.NewLine}Project Type: {vm.ProjectType}", "result");

            // If they canceled the operation, just quit.
            if (result == false) return;

            var instance = (MainWindow)Application.Current.MainWindow;
            CancellationTokenSource cts = new CancellationTokenSource();
            MetroDialogSettings mds = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Theme,
                AffirmativeButtonText = "Continue",
                CancellationToken = cts.Token
            };

            ProgressDialogController controller = await instance.ShowProgressAsync($"Creating Project '{vm.ProjectName}'", "Please wait...", true, mds);

            IFileService fileService = new FileService();
            INewProjectService newProjectService = new NewProjectService(Log.Logger, fileService);

            await newProjectService.CreateNewProject(vm.ProjectName, vm.ProjectLocation, vm.GameType, vm.CloneStock, vm.Fallback, controller);

            controller.SetProgress(1d);
            controller.SetMessage("Done!");

            await Task.Delay(1000);

            await controller.CloseAsync();

            // Open the newly created project
        }
    }
}
