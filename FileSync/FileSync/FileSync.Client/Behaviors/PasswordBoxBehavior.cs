using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace FileSync.Client.Behaviors
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordText = AssociatedObject.Password;
        }

        public string PasswordText
        {
            get { return (string)GetValue(PasswordTextProperty); }
            set { SetValue(PasswordTextProperty, value); }
        }

        public static readonly DependencyProperty PasswordTextProperty = DependencyProperty.Register(
            "PasswordText", typeof(string), typeof(PasswordBoxBehavior), new PropertyMetadata(null));
    }
}
