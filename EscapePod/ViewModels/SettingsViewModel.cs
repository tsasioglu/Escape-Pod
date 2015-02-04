using System.Windows.Forms;
using System.Windows.Input;

namespace EscapePod.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public EpisodesViewModel EpisodesViewModel;

        public SettingsViewModel()
        {
            ChooseDownloadLocationCommand = new DelegateCommand(ChooseDownloadLocation);
            _sendErrorReports = Properties.Settings.Default.SendErrorReports;
                
        }

        public ICommand ChooseDownloadLocationCommand { get; set; }

        public string DownloadLocation
        {
            get { return Properties.Settings.Default.DownloadLocation; }
        }

        private bool _sendErrorReports;

        public bool SendErrorReports
        {
            get { return _sendErrorReports; }
            set
            {
                _sendErrorReports = value;
                Properties.Settings.Default.SendErrorReports = value;
                Properties.Settings.Default.Save();
                OnPropertyChanged();
            }
        }

        public void ChooseDownloadLocation(object o)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "Select folder for downloaded podcasts",
                ShowNewFolderButton = true,
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Properties.Settings.Default.DownloadLocation = dialog.SelectedPath + "\\";
                Properties.Settings.Default.Save();
                OnPropertyChanged("DownloadLocation");
                if (EpisodesViewModel.Episodes != null)
                {
                    foreach (EpisodeViewModel episode in EpisodesViewModel.Episodes)
                    {
                        episode.RaiseAllPropertyChanged();
                    }
                }
            }
        }
    }
}
