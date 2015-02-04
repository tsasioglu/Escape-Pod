using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EscapePod.Model;
using EscapePod.Views;
using Microsoft.Practices.Unity;

namespace EscapePod.ViewModels
{
    public class EscapePodViewModel : ViewModelBase
    {
        public ImageSource Icon
        {
            get { return GetBitmapImage(Properties.Resources.PodcastIconLight); }
        }

        private BitmapImage GetBitmapImage(Bitmap bitmap)
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

        public ICommand GoLinkButtonCommand { get; set; }
        public ICommand SubscribeCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand GoToPodcastCommand { get; set; }

        public EscapePodViewModel()
        {
            Link = Constants.DefaultLinkBoxText;

            GoLinkButtonCommand = new DelegateCommand(OnGoLinkButtonPressed, _ => !EpisodesViewModel.IsLoading);
            SubscribeCommand = new DelegateCommand(OnSubscribeButtonPressed, _ =>  !PodcastsViewModel.IsLoading);
            SettingsCommand = new DelegateCommand(ShowSettings);
            GoToPodcastCommand = new DelegateCommand(GoToPodcast);
        }

        private void GoToPodcast(object obj)
        {
            Podcast podcast = (Podcast) obj;
            EpisodesViewModel.LoadPodcast(podcast.Url);
        }

        [Dependency]
        public EpisodesViewModel EpisodesViewModel { get; set; }

        [Dependency]
        public PodcastsViewModel PodcastsViewModel { get; set; }

        private string _link;
        public string Link
        {
            get { return _link; }
            set
            {
                _link = value;
                OnPropertyChanged();
            }
        }

        private void OnGoLinkButtonPressed(object obj)
        {
           EpisodesViewModel.LoadPodcast(Link);
        }

        private void OnSubscribeButtonPressed(object obj)
        {
            PodcastsViewModel.SubscribeToPodcast(Link);
        }

        private void ShowSettings(object obj)
        {
            SettingsView settingsView = new SettingsView();
            //todo why can't unity do this
            settingsView.ViewModel = new SettingsViewModel();
            settingsView.ViewModel.EpisodesViewModel = EpisodesViewModel;
            settingsView.Show();
        }
    }
}
