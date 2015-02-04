using System.Windows;
using EscapePod.ViewModels;
using MahApps.Metro.Controls;
using Microsoft.Practices.Unity;

namespace EscapePod.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : MetroWindow
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        [Dependency]
        public SettingsViewModel ViewModel
        {
            get { return (SettingsViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
