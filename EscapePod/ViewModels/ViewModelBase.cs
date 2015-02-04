using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using EscapePod.Annotations;

namespace EscapePod.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged

        protected static void DisplayError(string error)
        {
            MessageBox.Show(error);
        }
    }
}
