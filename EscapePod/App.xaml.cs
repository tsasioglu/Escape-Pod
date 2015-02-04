using System;
using System.Windows;
using System.Windows.Threading;
using EscapePod.Logging;
using EscapePod.Providers;
using EscapePod.ViewModels;
using EscapePod.Views;
using Microsoft.Practices.Unity;
using System.IO;
using System.Net;

namespace EscapePod
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            CopyDefaultPodcasts();

            DispatcherUnhandledException += App_DispatcherUnhandledException;

            IUnityContainer container = new UnityContainer();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<EpisodesViewModel, EpisodesViewModel>();
            container.RegisterType<PodcastsViewModel, PodcastsViewModel>();
            container.RegisterType<EscapePodViewModel, EscapePodViewModel>();
            container.RegisterType<SettingsViewModel, SettingsViewModel>();
            container.RegisterType<IDeserialisedRssProvider, DeserialisedRssProvider>();
            container.RegisterType<IDirectoryProvider, DirectoryProvider>();
            container.RegisterType<ICrashReporter, CrashReporter>();

           

            EscapePodView escapePodView = container.Resolve<EscapePodView>();
            escapePodView.Show();
        }

        private void CopyDefaultPodcasts()
        {
            try
            {
                if (!File.Exists(FlagLocation))
                {
                    Directory.CreateDirectory(PodcastsLocation);
                    using (WebClient Client = new WebClient())
                    {
                        Client.DownloadFile("http://escapepod.azurewebsites.net/Podcasts.xml", "Podcasts.xml");
                    }
                    File.Copy("Podcasts.xml", PodcastsFileLocation);
                    File.Create(FlagLocation);
                }
            }
            catch (Exception e)
            {

            }
        }

        private string FlagLocation
        {
            get
            {
                return String.Format("{0}\\{1}\\{2}",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "EscapePod",
                    "prerun.txt");
            }
        }

        private string PodcastsLocation
        {
            get
            {
                return String.Format("{0}\\{1}",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "EscapePod");
            }
        }

        private string PodcastsFileLocation
        {
            get
            {
                return String.Format("{0}\\{1}\\{2}",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "EscapePod",
                    "Podcasts.xml");
            }
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (!EscapePod.Properties.Settings.Default.SendErrorReports)
                return;

            // Process unhandled exception
            CrashReporter crashReporter = new CrashReporter();
            crashReporter.Report(e.Exception);
                
            // Prevent default unhandled exception processing
            e.Handled = true;
        }
        
    }
}
