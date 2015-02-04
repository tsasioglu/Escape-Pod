using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using EscapePod.Logging;
using EscapePod.Model;
using EscapePod.Providers;

namespace EscapePod.ViewModels
{
    public class EpisodesViewModel : ViewModelBase
    {
        private readonly ILogger _logger;
        private readonly IDeserialisedRssProvider _deserialisedRssProvider;
        private readonly IDirectoryProvider _directoryProvider;

        public EpisodesViewModel(ILogger logger, IDeserialisedRssProvider deserialisedRssProvider, IDirectoryProvider directoryProvider)
        {
            IsLoading = true;

            if (logger == null) throw new ArgumentNullException("logger");
            if (deserialisedRssProvider == null) throw new ArgumentNullException("deserialisedRssProvider");
            if (directoryProvider == null) throw new ArgumentNullException("directoryProvider");

            _logger = logger;
            _deserialisedRssProvider = deserialisedRssProvider;
            _directoryProvider = directoryProvider;
            
            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.DownloadLocation))
            {
                Properties.Settings.Default.DownloadLocation =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            }

            IsLoading = false;
        }
        
        public void LoadPodcast(string podcastLink)
        {
            IsLoading = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    // debug
                    if (podcastLink == "Paste RSS XML here")
                    {
                        Rss = _deserialisedRssProvider.Deserialise(@"http://static.aboveandbeyond.nu/grouptherapy/podcast.xml");
                    }
                    else
                    {
                        Rss = _deserialisedRssProvider.Deserialise(podcastLink);
                    }
                }
                catch (Exception e)
                {
                    DisplayError(e.Message);
                    IsLoading = false;
                    return;
                }
                
                var sorted = Rss.channel.item.ToList();
                try
                {
                    sorted.Sort((x, y) => DateTime.Parse(y.pubDate).CompareTo(DateTime.Parse(x.pubDate)));
                    Rss.channel.item = sorted.ToArray();
                }
                catch (FormatException e)
                {
                    _logger.Error("Unable to parse dates for sorting: {0}", e);
                }
                catch (InvalidOperationException e)
                {
                    _logger.Error("Unable to parse dates for sorting: {0}", e);
                }

                List<EpisodeViewModel> episodes = new List<EpisodeViewModel>(Rss.channel.item.Length);
                episodes.AddRange(Rss.channel.item.Select(episode => new EpisodeViewModel(_directoryProvider, Rss, episode)));

                Episodes = episodes.ToArray();

                IsLoading = false;
            });
        }

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
        
        private rss _rss;
        public rss Rss
        {
            get
            {
                return _rss;
            }
            set
            {
                _rss = value;
                OnPropertyChanged();
                OnPropertyChanged("ImageSource");
                OnPropertyChanged("Title");
                OnPropertyChanged("Summary");
                OnPropertyChanged("Category");
                OnPropertyChanged("Keywords");
                OnPropertyChanged("Episodes");
            }
        }

        public BitmapImage ImageSource
        {
            get
            {
                if (Rss == null || Rss.channel == null)
                {
                    return new BitmapImage();
                }
                if (Rss.channel.image == null)
                {
                    return GetBitmapImage(Properties.Resources.NoImage);
                }
                return new BitmapImage(new Uri(Rss.channel.image.href));
            }
        }

        private static BitmapImage GetBitmapImage(Image bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        private Bitmap LoadPicture(string url)
        {
            Bitmap bitmap = null;
            Stream stream = null;
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowWriteStreamBuffering = true;

                response = (HttpWebResponse)request.GetResponse();

                if ((stream = response.GetResponseStream()) != null)
                    bitmap = new Bitmap(stream);
            }
            finally
            {
                if (stream != null)
                    stream.Close();

                if (response != null)
                    response.Close();
            }
            return (bitmap);
        }

        public string Title
        {
            get
            {
                if (Rss == null)
                {
                    return "";
                }
                return Rss.channel.title;
            }
        }

        public string Summary
        {
            get
            {
                if (Rss == null)
                {
                    return "";
                }
                return WebUtility.HtmlDecode(Rss.channel.summary);
            }
        }

        public string Category
        {
            get
            {
                if (Rss == null || Rss.channel == null || Rss.channel.category == null)
                {
                    return "";
                }
                return Rss.channel.category.text;
            }
        }

        public string Keywords
        {
            get
            {
                if (Rss == null)
                {
                    return "";
                }
                return Rss.channel.keyword;
            }
        }

        private EpisodeViewModel[] _episodes;
        public EpisodeViewModel[] Episodes
        {
            get { return _episodes; }
            set
            {
                _episodes = value;
                OnPropertyChanged();
            }
        }
    }
}
