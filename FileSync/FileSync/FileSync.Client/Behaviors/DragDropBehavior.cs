using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace FileSync.Client.Behaviors
{
    public class DragDropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.Drop += AssociatedObject_Drop;
            AssociatedObject.DragEnter += AssociatedObject_DragEnter;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Drop -= AssociatedObject_Drop;
            AssociatedObject.DragEnter -= AssociatedObject_DragEnter;
        }

        private void AssociatedObject_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void AssociatedObject_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null && files.Any())
                {
                    e.Effects = DragDropEffects.Copy;
                    DropCommand?.Execute(files);
                }
            }
        }

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        public static readonly DependencyProperty DropCommandProperty = DependencyProperty.Register(
            "DropCommand", typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(null));
    }
}
