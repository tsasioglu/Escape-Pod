using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml.Serialization;
using EscapePod.Annotations;
using EscapePod.ExtensionMethods;
using EscapePod.Model;
using EscapePod.Providers;

namespace EscapePod.ViewModels
{
    public enum EpisodeState
    {
        NotDownloaded,
        Downloading,
        Downloaded,
    }

    public class EpisodeViewModel : INotifyPropertyChanged
    {
        private readonly IDirectoryProvider _directoryProvider;
        private readonly Episode _episode;
        
        public EpisodeViewModel(IDirectoryProvider directoryProvider, rss rss, Episode episode)
        {
            if (directoryProvider == null) throw new ArgumentNullException("directoryProvider");

            _directoryProvider = directoryProvider;

            Rss = rss;
            _episode = episode;
            State = File.Exists(FilePath) ? EpisodeState.Downloaded : EpisodeState.NotDownloaded;

            //todo delegateCommand version that doesn't pass arg?
            DownloadCommand = new DelegateCommand(Download);
            PlayCommand = new DelegateCommand(Play);
        }

        private EpisodeState _state;
        private EpisodeState State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaiseAllPropertyChanged();
            }
        }

        public rss Rss { get; set; }

        public bool IsNotDownloaded
        {
            get { return _state == EpisodeState.NotDownloaded; }
        }

        public bool IsDownloading
        {
            get { return _state == EpisodeState.Downloading; }
        }

        public bool IsDownloaded 
        {
            get { return _state == EpisodeState.Downloaded; }
        }

        private int _progress;


        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public string Title { get { return _episode.title; } }
        public string Description { get { return WebUtility.HtmlDecode(_episode.description); } }
        public string PubDate { get { return _episode.pubDate; } }
        public string Duration { get { return _episode.duration; } }

        [XmlIgnore]
        public ICommand DownloadCommand { get; set; }
        [XmlIgnore]
        public ICommand PlayCommand { get; set; }

        public string DownloadButtonText
        {
            get
            {
                switch (State)
                {
                    case EpisodeState.NotDownloaded:
                        return "Download";
                    case EpisodeState.Downloading:
                        return "Downloading";
                    case EpisodeState.Downloaded:
                        return "Downloaded";
                    default:
                        return "Unknown";
                }
            }
        }

        public string DownloadButtonSymbol
        {
            get
            {
                switch (State)
                {
                    case EpisodeState.NotDownloaded:
                        return Constants.DownloadSymbol;
                    default:
                        return String.Empty;
                }
            }
        }

        public string PlayButtonSymbol
        {
            get
            {
                return Constants.PlaySymbol;
            }
        }
    

        public void RaiseAllPropertyChanged()
        {
            OnPropertyChanged("IsNotDownloaded");
            OnPropertyChanged("IsDownloading");
            OnPropertyChanged("IsDownloaded");
            OnPropertyChanged("DownloadButtonText");
            OnPropertyChanged("Progress");
            OnPropertyChanged("PlayButtonSymbol");
            OnPropertyChanged("DownloadButtonSymbol");
        }

        public string FilePath
        {
            get
            {
                string fileName = _directoryProvider.SanitiseFileName(_episode.title);
                return String.Format("{0}\\{1}.{2}", FolderPath, fileName, ExtractFileExtension(_episode.GetUrl()));
            }
        }

        public string FolderPath
        {
            get
            {
                string folderName = _directoryProvider.SanitiseFileName(Rss.channel.title);
                return String.Format("{0}{1}", Properties.Settings.Default.DownloadLocation, folderName);
            }
        }

        private static string ExtractFileExtension(string url)
        {
            return url.Split(new[] { '.' }).LastOrDefault();
        }

        public void Download(object o)
        {
            EnsurePodcastFolderCreated();
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += (sender, args) =>
                {
                    State = EpisodeState.Downloaded;
                    Progress = 0;
                };
                webClient.DownloadProgressChanged += (sender, args) =>
                {
                    Progress = args.ProgressPercentage;
                };
                State = EpisodeState.Downloading;
                webClient.DownloadFileAsync(new Uri(_episode.GetUrl()), FilePath);
            }
        }

        private void EnsurePodcastFolderCreated()
        {
            if (!File.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
        }

        private void Play(object obj)
        {
            Process.Start(FilePath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
