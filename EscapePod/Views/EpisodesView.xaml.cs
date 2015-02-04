using System.Windows.Controls;
using EscapePod.ViewModels;
using Microsoft.Practices.Unity;

namespace EscapePod.Views
{
    /// <summary>
    /// Interaction logic for EpisodesView.xaml
    /// </summary>
    public partial class EpisodesView : UserControl
    {
        public EpisodesView()
        {
            InitializeComponent();
        }

        [Dependency]
        public EpisodesViewModel ViewModel
        {
            get { return (EpisodesViewModel)DataContext; }
            set { DataContext = value; }
        }

        
    }
}
