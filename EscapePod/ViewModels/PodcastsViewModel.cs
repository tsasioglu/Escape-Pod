using System;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Serialization;
using EscapePod.Model;
using EscapePod.Providers;

namespace EscapePod.ViewModels
{
    public class PodcastsViewModel : ViewModelBase
    {
        private readonly IDeserialisedRssProvider _deserialisedRssProvider;

        public PodcastsViewModel(IDeserialisedRssProvider deserialisedRssProvider)
        {
            if (deserialisedRssProvider == null) throw new ArgumentNullException("deserialisedRssProvider");

            _deserialisedRssProvider = deserialisedRssProvider;

            DeletePodcastCommand = new DelegateCommand(DeletePodcast);

            LoadSubscribedPodcasts();
        }

        private void DeletePodcast(object obj)
        {
            Podcast podcast = (Podcast)obj;
            Podcasts.Remove(podcast);
            SaveSubscribedPodcasts();
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

        private string PodcastsFolderLocation
        {
            get
            {
                return String.Format("{0}\\{1}",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "EscapePod");
            }
        }

        private void LoadSubscribedPodcasts()
        {
            if (!File.Exists(PodcastsFileLocation))
            {
                Directory.CreateDirectory(PodcastsFolderLocation);
                Podcasts = new ObservableCollection<Podcast>();
                return;
            }

            XmlSerializer reader = new XmlSerializer(typeof(Podcast[]));
            StreamReader file = new StreamReader(PodcastsFileLocation);
            Podcasts = new ObservableCollection<Podcast>((Podcast[])reader.Deserialize(file));
        }

        private void SaveSubscribedPodcasts()
        {
            XmlSerializer writer = new XmlSerializer(typeof(Podcast[]));
            StreamWriter file = new StreamWriter(PodcastsFileLocation);
            writer.Serialize(file, Podcasts.ToArray());
            file.Close();
        }

        public ObservableCollection<Podcast> Podcasts { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeletePodcastCommand { get; set; }

        public void SubscribeToPodcast(string link)
        {
            IsLoading = true;

            try
            {
                rss Rss;
                try
                {
                    Rss = _deserialisedRssProvider.Deserialise(link);
                }
                catch (Exception e)
                {
                    DisplayError(e.Message);
                    return;
                }

                Podcast podcast = new Podcast
                {
                    Title = Rss.channel.title,
                    Url = link,
                };

                if (Podcasts.Contains(podcast))
                {
                    DisplayError("Already subscribed to podcast");
                    return;
                    //todo highlight or jump to/display
                }

                Podcasts.Add(podcast);
                SaveSubscribedPodcasts();
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
