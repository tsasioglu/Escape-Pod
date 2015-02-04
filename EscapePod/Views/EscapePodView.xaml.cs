using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using EscapePod.Model;
using EscapePod.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

namespace EscapePod.Views
{
    /// <summary>
    /// Interaction logic for EscapePodView.xaml
    /// </summary>
    public partial class EscapePodView : MetroWindow
    {
        public EscapePodView()
        {
            InitializeComponent();
        }

        [Dependency]
        public EscapePodViewModel ViewModel
        {
            get { return (EscapePodViewModel) DataContext; }
            set { DataContext = value; }
        }

        private void Link_OnGotFocus(object sender, RoutedEventArgs e)
        {
            Link.Text = Link.Text == Constants.DefaultLinkBoxText ? string.Empty : Link.Text;
        }

        private void Link_OnLostFocus(object sender, RoutedEventArgs e)
        {
            Link.Text = Link.Text == string.Empty ? Constants.DefaultLinkBoxText : Link.Text;
        }
       
    }
}
