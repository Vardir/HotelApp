using System.Windows;

namespace HotelsApp.UI.AttachedProperties
{
    public class InputBindingBehavior
    {
        public static readonly DependencyProperty PropagateInputBindingsToWindowProperty =
            DependencyProperty.RegisterAttached("PropagateInputBindingsToWindow", typeof(bool), typeof(InputBindingBehavior),
            new PropertyMetadata(false, OnPropagateInputBindingsToWindowChanged));

        public static bool GetPropagateInputBindingsToWindow(FrameworkElement obj)
        {
            return (bool)obj.GetValue(PropagateInputBindingsToWindowProperty);
        }
        public static void SetPropagateInputBindingsToWindow(FrameworkElement obj, bool value)
        {
            obj.SetValue(PropagateInputBindingsToWindowProperty, value);
        }

        static void OnPropagateInputBindingsToWindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FrameworkElement)d).Loaded += FrameworkElement_Loaded;
        }
        static void FrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null) return;

            frameworkElement.Loaded -= FrameworkElement_Loaded;

            //var window = Window.GetWindow(frameworkElement);
            //if (window == null) return;

            //for (int i = frameworkElement.InputBindings.Count - 1; i >= 0; i--)
            //{
            //    var inputBinding = frameworkElement.InputBindings[i];
            //    window.InputBindings.Add(inputBinding);
            //    frameworkElement.InputBindings.Remove(inputBinding);
            //}
        }
    }
}