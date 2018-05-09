using System;
using System.Windows;

namespace HotelsApp.UI.AttachedProperties
{
    public abstract class BaseAttachedProperty<TParent, TProperty>
        where TParent : new()
    {
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, e) => { };
        public event Action<DependencyObject, object> ValueUpdated = (sender, value) => { };

        public static TParent Instance { get; private set; } = new TParent();

        public static readonly DependencyProperty ValueProperty =
             DependencyProperty.RegisterAttached("Value",
                 typeof(TProperty),
                 typeof(BaseAttachedProperty<TParent, TProperty>),
                 new UIPropertyMetadata(default(TProperty), new PropertyChangedCallback(OnValuePropertyChanged), new CoerceValueCallback(OnValuePropertyUpdated)));

        static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = Instance as BaseAttachedProperty<TParent, TProperty>;
            instance?.OnValueChanged(d, e);
            instance?.ValueChanged(d, e);
        }

        public static TProperty GetValue(DependencyObject d) => (TProperty)d.GetValue(ValueProperty);
        public static void SetValue(DependencyObject d, TProperty value) => d.SetValue(ValueProperty, value);

        public virtual void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) { }
        public virtual void OnValueUpdated(DependencyObject sender, object value) { }

        static object OnValuePropertyUpdated(DependencyObject d, object value)
        {
            var instance = Instance as BaseAttachedProperty<TParent, TProperty>;
            instance?.OnValueUpdated(d, value);
            instance?.ValueUpdated(d, value);
            return value;
        }

    }
}
