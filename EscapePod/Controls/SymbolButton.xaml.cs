using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EscapePod.ViewModels;

namespace EscapePod.Controls
{
    /// <summary>
    /// Interaction logic for SymbolButton.xaml
    /// </summary>
    public partial class SymbolButton : UserControl
    {
        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // TODO SHOULD THIS BE HERE OR IN VIEW MODEL?

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(Symbol), typeof(SymbolButton), new PropertyMetadata(Symbol.Unset));

        public SymbolButton()
        {
            InitializeComponent();
        }
    }
}
