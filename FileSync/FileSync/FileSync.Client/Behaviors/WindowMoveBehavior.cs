using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace FileSync.Client.Behaviors
{
    public class WindowMoveBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            AssociatedObject.MouseDown += AssociatedObject_MouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
            base.OnDetaching();
        }

        private void AssociatedObject_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var window = Application.Current.MainWindow;
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                    window.Top = 3;
                }
                window.DragMove();
            }
        }
    }
}
