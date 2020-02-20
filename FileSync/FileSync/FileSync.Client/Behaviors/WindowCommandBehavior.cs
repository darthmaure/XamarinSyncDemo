using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace FileSync.Client.Behaviors
{
    public class WindowCommandBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click -= AssociatedObject_Click;
            AssociatedObject.Click += AssociatedObject_Click;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObject_Click;
            base.OnDetaching();
        }

        private void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            var commandName = AssociatedObject.CommandParameter as string;
            if (_actions.ContainsKey(commandName))
            {
                _actions[commandName].Invoke(window);
            }
        }

        private IDictionary<string, System.Action<Window>> _actions = new Dictionary<string, System.Action<Window>>
        {
            { "minimize", w => w.WindowState = WindowState.Minimized },
            { "maximize", w => w.WindowState = w.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized },
            { "close", w => w.Close() }
        };
    }
}
