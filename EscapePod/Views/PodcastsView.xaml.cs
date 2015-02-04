using EscapePod.ViewModels;
using Microsoft.Practices.Unity;

namespace EscapePod.Views
{
    public partial class PodcastsView
    {
        public PodcastsView()
        {
            InitializeComponent();
        }

        [Dependency]
        public PodcastsViewModel ViewModel
        {
            get { return (PodcastsViewModel) DataContext; }
            set { DataContext = value; }
        }
    }
}