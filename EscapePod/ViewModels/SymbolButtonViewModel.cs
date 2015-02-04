using System.Collections.Generic;
using System.Windows;

namespace EscapePod.ViewModels
{
    public enum Symbol
    {
        Unset,
        Download,
        Play,
    }

    public class SymbolButtonViewModel : DependencyObject
    {
        private readonly Dictionary<Symbol, string> _symbolCharacterMap = new Dictionary<Symbol, string>
        {
            {Symbol.Download, "\uE118"},
            {Symbol.Play, "\uE102"},
        };

        private readonly Dictionary<Symbol, string> _symbolTextMap = new Dictionary<Symbol, string>
        {
            {Symbol.Download, "Download"},
            {Symbol.Play, "Play"},
        };
    }
}
