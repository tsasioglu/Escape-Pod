using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace EscapePod.Behaviours
{
    public class MinWidthSplitterBehavior : Behavior<Grid>
    {
        public Grid ParentGrid { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            ParentGrid = AssociatedObject;
            ParentGrid.SizeChanged += parent_SizeChanged;
        }

        void parent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ParentGrid.ColumnDefinitions.Count == 3)
            {
                Double maxWidth = e.NewSize.Width - ParentGrid.ColumnDefinitions[2].MinWidth - ParentGrid.ColumnDefinitions[1].ActualWidth;
                ParentGrid.ColumnDefinitions[0].MaxWidth = maxWidth;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (ParentGrid != null)
            {
                ParentGrid.SizeChanged -= parent_SizeChanged;
            }
        }
    }
}
